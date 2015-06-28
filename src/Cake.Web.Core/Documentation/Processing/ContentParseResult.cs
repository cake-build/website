using System.Collections.Generic;

namespace Cake.Web.Core.Documentation.Processing
{
    public sealed class ContentParseResult
    {
        private readonly IDictionary<string, string> _frontMatter;
        private readonly string _content;

        public IDictionary<string, string> FrontMatter
        {
            get { return _frontMatter; }
        }

        public string Content
        {
            get { return _content; }
        }

        public ContentParseResult(IDictionary<string, string> frontMatter, string content)
        {
            _frontMatter = frontMatter;
            _content = content;
        }
    }
}