using System.Collections.Generic;

namespace Cake.Web.Docs
{
    /// <summary>
    /// Represents the complete documented model.
    /// </summary>
    public sealed class DocumentModel
    {
        /// <summary>
        /// Gets the assemblies.
        /// </summary>
        /// <value>The assemblies.</value>
        public IReadOnlyList<DocumentedAssembly> Assemblies { get; }

        /// <summary>
        /// Initializes a new instance of the <see cref="DocumentModel"/> class.
        /// </summary>
        /// <param name="assemblies">The assemblies.</param>
        public DocumentModel(IEnumerable<DocumentedAssembly> assemblies)
        {
            Assemblies = new List<DocumentedAssembly>(assemblies);
        }
    }
}
