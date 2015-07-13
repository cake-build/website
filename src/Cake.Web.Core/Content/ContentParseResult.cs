using System.Collections.Generic;

namespace Cake.Web.Core.Content
{
    public sealed class ContentParseResult
    {
        private readonly IDictionary<string, string> _frontMatter;
        private readonly string _body;
        private readonly string _excerpt;

        public IDictionary<string, string> FrontMatter
        {
            get { return _frontMatter; }
        }

        public string Body
        {
            get { return _body; }
        }

        public string Excerpt
        {
            get { return _excerpt; }
        }

        public ContentParseResult(IDictionary<string, string> frontMatter, string body)
            : this(frontMatter, body, null)
        {
        }

        public ContentParseResult(IDictionary<string, string> frontMatter, string body, string excerpt)
        {
            _frontMatter = frontMatter;
            _body = body;
            _excerpt = excerpt ?? string.Empty;
        }

        public string GetFrontMatter(string key)
        {
            if (_frontMatter.ContainsKey(key))
            {
                return _frontMatter[key];
            }
            return null;
        }
    }
}