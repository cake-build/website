using System;
using System.Collections.Generic;

namespace Cake.Web.Docs.Xml.Model
{
    /// <summary>
    /// Represents the XML documentation model.
    /// </summary>
    public sealed class XmlDocumentationModel
    {
        private readonly List<XmlDocumentationMember> _members;
        private readonly Dictionary<string, XmlDocumentationMember> _lookup;

        /// <summary>
        /// Gets the assemblies.
        /// </summary>
        /// <value>The assemblies.</value>
        public IReadOnlyList<XmlDocumentationMember> Members
        {
            get { return _members; }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="XmlDocumentationModel" /> class.
        /// </summary>
        /// <param name="members">The XML documentation members.</param>
        internal XmlDocumentationModel(IEnumerable<XmlDocumentationMember> members)
        {
            // Get all members.
            _members = new List<XmlDocumentationMember>(members);

            // Create lookup table for members.
            _lookup = new Dictionary<string, XmlDocumentationMember>(StringComparer.Ordinal);
            foreach (var member in _members)
            {
                if (_lookup.ContainsKey(member.CRef))
                {
                    throw new InvalidOperationException();   
                }                    
                _lookup.Add(member.CRef, member);
            }
        }

        /// <summary>
        /// Finds the member with the specified Identity.
        /// </summary>
        /// <param name="identity">The Identity.</param>
        /// <returns>An <see cref="XmlDocumentationMember"/> or <c>null</c> if not found.</returns>
        public XmlDocumentationMember Find(string identity)
        {
            return _lookup.ContainsKey(identity)
                ? _lookup[identity] : null;
        }
    }
}
