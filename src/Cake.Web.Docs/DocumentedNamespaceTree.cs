using System;
using System.Collections.Generic;
using System.Linq;

namespace Cake.Web.Docs
{
    /// <summary>
    /// Represents a namespace tree.
    /// </summary>
    public sealed class DocumentedNamespaceTree
    {
        private readonly DocumentedNamespace _namespace;
        private readonly List<DocumentedNamespaceTree> _children;
        private DocumentedNamespaceTree _parent;

        /// <summary>
        /// Gets the namespace.
        /// </summary>
        /// <value>The namespace.</value>
        public DocumentedNamespace Namespace
        {
            get { return _namespace; }
        }

        /// <summary>
        /// Gets the parent namespace tree node.
        /// </summary>
        /// <value>The parent.</value>
        public DocumentedNamespaceTree Parent
        {
            get { return _parent; }
        }

        /// <summary>
        /// Gets the child namespace tree nodes.
        /// </summary>
        /// <value>
        /// The children.
        /// </value>
        public List<DocumentedNamespaceTree> Children
        {
            get { return _children; }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="DocumentedNamespaceTree"/> class.
        /// </summary>
        /// <param name="namespace">The namespace.</param>
        public DocumentedNamespaceTree(DocumentedNamespace @namespace)
        {
            _namespace = @namespace;
            _children = new List<DocumentedNamespaceTree>();
        }

        internal void AddChild(DocumentedNamespaceTree child)
        {
            if (child.SetParent(this))
            {
                _children.Add(child);
            }
        }

        internal bool SetParent(DocumentedNamespaceTree parent)
        {
            if (_parent != null)
            {
                return false;
            }
            _parent = parent;
            return true;
        }

        internal static Dictionary<string, DocumentedNamespaceTree> Build(IEnumerable<DocumentedNamespace> @namespaces)
        {
            var result = new HashSet<DocumentedNamespaceTree>();
            foreach (var ns in namespaces)
            {
                result.Add(new DocumentedNamespaceTree(ns));
            }
            foreach (var tree in result)
            {
                var parts = tree.Namespace.Identity.Split(new[] { '.' }, StringSplitOptions.RemoveEmptyEntries);
                var prefix = string.Join(".", parts.Take(parts.Length - 1));
                foreach (var other in result)
                {
                    if (other.Namespace.Identity == prefix)
                    {
                        other.AddChild(tree);
                    }
                }
            }
            return result.ToDictionary(x => x.Namespace.Identity, x => x, StringComparer.Ordinal);
        }
    }
}
