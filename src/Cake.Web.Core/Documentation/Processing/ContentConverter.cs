using MarkdownDeep;

namespace Cake.Web.Core.Documentation.Processing
{
    internal sealed class ContentConverter
    {
        private readonly Markdown _markdown;

        public ContentConverter()
        {
            _markdown = new Markdown();
        }

        public string ConvertToHtml(ContentParseResult result)
        {
            if (result.FrontMatter.ContainsKey("content-type"))
            {
                switch (result.FrontMatter["content-type"])
                {
                    case "html":
                        return result.Content;
                    case "markdown":
                        return _markdown.Transform(result.Content);
                }
            }
            return result.Content;
        }
    }
}
