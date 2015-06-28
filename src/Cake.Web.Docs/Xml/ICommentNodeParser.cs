using System.Xml;
using Cake.Web.Docs.Comments;

namespace Cake.Web.Docs.Xml
{
    /// <summary>
    /// Represents a comment node parser.
    /// </summary>
    public interface ICommentNodeParser
    {
        /// <summary>
        /// Determines whether this instance can parse the specified node.
        /// </summary>
        /// <param name="node">The node.</param>
        /// <returns><c>true</c> if this instance can parse the specified node; otherwise <c>false</c>.</returns>
        bool CanParse(XmlNode node);

        /// <summary>
        /// Parses the specified <see cref="XmlNode"/>.
        /// </summary>
        /// <param name="parser">The parser.</param>
        /// <param name="node">The node.</param>
        /// <returns>The parsed comment.</returns>
        IComment Parse(ICommentParser parser, XmlNode node);
    }
}