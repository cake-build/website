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
        public IReadOnlyList<AggregatedTypeMemberViewModel<DocumentedMethod>> Methods { get; }
        public List<DocumentedMethod> ExtensionMethods { get; }
        public List<DocumentedMethod> Operators { get; }
        public List<AggregatedTypeMemberViewModel<DocumentedProperty>> Properties { get; }
        public List<DocumentedField> Fields { get; }

        public bool IsEnum => Data.TypeClassification == TypeClassification.Enum;

        public sealed class AggregatedTypeMemberViewModel<T>
        {
            public T Data { get; set; }
            public DocumentedType Owner { get; set; }
        }

        public TypeViewModel(DocumentedType data)
        {
            Data = data;
            Namespace = data.Namespace;
            Constructors = new List<DocumentedMethod>(data.Constructors);
            Methods = new List<AggregatedTypeMemberViewModel<DocumentedMethod>>(GetMethods(data));
            ExtensionMethods = new List<DocumentedMethod>(data.ExtensionMethods);
            Operators = new List<DocumentedMethod>(data.Operators);
            Properties = new List<AggregatedTypeMemberViewModel<DocumentedProperty>>(GetProperties(data));
            Fields = new List<DocumentedField>(data.Fields);
        }

        private static IEnumerable<AggregatedTypeMemberViewModel<DocumentedMethod>> GetMethods(DocumentedType type)
        {
            var current = type;
            while (current != null)
            {
                foreach (var method in current.Methods)
                {
                    yield return new AggregatedTypeMemberViewModel<DocumentedMethod>()
                    {
                        Data = method,
                        Owner = current
                    };
                }
                current = current.BaseType;
            }
        }

        private static IEnumerable<AggregatedTypeMemberViewModel<DocumentedProperty>> GetProperties(DocumentedType type)
        {
            var current = type;
            while (current != null)
            {
                foreach (var property in current.Properties)
                {
                    if (!property.Definition.GetMethod.IsExplicitlyImplemented())
                    {
                        yield return new AggregatedTypeMemberViewModel<DocumentedProperty>()
                        {
                            Data = property,
                            Owner = current
                        };
                    }
                }
                current = current.BaseType;
            }
        }
    }
}