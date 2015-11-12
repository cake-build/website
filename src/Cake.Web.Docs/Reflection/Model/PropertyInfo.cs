using Cake.Web.Docs.Identity;
using Mono.Cecil;

namespace Cake.Web.Docs.Reflection.Model
{
    internal sealed class PropertyInfo : IPropertyInfo
    {
        public string Identity { get; }
        public IDocumentationMetadata Metadata { get; }
        public PropertyDefinition Definition { get; }

        public PropertyInfo(PropertyDefinition definition, IDocumentationMetadata metadata)
        {
            Definition = definition;
            Metadata = metadata;
            Identity = CRefGenerator.GetPropertyCRef(definition);
        }
    }
}
