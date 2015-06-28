using System.Diagnostics;
using System.Xml;
using Cake.Web.Docs.Comments;

namespace Cake.Web.Docs.Xml.Parsers
{
    /// <summary>
    /// Responsible for parsing <code>exception</code> comments.
    /// </summary>
    public sealed class ExceptionParser : AggregateParser<ExceptionComment>
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
            return node.Name == "exception";
        }

        /// <summary>
        /// Parses the specified <see cref="XmlNode" />.
        /// </summary>
        /// <param name="parser">The parser.</param>
        /// <param name="node">The node.</param>
        /// <returns>
        /// The parsed comment.
        /// </returns>
        public override ExceptionComment Parse(ICommentParser parser, XmlNode node)
        {
            Debug.Assert(node.Attributes != null, "Node has no attributes.");
            var attribute = node.Attributes["cref"];
            return new ExceptionComment(attribute.InnerText, ParseChildren(parser, node));
        }
    }
}
