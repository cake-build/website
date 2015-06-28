using System.Collections.Generic;

namespace Cake.Web.Docs
{
    /// <summary>
    /// Represents the complete documented model.
    /// </summary>
    public sealed class DocumentModel
    {
        private readonly List<DocumentedAssembly> _assemblies;

        /// <summary>
        /// Gets the assemblies.
        /// </summary>
        /// <value>The assemblies.</value>
        public IReadOnlyList<DocumentedAssembly> Assemblies
        {
            get { return _assemblies; }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="DocumentModel"/> class.
        /// </summary>
        /// <param name="assemblies">The assemblies.</param>
        public DocumentModel(IEnumerable<DocumentedAssembly> assemblies)
        {
            _assemblies = new List<DocumentedAssembly>(assemblies);
        }
    }
}
