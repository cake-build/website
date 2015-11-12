using System;
using System.Collections.Generic;
using System.Diagnostics;
using Cake.Web.Docs.Reflection;
using Cake.Web.Docs.Reflection.Model;
using Mono.Cecil;

namespace Cake.Web.Docs
{
    /// <summary>
    /// Represents a documented assembly.
    /// </summary>
    [DebuggerDisplay("{Identity,nq}")]
    public sealed class DocumentedAssembly : DocumentedMember
    {
        /// <summary>
        /// Gets the identity.
        /// </summary>
        /// <value>The identity.</value>
        public string Identity { get; }

        /// <summary>
        /// Gets the name.
        /// </summary>
        /// <value>The name.</value>
        public string Name { get; }

        /// <summary>
        /// Gets the document model.
        /// </summary>
        /// <value>The document model.</value>
        public DocumentModel Model { get; internal set; }

        /// <summary>
        /// Gets the assembly definition.
        /// </summary>
        /// <value>The assembly definition.</value>
        public AssemblyDefinition Definition { get; }

        /// <summary>
        /// Gets the types in this namespace.
        /// </summary>
        /// <value>The types in this namespace.</value>
        public IReadOnlyList<DocumentedNamespace> Namespaces { get; }

        /// <summary>
        /// Initializes a new instance of the <see cref="DocumentedAssembly"/> class.
        /// </summary>
        /// <param name="info">The assembly information.</param>
        /// <param name="namespaces">The namespaces.</param>
        /// <param name="metadata">The associated metadata.</param>
        public DocumentedAssembly(
            IAssemblyInfo info,
            IEnumerable<DocumentedNamespace> namespaces,
            IDocumentationMetadata metadata)
            : base(MemberClassification.Assembly,  null, null, null, metadata)
        {
            Definition = info.Definition;
            Identity = info.Identity;
            Name = GetAssemblyName(info);
            Namespaces = new List<DocumentedNamespace>(namespaces);
        }

        private static string GetAssemblyName(IAssemblyInfo info)
        {
            var name = info.Identity;
            var index = name.IndexOf(", ", 0, StringComparison.Ordinal);
            if (index != -1)
            {
                name = string.Concat(name.Substring(0, index), ".dll");
            }
            return name;
        }
    }
}
