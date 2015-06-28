using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using Cake.Web.Docs.Comments;
using Cake.Web.Docs.Reflection;
using Cake.Web.Docs.Reflection.Model;
using Mono.Cecil;

namespace Cake.Web.Docs
{
    /// <summary>
    /// Represents a documented type.
    /// </summary>
    [DebuggerDisplay("{Identity,nq}")]
    public sealed class DocumentedType : DocumentedMember
    {
        private readonly TypeDefinition _definition;
        private readonly TypeClassification _typeClassification;
        private readonly string _identity;

        private readonly List<DocumentedMethod> _constructors;
        private readonly List<DocumentedMethod> _methods;
        private readonly List<DocumentedMethod> _extensionMethods;
        private readonly List<DocumentedMethod> _operators;
        private readonly List<DocumentedProperty> _properties;
        private readonly List<DocumentedField> _fields;

        /// <summary>
        /// Gets the type's identity.
        /// </summary>
        /// <value>The type's identity.</value>
        public string Identity
        {
            get { return _identity; }
        }

        /// <summary>
        /// Gets the namespace.
        /// </summary>
        /// <value>The namespace.</value>
        public DocumentedNamespace Namespace { get; internal set; }

        /// <summary>
        /// Gets the type definition.
        /// </summary>
        /// <value>The type definition.</value>
        public TypeDefinition Definition
        {
            get { return _definition; }
        }

        /// <summary>
        /// Gets the type's constructors.
        /// </summary>
        /// <value>The type's constructors.</value>
        public IReadOnlyList<DocumentedMethod> Constructors
        {
            get { return _constructors; }
        }

        /// <summary>
        /// Gets the type's methods.
        /// </summary>
        /// <value>The type's methods.</value>
        public IReadOnlyList<DocumentedMethod> Methods
        {
            get { return _methods; }
        }

        /// <summary>
        /// Gets the type's extension methods.
        /// </summary>
        /// <value>The type's extension methods.</value>
        public IReadOnlyList<DocumentedMethod> ExtensionMethods
        {
            get { return _extensionMethods; }
        }

        /// <summary>
        /// Gets the type's operators.
        /// </summary>
        /// <value>The type's operators.</value>
        public IReadOnlyList<DocumentedMethod> Operators
        {
            get { return _operators; }
        }

        /// <summary>
        /// Gets the type's properties.
        /// </summary>
        /// <value>The type's properties.</value>
        public IReadOnlyList<DocumentedProperty> Properties
        {
            get { return _properties; }
        }

        /// <summary>
        /// Gets the type's fields.
        /// </summary>
        /// <value>The type's fields.</value>
        public IReadOnlyList<DocumentedField> Fields
        {
            get { return _fields; }
        }

        /// <summary>
        /// Gets the type classification.
        /// </summary>
        /// <value>The type classification.</value>
        public TypeClassification TypeClassification
        {
            get { return _typeClassification; }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="DocumentedType" /> class.
        /// </summary>
        /// <param name="info">The type information.</param>
        /// <param name="properties">The type's properties.</param>
        /// <param name="methods">The type's methods.</param>
        /// <param name="fields">The type's fields.</param>
        /// <param name="summary">The summary.</param>
        /// <param name="remarks">The remarks.</param>
        /// <param name="example">The example.</param>
        public DocumentedType(
            ITypeInfo info,
            IEnumerable<DocumentedProperty> properties,
            IEnumerable<DocumentedMethod> methods,
            IEnumerable<DocumentedField> fields,
            SummaryComment summary,
            RemarksComment remarks,
            ExampleComment example)
            : base(MemberClassification.Type, summary, remarks, example)
        {
            _definition = info.Definition;
            _typeClassification = info.Definition.GetTypeClassification();
            _identity = info.Identity;
            _properties = new List<DocumentedProperty>(properties);
            _fields = new List<DocumentedField>(fields);

            // Materialize all methods.
            var documentedMethods = methods as DocumentedMethod[] ?? methods.ToArray();

            _constructors = new List<DocumentedMethod>(GetConstructors(documentedMethods));
            _methods = new List<DocumentedMethod>(GetMethods(documentedMethods));
            _extensionMethods = new List<DocumentedMethod>();
            _operators = new List<DocumentedMethod>(GetOperators(documentedMethods));
        }

        internal void SetExtensionMethods(IEnumerable<DocumentedMethod> methods)
        {
            var methodArray = methods as DocumentedMethod[] ?? methods.ToArray();
            if (methods != null && methodArray.Length > 0)
            {
                _extensionMethods.Clear();
                _extensionMethods.AddRange(methodArray);
            }
        }

        private static IEnumerable<DocumentedMethod> GetConstructors(IEnumerable<DocumentedMethod> methods)
        {
            return methods.Where(m => m.MethodClassification == MethodClassification.Constructor)
                .Where(x => !(x.Summary == null && x.Parameters.Count == 0));
        }

        private static IEnumerable<DocumentedMethod> GetMethods(IEnumerable<DocumentedMethod> methods)
        {
            return methods.Where(x => x.MethodClassification == MethodClassification.Method ||
                x.MethodClassification == MethodClassification.ExtensionMethod);
        }

        private static IEnumerable<DocumentedMethod> GetOperators(IEnumerable<DocumentedMethod> methods)
        {
            return methods.Where(x => x.MethodClassification == MethodClassification.Operator);
        }
    }
}