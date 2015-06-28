using System.Xml;
using Cake.Web.Docs.Comments;

namespace Cake.Web.Docs.Xml.Parsers
{
    /// <summary>
    /// Responsible for parsing inline text comments.
    /// </summary>
    public sealed class InlineTextParser : CommentNodeParser<InlineTextComment>
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
            return node is XmlText;
        }

        /// <summary>
        /// Parses the specified <see cref="XmlNode" />.
        /// </summary>
        /// <param name="parser">The parser.</param>
        /// <param name="node">The node.</param>
        /// <returns>
        /// The parsed comment.
        /// </returns>
        public override InlineTextComment Parse(ICommentParser parser, XmlNode node)
        {
            return new InlineTextComment(node.InnerText);
        }
    }
}
