using System;
using System.Collections.Generic;
using Cake.Web.Core.Content.Blog;

namespace Cake.Web.Models
{
    public sealed class BlogPageViewModel : BlogViewModel
    {
        public int CurrentPage { get; set; }
        public string Category { get; set; }
        public string Author { get; set; }
        public int Year { get; set; }
        public int Month { get; set; }

        public bool HasNewerPosts { get; set; }
        public bool HasOlderPosts { get; set; }

        public IReadOnlyList<BlogPost> Posts { get; }

        public BlogPageViewModel(
            IReadOnlyList<BlogPost> posts,
            IReadOnlyList<BlogCategory> categories,
            IReadOnlyList<DateTime> archive,
            IReadOnlyList<string> authors) : base(categories, archive, authors)
        {
            Posts = posts;
        }
    }
}