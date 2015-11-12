using System;
using System.Collections.Generic;
using Cake.Web.Core.Content.Blog;

namespace Cake.Web.Models
{
    public abstract class BlogViewModel
    {
        public IReadOnlyList<BlogCategory> Categories { get; }
        public IReadOnlyList<DateTime> Archive { get; }

        protected BlogViewModel(
            IReadOnlyList<BlogCategory> categories,
            IReadOnlyList<DateTime> archive)
        {
            Categories = categories;
            Archive = archive;
        }
    }
}