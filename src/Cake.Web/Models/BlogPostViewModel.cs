using System;
using System.Collections.Generic;
using Cake.Web.Core.Content.Blog;

namespace Cake.Web.Models
{
    public sealed class BlogPostViewModel : BlogViewModel
    {
        private readonly BlogPost _post;

        public BlogPost Post
        {
            get { return _post; }
        }

        public BlogPostViewModel(
            BlogPost post,
            IReadOnlyList<BlogCategory> categories,
            IReadOnlyList<DateTime> archive) : base(categories, archive)
        {
            _post = post;
        }
    }
}