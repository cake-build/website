using System.Collections.Generic;

namespace Cake.Web.Docs.Reflection.Model
{
    /// <summary>
    /// Represents the reflection model.
    /// </summary>
    public sealed class ReflectionModel
    {
        /// <summary>
        /// Gets the types.
        /// </summary>
        /// <value>The types.</value>
        public IReadOnlyList<IAssemblyInfo> Assemblies { get; private set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="ReflectionModel" /> class.
        /// </summary>
        /// <param name="assemblies">The assemblies.</param>
        public ReflectionModel(IEnumerable<IAssemblyInfo> assemblies)
        {
            Assemblies = new List<IAssemblyInfo>(assemblies);
        }
    }
}