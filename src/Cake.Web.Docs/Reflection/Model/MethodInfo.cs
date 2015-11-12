using Cake.Web.Docs.Identity;
using Mono.Cecil;

namespace Cake.Web.Docs.Reflection.Model
{
    internal sealed class MethodInfo : IMethodInfo
    {
        public string Identity { get; }
        public IDocumentationMetadata Metadata { get; }
        public MethodDefinition Definition { get; }

        public MethodInfo(MethodDefinition definition, IDocumentationMetadata metadata)
        {
            Definition = definition;
            Metadata = metadata;
            Identity = CRefGenerator.GetMethodCRef(definition);
        }
    }
}
