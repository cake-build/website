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
        private readonly List<DocumentedMethod> _extensionMethods;

        /// <summary>
        /// Gets the type's identity.
        /// </summary>
        /// <value>The type's identity.</value>
        public string Identity { get; }

        /// <summary>
        /// Gets the namespace.
        /// </summary>
        /// <value>The namespace.</value>
        public DocumentedNamespace Namespace { get; internal set; }

        /// <summary>
        /// Gets the type definition.
        /// </summary>
        /// <value>The type definition.</value>
        public TypeDefinition Definition { get; }

        /// <summary>
        /// Gets the type's constructors.
        /// </summary>
        /// <value>The type's constructors.</value>
        public IReadOnlyList<DocumentedMethod> Constructors { get; }

        /// <summary>
        /// Gets the type's methods.
        /// </summary>
        /// <value>The type's methods.</value>
        public IReadOnlyList<DocumentedMethod> Methods { get; }

        /// <summary>
        /// Gets the type's extension methods.
        /// </summary>
        /// <value>The type's extension methods.</value>
        public IReadOnlyList<DocumentedMethod> ExtensionMethods => _extensionMethods;

        /// <summary>
        /// Gets the type's operators.
        /// </summary>
        /// <value>The type's operators.</value>
        public IReadOnlyList<DocumentedMethod> Operators { get; }

        /// <summary>
        /// Gets the type's properties.
        /// </summary>
        /// <value>The type's properties.</value>
        public IReadOnlyList<DocumentedProperty> Properties { get; }

        /// <summary>
        /// Gets the type's fields.
        /// </summary>
        /// <value>The type's fields.</value>
        public IReadOnlyList<DocumentedField> Fields { get; }

        /// <summary>
        /// Gets the type classification.
        /// </summary>
        /// <value>The type classification.</value>
        public TypeClassification TypeClassification { get; }

        /// <summary>
        /// Initializes a new instance of the <see cref="DocumentedType" /> class.
        /// </summary>
        /// <param name="info">The type information.</param>
        /// <param name="properties">The type's properties.</param>
        /// <param name="methods">The type's methods.</param>
        /// <param name="fields">The type's fields.</param>
        /// <param name="summary">The summary.</param>
        /// <param name="remarks">The remarks.</param>
        /// <param name="examples">The examples.</param>
        /// <param name="metadata">The type metadata.</param>
        public DocumentedType(
            ITypeInfo info,
            IEnumerable<DocumentedProperty> properties,
            IEnumerable<DocumentedMethod> methods,
            IEnumerable<DocumentedField> fields,
            SummaryComment summary,
            RemarksComment remarks,
            IEnumerable<ExampleComment> examples,
            IDocumentationMetadata metadata)
            : base(MemberClassification.Type, summary, remarks, examples, metadata)
        {
            Definition = info.Definition;
            TypeClassification = info.Definition.GetTypeClassification();
            Identity = info.Identity;
            Properties = new List<DocumentedProperty>(properties);
            Fields = new List<DocumentedField>(fields);

            // Materialize all methods.
            var documentedMethods = methods as DocumentedMethod[] ?? methods.ToArray();

            Constructors = new List<DocumentedMethod>(GetConstructors(documentedMethods));
            Methods = new List<DocumentedMethod>(GetMethods(documentedMethods));
            Operators = new List<DocumentedMethod>(GetOperators(documentedMethods));

            _extensionMethods = new List<DocumentedMethod>();
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