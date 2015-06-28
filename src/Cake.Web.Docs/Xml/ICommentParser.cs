using System.Xml;
using Cake.Web.Docs.Comments;

namespace Cake.Web.Docs.Xml
{
    /// <summary>
    /// Represents a comment parser.
    /// </summary>
    public interface ICommentParser
    {
        /// <summary>
        /// Parses the specified node.
        /// </summary>
        /// <param name="node">The node.</param>
        /// <returns>The parsed comment.</returns>
        IComment Parse(XmlNode node);
    }
}
