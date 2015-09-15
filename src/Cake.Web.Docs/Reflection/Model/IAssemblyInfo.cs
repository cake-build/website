using System.Collections.Generic;
using Mono.Cecil;

namespace Cake.Web.Docs.Reflection.Model
{
    /// <summary>
    /// Represents reflected assembly information.
    /// </summary>
    public interface IAssemblyInfo
    {
        /// <summary>
        /// Gets the assembly Identity.
        /// </summary>
        /// <value>The Identity.</value>
        string Identity { get; }

        /// <summary>
        /// Gets the assembly name.
        /// </summary>
        /// <value>The assembly name.</value>
        string Name { get; }
        
        /// <summary>
        /// Gets the assembly definition.
        /// </summary>
        /// <value>The assembly definition.</value>
        AssemblyDefinition Definition { get; }

        /// <summary>
        /// Gets the types.
        /// </summary>
        /// <value>The types.</value>
        IReadOnlyList<ITypeInfo> Types { get; }

        /// <summary>
        /// The associated metadata.
        /// </summary>
        IDocumentationMetadata Metadata { get; }
    }
}
