using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Xml;
using Cake.Web.Docs.Comments;
using Cake.Web.Docs.Xml.Model;

namespace Cake.Web.Docs.Xml
{
    /// <summary>
    /// Responsible for parsing XML documentation.
    /// </summary>
    internal sealed class XmlDocumentationParser
    {
        private readonly ICommentParser _parser;

        /// <summary>
        /// Initializes a new instance of the <see cref="XmlDocumentationParser"/> class.
        /// </summary>
        public XmlDocumentationParser()
        {
            _parser = new CommentParser();
        }

        /// <summary>
        /// Parses the specified documents.
        /// </summary>
        /// <param name="documents">The documents.</param>
        /// <returns>An XMl documentation model.</returns>
        public XmlDocumentationModel Parse(IEnumerable<XmlDocument> documents)
        {
            if (documents == null)
            {
                throw new ArgumentNullException(nameof(documents));
            }

            var result = new List<XmlDocumentationMember>();
            foreach (var document in documents)
            {
                result.AddRange(Parse(document));
            }
            return new XmlDocumentationModel(result);
        }

        private IEnumerable<XmlDocumentationMember> Parse(XmlDocument document)
        {
            if (document == null)
            {
                throw new ArgumentNullException(nameof(document));
            }

            // Get the assembly Identity.
            var members = new List<XmlDocumentationMember>();

            // Get all members and parse them.
            var nodes = document.SelectNodes("/doc/members/member");
            if (nodes != null)
            {
                foreach (XmlNode node in nodes)
                {
                    Debug.Assert(node != null, "Node cannot be null.");
                    Debug.Assert(node.Attributes != null, "Node has no attributes.");

                    var name = node.Attributes["name"];
                    if (name == null)
                    {
                        throw new InvalidOperationException("Encountered XML member without name.");
                    }

                    var comments = new List<IComment>();
                    foreach (XmlNode commentNode in node.ChildNodes)
                    {
                        var comment = _parser.Parse(commentNode);
                        if (comment != null)
                        {
                            comments.Add(comment);
                        }
                    }

                    members.Add(new XmlDocumentationMember(name.InnerText, comments));
                }
            }

            // Return the result.
            return members;
        }
    }
}
