using Cake.Web.Docs.Identity;
using Mono.Cecil;

namespace Cake.Web.Docs.Reflection.Model
{
    internal sealed class PropertyInfo : IPropertyInfo
    {
        private readonly string _identity;
        private readonly IDocumentationMetadata _metadata;
        private readonly PropertyDefinition _definition;

        public string Identity
        {
            get { return _identity; }
        }

        public IDocumentationMetadata Metadata
        {
            get { return _metadata; }
        }

        public PropertyDefinition Definition
        {
            get { return _definition; }
        }

        public PropertyInfo(PropertyDefinition definition, IDocumentationMetadata metadata)
        {
            _definition = definition;
            _metadata = metadata;
            _identity = CRefGenerator.GetPropertyCRef(definition);
        }
    }
}
