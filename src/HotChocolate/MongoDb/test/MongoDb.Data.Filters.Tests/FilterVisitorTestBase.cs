using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using HotChocolate.Data;
using HotChocolate.Data.Filters;
using HotChocolate.Execution;
using HotChocolate.Execution.Configuration;
using HotChocolate.Resolvers;
using HotChocolate.Types;
using Microsoft.Extensions.DependencyInjection;
using MongoDB.Bson;
using MongoDB.Driver;
using Squadron;

namespace HotChocolate.MongoDb.Data.Filters
{
    public class FilterVisitorTestBase
    {
        protected string? FileName { get; set; } = Guid.NewGuid().ToString("N") + ".db";

        private Func<IResolverContext, Task<IEnumerable<TResult>>> BuildResolver<TResult>(
            MongoResource mongoResource,
            params TResult[] results)
            where TResult : class
        {
            if (FileName is null)
            {
                throw new InvalidOperationException();
            }

            IMongoCollection<TResult> collection =
                mongoResource.CreateCollection<TResult>("data_" + Guid.NewGuid().ToString("N"));

            collection.InsertMany(results);

            return async ctx =>
            {
                if (ctx.LocalContextData.TryGetValue(
                        nameof(FilterDefinition<TResult>),
                        out var def) &&
                    def is BsonDocument filter)
                {
                    ctx.ContextData["query"] = filter.ToString();
                    List<TResult> result = await collection.Find(filter).ToListAsync();
                    return result;
                }

                return new List<TResult>();
            };
        }

        protected IRequestExecutor CreateSchema<TEntity, T>(
            TEntity[] entities,
            MongoResource mongoResource,
            bool withPaging = false)
            where TEntity : class
            where T : FilterInputType<TEntity>
        {
            Func<IResolverContext, Task<IEnumerable<TEntity>>> resolver = BuildResolver(
                mongoResource,
                entities);

            ISchemaBuilder builder = SchemaBuilder.New()
                .AddFiltering(x => x.BindRuntimeType<TEntity, T>().AddMongoDbDefaults())
                .AddQueryType(
                    c => c
                        .Name("Query")
                        .Field("root")
                        .Resolver(resolver)
                        .UseFiltering<T>());

            ISchema schema = builder.Create();

            return new ServiceCollection()
                .Configure<RequestExecutorFactoryOptions>(
                    Schema.DefaultName,
                    o => o.Schema = schema)
                .AddGraphQL()
                .UseRequest(
                    next => async context =>
                    {
                        await next(context);
                        if (context.Result is IReadOnlyQueryResult result &&
                            context.ContextData.TryGetValue("query", out var queryString))
                        {
                            context.Result =
                                QueryResultBuilder
                                    .FromResult(result)
                                    .SetContextData("query", queryString)
                                    .Create();
                        }
                    })
                .UseDefaultPipeline()
                .Services
                .BuildServiceProvider()
                .GetRequiredService<IRequestExecutorResolver>()
                .GetRequestExecutorAsync()
                .Result;
        }
    }
}
