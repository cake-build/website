using System.Collections.Generic;

namespace Cake.Web.Core.Content
{
    public sealed class ContentParseResult
    {
        public IDictionary<string, string> FrontMatter { get; }
        public string Body { get; }
        public string Excerpt { get; }

        public ContentParseResult(IDictionary<string, string> frontMatter, string body)
            : this(frontMatter, body, null)
        {
        }

        public ContentParseResult(IDictionary<string, string> frontMatter, string body, string excerpt)
        {
            FrontMatter = frontMatter;
            Body = body;
            Excerpt = excerpt ?? string.Empty;
        }

        public string GetFrontMatter(string key)
        {
            return FrontMatter.ContainsKey(key)
                ? FrontMatter[key] : null;
        }
    }
}