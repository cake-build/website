using System.Xml;
using Cake.Web.Docs.Comments;

namespace Cake.Web.Docs.Xml.Parsers
{
    /// <summary>
    /// Represents a whitespace parser.
    /// </summary>
    public sealed class WhitespaceParser : CommentNodeParser<WhitespaceComment>
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
            return node is XmlWhitespace;
        }

        /// <summary>
        /// Parses the specified <see cref="XmlNode" />.
        /// </summary>
        /// <param name="parser">The parser.</param>
        /// <param name="node">The node.</param>
        /// <returns>
        /// The parsed comment.
        /// </returns>
        public override WhitespaceComment Parse(ICommentParser parser, XmlNode node)
        {
            return new WhitespaceComment(node.InnerText);
        }
    }
}
