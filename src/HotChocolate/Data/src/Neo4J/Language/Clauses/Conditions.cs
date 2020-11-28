﻿namespace HotChocolate.Data.Neo4J.Language
{
    /// <summary>
    /// Builder for various conditions
    /// </summary>
    public static class Conditions
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="lhs"></param>
        /// <param name="rhs"></param>
        /// <returns></returns>
        public static Condition Matches(Expression lhs, Expression rhs) =>
            Comparison.Create(lhs, Operator.Matches, rhs);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="lhs"></param>
        /// <param name="rhs"></param>
        /// <returns></returns>
        public static Condition IsEqualTo(Expression lhs, Expression rhs) =>
            Comparison.Create(lhs, Operator.Equality, rhs);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="lhs"></param>
        /// <param name="rhs"></param>
        /// <returns></returns>
        public static Condition IsNotEqualTo(Expression lhs, Expression rhs) =>
            Comparison.Create(lhs, Operator.InEquality, rhs);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="lhs"></param>
        /// <param name="rhs"></param>
        /// <returns></returns>
        public static Condition LessThan(Expression lhs, Expression rhs) =>
            Comparison.Create(lhs, Operator.LessThan, rhs);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="lhs"></param>
        /// <param name="rhs"></param>
        /// <returns></returns>
        public static Condition LessThanOrEqualTo(Expression lhs, Expression rhs) =>
            Comparison.Create(lhs, Operator.LessThanOrEqualTo, rhs);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="lhs"></param>
        /// <param name="rhs"></param>
        /// <returns></returns>
        public static Condition GreaterThan(Expression lhs, Expression rhs) =>
            Comparison.Create(lhs, Operator.GreaterThan, rhs);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="lhs"></param>
        /// <param name="rhs"></param>
        /// <returns></returns>
        public static Condition GreaterThanOEqualTo(Expression lhs, Expression rhs) =>
            Comparison.Create(lhs, Operator.GreaterThanOrEqualTo, rhs);

        public static Condition Not(Condition condition) => condition.Not();

        public static Condition Not(PatternElement patternElement) => new ExcludePattern(patternElement);
    }
}