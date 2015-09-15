using Mono.Cecil;

namespace Cake.Web.Docs.Reflection.Model
{
    /// <summary>
    /// Represents reflected method information.
    /// </summary>
    public interface IMethodInfo
    {
        /// <summary>
        /// Gets the method identity.
        /// </summary>
        /// <value>The method identity.</value>
        string Identity { get; }

        /// <summary>
        /// Gets the method definition.
        /// </summary>
        /// <value>
        /// The method definition.
        /// </value>
        MethodDefinition Definition { get; }

        /// <summary>
        /// The associated metadata.
        /// </summary>
        IDocumentationMetadata Metadata { get; }
    }
}
