using System.Collections.Generic;
using Mono.Cecil;

namespace Cake.Web.Docs.Reflection.Model
{
    internal sealed class AssemblyInfo : IAssemblyInfo
    {
        public string Identity { get; }
        public AssemblyDefinition Definition { get; }
        public IDocumentationMetadata Metadata { get; }
        public IReadOnlyList<ITypeInfo> Types { get; }
        public string Name { get; }

        public AssemblyInfo(
            AssemblyDefinition definition,
            IEnumerable<ITypeInfo> types,
            IDocumentationMetadata metadata)
        {
            Definition = definition;
            Metadata = metadata;
            Name = definition.Name.Name;
            Identity = definition.FullName;
            Types = new List<ITypeInfo>(types);
        }
    }
}
