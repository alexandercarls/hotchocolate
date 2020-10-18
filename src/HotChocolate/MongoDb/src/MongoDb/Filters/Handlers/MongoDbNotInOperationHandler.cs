using HotChocolate.Configuration;
using HotChocolate.Data.Filters;
using HotChocolate.Language;
using MongoDB.Bson;
using MongoDB.Driver;

namespace HotChocolate.MongoDb.Data.Filters
{
    public class MongoDbNotInOperationHandler
        : MongoDbOperationHandlerBase
    {
        public override bool CanHandle(
            ITypeDiscoveryContext context,
            IFilterInputTypeDefinition typeDefinition,
            IFilterFieldDefinition fieldDefinition)
        {
            return fieldDefinition is FilterOperationFieldDefinition operationField &&
                operationField.Id is DefaultOperations.NotIn;
        }

        public override FilterDefinition<BsonDocument> HandleOperation(
            MongoDbFilterVisitorContext context,
            IFilterOperationField field,
            IValueNode value,
            object? parsedValue)
        {
            var doc = new BsonDocument(
                "$nin",
                BsonValue.Create(parsedValue));

            return new BsonDocument(
                context.GetMongoFilterScope().GetPath(),
                doc);
        }
    }
}