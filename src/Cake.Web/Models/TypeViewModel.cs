using System.Collections.Generic;
using System.Linq;
using Cake.Web.Docs;
using Cake.Web.Docs.Reflection;

namespace Cake.Web.Models
{
    public sealed class TypeViewModel
    {
        private readonly DocumentedType _data;
        private readonly DocumentedNamespace _namespace;
        private readonly List<DocumentedMethod> _constructors;
        private readonly List<DocumentedMethod> _methods;
        private readonly List<DocumentedMethod> _extensionMethods;
        private readonly List<DocumentedMethod> _operators;
        private readonly List<DocumentedProperty> _properties;
        private readonly List<DocumentedField> _fields;

        public DocumentedType Data
        {
            get { return _data; }
        }

        public DocumentedNamespace Namespace
        {
            get { return _namespace; }
        }

        public IReadOnlyList<DocumentedMethod> Constructors
        {
            get { return _constructors; }
        }

        public IReadOnlyList<DocumentedMethod> Methods
        {
            get { return _methods; }
        }

        public List<DocumentedMethod> ExtensionMethods
        {
            get { return _extensionMethods; }
        }

        public List<DocumentedMethod> Operators
        {
            get { return _operators; }
        }

        public List<DocumentedProperty> Properties
        {
            get { return _properties; }
        }

        public List<DocumentedField> Fields
        {
            get { return _fields; }
        }

        public bool IsEnum
        {
            get { return _data.TypeClassification == TypeClassification.Enum; }
        }

        public TypeViewModel(DocumentedType data)
        {
            _data = data;
            _namespace = data.Namespace;
            _constructors = new List<DocumentedMethod>(data.Constructors);
            _methods = new List<DocumentedMethod>(data.Methods);
            _extensionMethods = new List<DocumentedMethod>(data.ExtensionMethods);
            _operators = new List<DocumentedMethod>(data.Operators);
            _properties = new List<DocumentedProperty>(GetProperties(data));
            _fields = new List<DocumentedField>(data.Fields);
        }

        private static IEnumerable<DocumentedProperty> GetProperties(DocumentedType type)
        {
            return type.Properties.Where(p => !p.Definition.GetMethod.IsExplicitlyImplemented());
        }
    }
}