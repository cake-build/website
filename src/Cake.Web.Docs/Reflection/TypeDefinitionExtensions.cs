using System;
using Mono.Cecil;

namespace Cake.Web.Docs.Reflection
{
    /// <summary>
    /// Contains extension methods for <see cref="TypeDefinition"/>.
    /// </summary>
    public static class TypeDefinitionExtensions
    {
        /// <summary>
        /// Determines whether the specified <see cref="TypeDefinition"/>
        /// retpresents a static type.
        /// </summary>
        /// <param name="type">The type definition.</param>
        /// <returns><c>true</c> if the type is static; otherwise <c>false</c>.</returns>
        public static bool IsStatic(this TypeDefinition type)
        {
            return type.IsAbstract && type.IsSealed;
        }

        /// <summary>
        /// Gets the type classification for this type reference.
        /// </summary>
        /// <param name="type">The type reference.</param>
        /// <returns>The type classification for this type reference.</returns>
        public static TypeClassification GetTypeClassification(this TypeReference type)
        {
            var definition = type.Resolve();
            if (definition == null)
            {
                throw new InvalidOperationException("Could not classify external reference.");
            }

            if (definition.IsEnum)
            {
                return TypeClassification.Enum;
            }
            if (definition.IsInterface)
            {
                return TypeClassification.Interface;
            }
            if (definition.IsValueType)
            {
                return TypeClassification.Struct;
            }
            if (definition.IsClass)
            {
                return TypeClassification.Class;
            }
            return TypeClassification.Unknown;
        }
    }
}
