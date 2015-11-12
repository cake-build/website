using System;
using System.Collections.Generic;

namespace Cake.Web.Core.Content.Blog
{
    public sealed class BlogPost
    {
        public string Id { get; }
        public string Slug { get; }
        public string Title { get; }
        public string Body { get; }
        public string FeedBody { get; }
        public string Excerpt { get; }
        public DateTime PostedAt { get; }
        public IReadOnlyList<BlogCategory> Categories { get; }

        public bool IsLatest { get; internal set; }
        public bool HasExcept => !string.IsNullOrWhiteSpace(Excerpt);

        public BlogPost(string slug, string title, string body, string feedBody,
            string excerpt, DateTime postedAt, IEnumerable<BlogCategory> categories)
        {
            Id = GetId(postedAt.Year, postedAt.Month, slug);
            Slug = slug;
            Title = title;
            Body = body;
            FeedBody = feedBody;
            Excerpt = excerpt;
            PostedAt = postedAt;
            Categories = new List<BlogCategory>(categories);
        }

        public static string GetId(int year, int month, string slug)
        {
            return string.Concat(year, "/", month, "/", slug);
        }
    }
}
