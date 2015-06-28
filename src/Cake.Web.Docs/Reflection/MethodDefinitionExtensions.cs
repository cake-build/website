using Mono.Cecil;

namespace Cake.Web.Docs.Reflection
{
    /// <summary>
    /// Contains extension methods for <see cref="MethodDefinition"/>.
    /// </summary>
    public static class MethodDefinitionExtensions
    {
        /// <summary>
        /// Determines whether the specified method is explicitly implemented.
        /// </summary>
        /// <param name="method">The method to check.</param>
        /// <returns>Whether the specified method is explicitly implemented</returns>
        public static bool IsExplicitlyImplemented(this MethodDefinition method)
        {
            return method.IsPrivate && method.IsFinal && method.IsVirtual;
        }

        /// <summary>
        /// Determines whether the specified method is an operator.
        /// </summary>
        /// <param name="method">The method to check.</param>
        /// <returns>Whether the specified method is an operator.</returns>
        public static bool IsOperator(this MethodDefinition method)
        {
            return method.Name.StartsWith("op_");
        }
    }
}
