using System;
using System.Collections.Generic;

namespace Cake.Web.Core.Content.Blog
{
    public sealed class BlogPost
    {
        private readonly string _id;
        private readonly string _slug;
        private readonly string _title;
        private readonly string _body;
        private readonly string _feedBody;
        private readonly bool _isLatest;
        private readonly string _excerpt;
        private readonly DateTime _postedAt;
        private readonly IReadOnlyList<BlogCategory> _categories;

        public string Id
        {
            get { return _id; }
        }

        public string Slug
        {
            get { return _slug; }
        }

        public string Title
        {
            get { return _title; }
        }

        public string Body
        {
            get { return _body; }
        }

        public string FeedBody
        {
            get { return _feedBody; }
        }

        public string Excerpt
        {
            get { return _excerpt; }
        }

        public DateTime PostedAt
        {
            get { return _postedAt; }
        }

        public bool IsLatest { get; internal set; }

        public bool HasExcept
        {
            get { return !string.IsNullOrWhiteSpace(Excerpt); }
        }

        public IReadOnlyList<BlogCategory> Categories
        {
            get { return _categories; }
        }

        public BlogPost(string slug, string title, string body, string feedBody, 
            string excerpt, DateTime postedAt, IEnumerable<BlogCategory> categories)
        {
            _id = GetId(postedAt.Year, postedAt.Month, slug);
            _slug = slug;
            _title = title;
            _body = body;
            _feedBody = feedBody;
            _excerpt = excerpt;
            _postedAt = postedAt;
            _categories = new List<BlogCategory>(categories);
        }

        public static string GetId(int year, int month, string slug)
        {
            return string.Concat(year, "/", month, "/", slug);
        }
    }
}
