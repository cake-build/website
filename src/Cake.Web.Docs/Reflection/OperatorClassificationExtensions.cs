namespace Cake.Web.Docs.Reflection
{
    /// <summary>
    /// Contains extension methods for <see cref="OperatorClassification"/>.
    /// </summary>
    public static class OperatorClassificationExtensions
    {
        /// <summary>
        /// Gets the name of the specified operator.
        /// </summary>
        /// <param name="operator">The operator.</param>
        /// <returns>The name of the specified operator</returns>
        public static string GetOperatorName(this OperatorClassification @operator)
        {
            switch (@operator)
            {
                case OperatorClassification.Addition: return "Addition";
                case OperatorClassification.BitwiseAnd: return "BitwiseAnd";
                case OperatorClassification.BitwiseOr: return "BitwiseOr";
                case OperatorClassification.Decrement: return "Decrement";
                case OperatorClassification.Division: return "Division";
                case OperatorClassification.Equality: return "Equality";
                case OperatorClassification.ExclusiveOr: return "ExclusiveOr";
                case OperatorClassification.Explicit: return "Explicit";
                case OperatorClassification.False: return "False";
                case OperatorClassification.GreaterThan: return "GreaterThan";
                case OperatorClassification.GreaterThanOrEqual: return "GreaterThanOrEqual";
                case OperatorClassification.Implicit: return "Implicit";
                case OperatorClassification.Increment: return "Increment";
                case OperatorClassification.Inequality: return "Inequality";
                case OperatorClassification.LeftShift: return "LeftShift";
                case OperatorClassification.LessThan: return "LessThan";
                case OperatorClassification.LessThanOrEqual: return "LessThanOrEqual";
                case OperatorClassification.LogicalNot: return "LogicalNot";
                case OperatorClassification.Modulus: return "Modulus";
                case OperatorClassification.Multiply: return "Multiply";
                case OperatorClassification.OnesComplement: return "OnesComplement";
                case OperatorClassification.RightShift: return "RightShift";
                case OperatorClassification.Subtraction: return "Subtraction";
                case OperatorClassification.True: return "True";
                case OperatorClassification.UnaryNegation: return "UnaryNegation";
                case OperatorClassification.UnaryPlus: return "UnaryPlus";
            }
            return null;
        }

        /// <summary>
        /// Gets the symbol for the specified operator.
        /// </summary>
        /// <param name="operator">The operator.</param>
        /// <returns>The symbol for the specified operator.</returns>
        public static string GetOperatorSymbol(this OperatorClassification @operator)
        {
            switch (@operator)
            {
                case OperatorClassification.Addition: return "+";
                case OperatorClassification.BitwiseAnd: return "&";
                case OperatorClassification.BitwiseOr: return "|";
                case OperatorClassification.Decrement: return "--";
                case OperatorClassification.Division: return "/";
                case OperatorClassification.Equality: return "==";
                case OperatorClassification.ExclusiveOr: return "^";
                case OperatorClassification.False: return "false";
                case OperatorClassification.GreaterThan: return ">";
                case OperatorClassification.GreaterThanOrEqual: return ">=";
                case OperatorClassification.Increment: return "++";
                case OperatorClassification.Inequality: return "!=";
                case OperatorClassification.LeftShift: return "<<";
                case OperatorClassification.LessThan: return "<";
                case OperatorClassification.LessThanOrEqual: return "<=";
                case OperatorClassification.LogicalNot: return "LogicalNot";
                case OperatorClassification.Modulus: return "%";
                case OperatorClassification.Multiply: return "*";
                case OperatorClassification.OnesComplement: return "~";
                case OperatorClassification.RightShift: return ">>";
                case OperatorClassification.Subtraction: return "-";
                case OperatorClassification.True: return "true";
                case OperatorClassification.UnaryNegation: return "-";
                case OperatorClassification.UnaryPlus: return "+";
            }
            return null;
        }
    }
}
