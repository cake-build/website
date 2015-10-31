using System.Diagnostics;
using System.Xml;
using Cake.Web.Docs.Comments;

namespace Cake.Web.Docs.Xml.Parsers
{
    /// <summary>
    /// Responsible for parsing <code>see</code> comments with a cref attribute.
    /// </summary>
    public sealed class SeeParser : CommentNodeParser<SeeComment>
    {
        /// <summary>
        /// Determines whether this instance can parse the specified node.
        /// </summary>
        /// <param name="node">The node.</param>
        /// <returns>
        ///   <c>true</c> if this instance can parse the specified node; otherwise <c>false</c>.
        /// </returns>
        public override bool CanParse(XmlNode node)
        {
            return node != null && node.Name == "see" && node.Attributes?["cref"] != null;
        }

        /// <summary>
        /// Parses the specified <see cref="XmlNode" />.
        /// </summary>
        /// <param name="parser">The parser.</param>
        /// <param name="node">The node.</param>
        /// <returns>
        /// The parsed comment.
        /// </returns>
        public override SeeComment Parse(ICommentParser parser, XmlNode node)
        {
            Debug.Assert(node.Attributes != null, "Node has no attributes.");
            var attribute = node.Attributes["cref"];
            return attribute != null ? new SeeComment(attribute.InnerText) : null;
        }
    }

    /// <summary>
    /// Responsible for parsing <code>see</code> comments with a href attribute.
    /// </summary>
    public sealed class SeeExternalLinkParser : CommentNodeParser<SeeExternalLinkComment>
    {
        /// <summary>
        /// Determines whether this instance can parse the specified node.
        /// </summary>
        /// <param name="node">The node.</param>
        /// <returns>
        ///   <c>true</c> if this instance can parse the specified node; otherwise <c>false</c>.
        /// </returns>
        public override bool CanParse(XmlNode node)
        {
            return node != null && node.Name == "see" && node.Attributes?["href"] != null;
        }

        /// <summary>
        /// Parses the specified <see cref="XmlNode" />.
        /// </summary>
        /// <param name="parser">The parser.</param>
        /// <param name="node">The node.</param>
        /// <returns>
        /// The parsed comment.
        /// </returns>
        public override SeeExternalLinkComment Parse(ICommentParser parser, XmlNode node)
        {
            Debug.Assert(node.Attributes != null, "Node has no attributes.");
            var attribute = node.Attributes["href"];
            var link = attribute?.InnerText;
            var text = node.InnerText;
            text = string.IsNullOrWhiteSpace(text) ? link : text;
            return new SeeExternalLinkComment(link, text);
        }
    }
}
