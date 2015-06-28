using Cake.Web.Docs.Identity;
using Cake.Web.Docs.Reflection.Signatures;
using Mono.Cecil;

namespace Cake.Web.Docs.Reflection
{
    /// <summary>
    /// Contains extension methods for <see cref="PropertyReference"/>.
    /// </summary>
    public static class PropertyDefinitionExtensions
    {
        /// <summary>
        /// Gets the property signature for the specified property reference.
        /// </summary>
        /// <param name="property">The property reference.</param>
        /// <param name="resolver">The link resolver.</param>
        /// <returns>The property signature for the specified property reference.</returns>
        public static PropertySignature GetPropertySignature(this PropertyReference property, IUrlResolver resolver)
        {
            // Get the property definition.
            var definition = property.Resolve();

            // Get the property Identity and name.
            var identity = CRefGenerator.GetPropertyCRef(definition);
            var name = definition.Name;
            var declaringType = definition.DeclaringType.GetTypeSignature(resolver);
            var propertyType = definition.PropertyType.GetTypeSignature(resolver);            

            return new PropertySignature(identity, name, declaringType, propertyType);
        }
    }
}
