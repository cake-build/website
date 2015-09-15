using System.Collections.Generic;
using Mono.Cecil;

namespace Cake.Web.Docs.Reflection.Model
{
    /// <summary>
    /// Represents reflected type information.
    /// </summary>
    public interface ITypeInfo
    {
        /// <summary>
        /// Gets the type Identity.
        /// </summary>
        /// <value>The Identity.</value>
        string Identity { get; }

        /// <summary>
        /// Gets the type Definition.
        /// </summary>
        /// <value>The type Definition.</value>
        TypeDefinition Definition { get; }

        /// <summary>
        /// Gets the type's methods.
        /// </summary>
        /// <value>The type's methods.</value>
        IReadOnlyList<IMethodInfo> Methods { get; }

        /// <summary>
        /// Gets the type's properties.
        /// </summary>
        /// <value>The type's properties.</value>
        IReadOnlyList<IPropertyInfo> Properties { get; }

        /// <summary>
        /// Gets the type's fields.
        /// </summary>
        /// <value>The fields.</value>
        IReadOnlyList<IFieldInfo> Fields { get; }

        /// <summary>
        /// The associated metadata.
        /// </summary>
        IDocumentationMetadata Metadata { get; }
    }
}
