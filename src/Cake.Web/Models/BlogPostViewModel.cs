using System;
using System.Collections.Generic;
using Cake.Web.Core.Content.Blog;

namespace Cake.Web.Models
{
    public sealed class BlogPostViewModel : BlogViewModel
    {
        public BlogPost Post { get; }

        public BlogPostViewModel(
            BlogPost post,
            IReadOnlyList<BlogCategory> categories,
            IReadOnlyList<DateTime> archive) : base(categories, archive)
        {
            Post = post;
        }
    }
}