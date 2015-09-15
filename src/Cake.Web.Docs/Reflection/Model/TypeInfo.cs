using System.Collections.Generic;
using Cake.Web.Docs.Identity;
using Mono.Cecil;

namespace Cake.Web.Docs.Reflection.Model
{
    internal sealed class TypeInfo : ITypeInfo
    {
        private readonly TypeDefinition _type;
        private readonly IDocumentationMetadata _metadata;
        private readonly List<IMethodInfo> _methods;
        private readonly List<IPropertyInfo> _properties;
        private readonly List<IFieldInfo> _fields;
        private readonly string _identity;

        public string Identity
        {
            get { return _identity; }
        }

        public IDocumentationMetadata Metadata
        {
            get { return _metadata; }
        }

        public TypeDefinition Definition
        {
            get { return _type; }
        }

        public IReadOnlyList<IMethodInfo> Methods
        {
            get { return _methods; }
        }

        public IReadOnlyList<IPropertyInfo> Properties
        {
            get { return _properties; }
        }

        public IReadOnlyList<IFieldInfo> Fields
        {
            get { return _fields; }
        }

        public TypeInfo(
            TypeDefinition type,
            IDocumentationMetadata metadata,
            IEnumerable<IMethodInfo> methods, 
            IEnumerable<IPropertyInfo> properties,
            IEnumerable<IFieldInfo> fields)
        {
            _type = type;
            _metadata = metadata;
            _methods = new List<IMethodInfo>(methods);
            _properties = new List<IPropertyInfo>(properties);
            _fields = new List<IFieldInfo>(fields);
            _identity = CRefGenerator.GetTypeCRef(type);
        }
    }
}