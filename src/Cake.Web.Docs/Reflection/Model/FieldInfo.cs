using Cake.Web.Docs.Identity;
using Mono.Cecil;

namespace Cake.Web.Docs.Reflection.Model
{
    internal sealed class FieldInfo : IFieldInfo
    {
        public string Identity { get; }
        public IDocumentationMetadata Metadata { get; }
        public FieldDefinition Definition { get; }

        public FieldInfo(FieldDefinition definition, IDocumentationMetadata metadata)
        {
            Definition = definition;
            Metadata = metadata;
            Identity = CRefGenerator.GetFieldCRef(definition);
        }
    }
}
