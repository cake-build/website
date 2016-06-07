using MarkdownDeep;

namespace Cake.Web.Core.Content
{
    internal sealed class ContentConverter
    {
        private readonly Markdown _markdown;

        public ContentConverter()
        {
            _markdown = new Markdown();
        }

        public string ConvertToHtml(ContentParseResult result, string content)
        {
            if (!string.IsNullOrWhiteSpace(content))
            {
                if (result.FrontMatter.ContainsKey("content-type"))
                {
                    switch (result.FrontMatter["content-type"])
                    {
                        case "html":
                        return content;
                        case "markdown":
                        return _markdown.Transform(content);
                    }
                }
            }
            return _markdown.Transform(content);
        }
    }
}
