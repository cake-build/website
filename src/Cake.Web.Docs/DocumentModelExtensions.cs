namespace Cake.Web.Docs
{
    /// <summary>
    /// Contains extension methods for <see cref="DocumentModel"/>.
    /// </summary>
    public static class DocumentModelExtensions
    {
        /// <summary>
        /// Finds the namespace with the specified identity.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <param name="identity">The identity.</param>
        /// <returns>The first namespace the matching identity.</returns>
        public static DocumentedNamespace FindNamespace(this DocumentModel model, string identity)
        {
            foreach (var assembly in model.Assemblies)
            {
                foreach (var @namespace in assembly.Namespaces)
                {
                    if (@namespace.Identity == identity)
                    {
                        return @namespace;
                    }
                }
            }
            return null;
        }

        /// <summary>
        /// Finds the specified documented type.
        /// </summary>
        /// <param name="model">The document model.</param>
        /// <param name="identity">The Identity of the documented type.</param>
        /// <returns>The documented type.</returns>
        public static DocumentedType FindType(this DocumentModel model, string identity)
        {
            foreach (var assembly in model.Assemblies)
            {
                foreach (var @namespace in assembly.Namespaces)
                {
                    foreach (var type in @namespace.Types)
                    {
                        if (type.Identity == identity)
                        {
                            return type;
                        }
                    }
                }
            }
            return null;
        }
    }
}
