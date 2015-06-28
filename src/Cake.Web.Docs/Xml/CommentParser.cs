using System.Collections.Generic;
using System.Xml;
using Cake.Web.Docs.Comments;
using Cake.Web.Docs.Xml.Parsers;

namespace Cake.Web.Docs.Xml
{
    /// <summary>
    /// Responsible for parsing XML comments.
    /// </summary>
    public sealed class CommentParser : ICommentParser
    {
        private readonly List<ICommentNodeParser> _parsers;

        /// <summary>
        /// Initializes a new instance of the <see cref="CommentParser"/> class.
        /// </summary>
        public CommentParser()
        {
            _parsers = new List<ICommentNodeParser>
            {
                new CodeParser(),
                new ExampleParser(),
                new ExceptionParser(),
                new InlineCodeParser(),
                new InlineTextParser(),
                new ParamParser(),
                new ParamRefParser(),
                new ParaParser(),
                new PermissionParser(),
                new RemarksParser(),
                new ReturnsParser(),
                new SeeAlsoParser(),
                new SeeParser(),
                new SummaryParser(),
                new TypeParamParser(),
                new TypeParamRefParser(),
                new ValueParser(),
                new WhitespaceParser()
            };
        }

        /// <summary>
        /// Parses the specified node.
        /// </summary>
        /// <param name="node">The node.</param>
        /// <returns>
        /// The parsed comment.
        /// </returns>
        public IComment Parse(XmlNode node)
        {
            foreach (var parser in _parsers)
            {
                if (parser.CanParse(node))
                {
                    return parser.Parse(this, node);
                }
            }
            return null;
        }
    }
}
