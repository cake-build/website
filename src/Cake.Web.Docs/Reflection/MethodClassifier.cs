using System;
using System.Collections.Generic;
using Mono.Cecil;

namespace Cake.Web.Docs.Reflection
{
    /// <summary>
    /// Responsible for classifying methods.
    /// </summary>
    public static class MethodClassifier
    {
        private static readonly Dictionary<string, OperatorClassification> Operators;

        static MethodClassifier()
        {
            Operators = new Dictionary<string, OperatorClassification>(StringComparer.Ordinal)
            {
                {"op_Addition", OperatorClassification.Addition},
                {"op_BitwiseAnd", OperatorClassification.BitwiseAnd},
                {"op_BitwiseOr", OperatorClassification.BitwiseOr},
                {"op_Decrement", OperatorClassification.Decrement},
                {"op_Division", OperatorClassification.Division},
                {"op_Equality", OperatorClassification.Equality},
                {"op_ExclusiveOr", OperatorClassification.ExclusiveOr},
                {"op_Explicit", OperatorClassification.Explicit},
                {"op_False", OperatorClassification.False},
                {"op_GreaterThan", OperatorClassification.GreaterThan},
                {"op_GreaterThanOrEqual", OperatorClassification.GreaterThanOrEqual},
                {"op_Implicit", OperatorClassification.Implicit},
                {"op_Increment", OperatorClassification.Increment},
                {"op_Inequality", OperatorClassification.Inequality},
                {"op_LeftShift", OperatorClassification.LeftShift},
                {"op_LessThan", OperatorClassification.LessThan},
                {"op_LessThanOrEqual", OperatorClassification.LessThanOrEqual},
                {"op_LogicalNot", OperatorClassification.LogicalNot},
                {"op_Modulus", OperatorClassification.Modulus},
                {"op_Multiply", OperatorClassification.Multiply},
                {"op_OnesComplement", OperatorClassification.OnesComplement},
                {"op_RightShift", OperatorClassification.RightShift},
                {"op_Subtraction", OperatorClassification.Subtraction},
                {"op_True", OperatorClassification.True},
                {"op_UnaryNegation", OperatorClassification.UnaryNegation},
                {"op_UnaryPlus", OperatorClassification.UnaryPlus},
            };
        }

        /// <summary>
        /// Gets the method classification for this method.
        /// </summary>
        /// <param name="method">The method definition.</param>
        /// <returns>A method classification.</returns>
        public static MethodClassification GetMethodClassification(MethodDefinition method)
        {
            if (method.IsConstructor)
            {
                return MethodClassification.Constructor;
            }
            if (method.IsSpecialName)
            {
                if (method.IsOperator())
                {
                    return MethodClassification.Operator;
                }
                if (method.Name.StartsWith("add_"))
                {
                    return MethodClassification.EventAccessor;
                }
                if (method.Name.StartsWith("remove_"))
                {
                    return MethodClassification.EventAccessor;
                }
            }
            if (method.IsExtensionMethod())
            {
                return MethodClassification.ExtensionMethod;
            }
            return MethodClassification.Method;
        }

        /// <summary>
        /// Gets the operator classification for the specified method.
        /// </summary>
        /// <param name="method">The method to get the operator classification for.</param>
        /// <returns>The operator classification or <c>null</c> if the specified method wasn't an operator.</returns>
        public static OperatorClassification GetOperatorClassification(MethodDefinition method)
        {
            if (method.IsOperator())
            {
                if (Operators.ContainsKey(method.Name))
                {
                    return Operators[method.Name];
                }
            }
            return OperatorClassification.Unknown;
        }
    }
}
