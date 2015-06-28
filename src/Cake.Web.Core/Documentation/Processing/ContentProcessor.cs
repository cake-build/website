using System;
using System.Linq;
using Cake.Web.Core.Dsl;
using Cake.Web.Docs.Reflection;
using HtmlAgilityPack;

namespace Cake.Web.Core.Documentation.Processing
{
    internal sealed class ContentProcessor
    {
        private readonly IUrlResolver _resolver;
        private readonly DslModel _dslModel;

        public ContentProcessor(IUrlResolver resolver, DslModel dslModel)
        {
            _resolver = resolver;
            _dslModel = dslModel;
        }

        public string Process(string content)
        {
            // Load the document.
            var document = new HtmlDocument();
            document.LoadHtml(content);

            // Find all links in the document.
            var linkQuery = document.DocumentNode.Descendants("a");
            foreach (var linkNode in linkQuery.ToList())
            {
                if (linkNode.HasAttributes)
                {
                    var url = linkNode.GetAttributeValue("href", "#");
                    if (!url.Equals("#", StringComparison.Ordinal))
                    {
                        var parts = url.Split(new[] { "://" }, StringSplitOptions.RemoveEmptyEntries);
                        if (parts.Length == 2)
                        {
                            var protocol = parts[0];
                            if (protocol != null)
                            {
                                if (protocol.Equals("api", StringComparison.OrdinalIgnoreCase))
                                {
                                    // Resolve the API member.
                                    var data = parts[1].TrimStart('/');

                                    // Update the link.
                                    linkNode.SetAttributeValue("href", _resolver.GetUrl(data) ?? url);
                                }
                                else if (protocol.Equals("dsl", StringComparison.OrdinalIgnoreCase))
                                {
                                    // Resolve the DSL member.
                                    var data = parts[1].TrimStart('/');

                                    // Update the link.
                                    // TODO: Centralize resolving of URL.
                                    var category = _dslModel.FindCategory(data);
                                    var link = category != null ? string.Concat("/dsl/", category.Slug) : url;
                                    linkNode.SetAttributeValue("href", link);
                                }
                            }
                        }
                    }
                }
            }

            return document.DocumentNode.OuterHtml;
        }
    }
}