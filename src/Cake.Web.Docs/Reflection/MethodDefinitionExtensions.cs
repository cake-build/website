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

        public static bool IsCakeAlias(this MethodDefinition method, out bool isPropertyAlias)
        {
            foreach (var attribute in method.CustomAttributes)
            {
                if (attribute.AttributeType != null && (
                    attribute.AttributeType.FullName == "Cake.Core.Annotations.CakeMethodAliasAttribute" ||
                    attribute.AttributeType.FullName == "Cake.Core.Annotations.CakePropertyAliasAttribute"))
                {
                    isPropertyAlias = attribute.AttributeType.FullName == "Cake.Core.Annotations.CakePropertyAliasAttribute";
                    return true;
                }
            }
            isPropertyAlias = false;
            return false;
        }
    }
}
