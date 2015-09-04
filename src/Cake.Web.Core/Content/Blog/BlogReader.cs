using System;
using System.Collections.Generic;
using Cake.Core.IO;
using System.Linq;
using HtmlAgilityPack;

namespace Cake.Web.Core.Content.Blog
{
    internal sealed class BlogReader : IBlogReader
    {
        private readonly IFileSystem _fileSystem;
        private readonly ContentParser _parser;
        private readonly ContentConverter _converter;
        private readonly ContentProcessor _processor;

        public BlogReader(IFileSystem fileSystem, 
            ContentParser parser,
            ContentConverter converter,
            ContentProcessor processor)
        {
            _fileSystem = fileSystem;
            _parser = parser;
            _converter = converter;
            _processor = processor;
        }

        public BlogIndex Parse(DirectoryPath path)
        {
            var directory = _fileSystem.GetDirectory(path);
            if (!directory.Exists)
            {
                return new BlogIndex(Enumerable.Empty<BlogPost>());
            }

            var posts = new List<BlogPost>();
            var files = directory.GetFiles("*", SearchScope.Current);
            foreach (var file in files)
            {
                var filename = BlogFilename.Parse(file.Path);
                if (filename != null)
                {
                    // Read the file.
                    var content = _parser.Parse(file.Path);
                    if (!content.FrontMatter.ContainsKey("content-type"))
                    {
                        content.FrontMatter.Add("content-type", "markdown");
                    }

                    // Process the content.
                    var body = _processor.PreProcess(content.Body);
                    body = _converter.ConvertToHtml(content, body);
                    body = _processor.PostProcess(body) ?? body;

                    // Get the excerpts.
                    var excerpts = _converter.ConvertToHtml(content, content.Excerpt);

                    // Rewrite the feed body.
                    // This is kind of a hack, but we need to make sure that all links are absolute.
                    var feedBody = RewriteRelativeLinks(body);

                    // Add the blog post.
                    posts.Add(new BlogPost(filename.Slug,
                        content.GetFrontMatter("title"), body, feedBody, excerpts, filename.PostedAt,
                        GetCategories(content)));
                }
            }

            return new BlogIndex(posts);
        }

        private static IEnumerable<BlogCategory> GetCategories(ContentParseResult content)
        {
            var result = new List<BlogCategory>();
            var categories = content.GetFrontMatter("category");
            if (categories != null)
            {
                var parts = categories.Split(new[] {','}, StringSplitOptions.RemoveEmptyEntries);
                foreach (var part in parts)
                {
                    var title = part.Trim();
                    var slug = title;

                    slug = slug.Replace("C#", "csharp");
                    slug = slug.Replace("c#", "csharp");
                    slug = slug.Replace(".net", "dotnet");
                    slug = slug.Replace(".NET", "dotnet");
                    slug = slug.Replace("C++", "cpp");
                    slug = slug.Replace("c++", "cpp");
                    slug = slug.ToSlug();

                    result.Add(new BlogCategory(slug, title));
                }
            }
            return result;
        }

        private static string RewriteRelativeLinks(string content)
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
                    var url = linkNode.GetAttributeValue("href", null);
                    if (url != null)
                    {
                        if (url.StartsWith("/"))
                        {
                            url = string.Concat("http://cakebuild.net", url);
                        }
                        linkNode.SetAttributeValue("href", url);
                    }
                }
            }

            return document.DocumentNode.OuterHtml;
        }
    }
}
