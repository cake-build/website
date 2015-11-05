using System;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using Cake.Web.Core.Dsl;
using Cake.Web.Docs.Reflection;
using HtmlAgilityPack;

namespace Cake.Web.Core.Content
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

        public string PreProcess(string content)
        {
            return RewriteCodeBlock(content);
        }

        public string PostProcess(string content)
        {
            return RewriteLinks(content);
        }

        private static string RewriteCodeBlock(string content)
        {
            const string pattern = @"```([a-z]*)[\s\S]([\s\S]*?\n)```";
            const string replacement = "<pre><code class=\"$1\">$2</code></pre>";
            content = Regex.Replace(content, pattern, replacement, RegexOptions.Compiled | RegexOptions.Multiline);

            // Load the document.
            var document = new HtmlDocument();
            document.LoadHtml(content);
            var query = document.DocumentNode.Descendants("code");
            foreach (var linkNode in query.ToList())
            {
                if (linkNode.HasAttributes)
                {
                    var @class = linkNode.GetAttributeValue("class", null);
                    if (@class != null)
                    {
                        if (string.IsNullOrWhiteSpace(@class) || @class=="language-")
                        {
                            linkNode.Attributes["class"].Value = "nohighlight";
                        }
                        else
                        {
                            if (@class == "bash")
                            {
                                linkNode.SetAttributeValue("class", "sh");
                            }
                            if (@class == "powershell")
                            {
                                linkNode.SetAttributeValue("class", "ps");
                            }
                        }
                    }
                }

                // Trim content of code so it doesn't start with a new line.
                linkNode.InnerHtml = linkNode.InnerHtml.Trim('\r', '\n');
                linkNode.InnerHtml = HttpUtility.HtmlEncode(linkNode.InnerHtml);
            }

            return document.DocumentNode.OuterHtml;
        }

        private string RewriteLinks(string content)
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
                                else if (protocol.Equals("docs", StringComparison.OrdinalIgnoreCase))
                                {
                                    // Resolve the documentation id.
                                    var data = parts[1].TrimStart('/');

                                    // Update the link.
                                    linkNode.SetAttributeValue("href", string.Concat("/docs/", data));
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