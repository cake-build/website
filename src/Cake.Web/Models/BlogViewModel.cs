using System;
using System.Collections.Generic;
using Cake.Web.Core.Content.Blog;

namespace Cake.Web.Models
{
    public abstract class BlogViewModel
    {
        private readonly IReadOnlyList<BlogCategory> _categories;
        private readonly IReadOnlyList<DateTime> _archive;

        public IReadOnlyList<BlogCategory> Categories
        {
            get { return _categories; }
        }

        public IReadOnlyList<DateTime> Archive
        {
            get { return _archive; }
        }

        protected BlogViewModel(
            IReadOnlyList<BlogCategory> categories,
            IReadOnlyList<DateTime> archive)
        {
            _categories = categories;
            _archive = archive;
        }
    }
}