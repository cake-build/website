using Cake.Web.Docs.Identity;
using Mono.Cecil;

namespace Cake.Web.Docs.Reflection.Model
{
    internal sealed class PropertyInfo : IPropertyInfo
    {
        private readonly string _identity;
        private readonly PropertyDefinition _definition;

        public string Identity
        {
            get { return _identity; }
        }

        public PropertyDefinition Definition
        {
            get { return _definition; }
        }

        public PropertyInfo(PropertyDefinition definition)
        {
            _definition = definition;
            _identity = CRefGenerator.GetPropertyCRef(definition);
        }
    }
}
