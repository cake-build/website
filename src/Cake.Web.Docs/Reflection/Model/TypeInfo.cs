using System.Collections.Generic;
using Cake.Web.Docs.Identity;
using Mono.Cecil;

namespace Cake.Web.Docs.Reflection.Model
{
    internal sealed class TypeInfo : ITypeInfo
    {
        public string Identity { get; }
        public IDocumentationMetadata Metadata { get; }
        public TypeDefinition Definition { get; }
        public IReadOnlyList<IMethodInfo> Methods { get; }
        public IReadOnlyList<IPropertyInfo> Properties { get; }
        public IReadOnlyList<IFieldInfo> Fields { get; }

        public TypeInfo(
            TypeDefinition type,
            IDocumentationMetadata metadata,
            IEnumerable<IMethodInfo> methods,
            IEnumerable<IPropertyInfo> properties,
            IEnumerable<IFieldInfo> fields)
        {
            Definition = type;
            Metadata = metadata;
            Methods = new List<IMethodInfo>(methods);
            Properties = new List<IPropertyInfo>(properties);
            Fields = new List<IFieldInfo>(fields);
            Identity = CRefGenerator.GetTypeCRef(type);
        }
    }
}