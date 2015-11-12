using System.Collections.Generic;
using System.Linq;
using Cake.Web.Docs;
using Cake.Web.Docs.Reflection;

namespace Cake.Web.Models
{
    public sealed class TypeViewModel
    {
        public DocumentedType Data { get; }
        public DocumentedNamespace Namespace { get; }
        public IReadOnlyList<DocumentedMethod> Constructors { get; }
        public IReadOnlyList<DocumentedMethod> Methods { get; }
        public List<DocumentedMethod> ExtensionMethods { get; }
        public List<DocumentedMethod> Operators { get; }
        public List<DocumentedProperty> Properties { get; }
        public List<DocumentedField> Fields { get; }

        public bool IsEnum => Data.TypeClassification == TypeClassification.Enum;

        public TypeViewModel(DocumentedType data)
        {
            Data = data;
            Namespace = data.Namespace;
            Constructors = new List<DocumentedMethod>(data.Constructors);
            Methods = new List<DocumentedMethod>(data.Methods);
            ExtensionMethods = new List<DocumentedMethod>(data.ExtensionMethods);
            Operators = new List<DocumentedMethod>(data.Operators);
            Properties = new List<DocumentedProperty>(GetProperties(data));
            Fields = new List<DocumentedField>(data.Fields);
        }

        private static IEnumerable<DocumentedProperty> GetProperties(DocumentedType type)
        {
            return type.Properties.Where(p => !p.Definition.GetMethod.IsExplicitlyImplemented());
        }
    }
}