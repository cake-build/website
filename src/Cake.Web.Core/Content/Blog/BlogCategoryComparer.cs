using System;
using System.Collections.Generic;

namespace Cake.Web.Core.Content.Blog
{
    public sealed class BlogCategoryComparer : IEqualityComparer<BlogCategory>
    {
        public bool Equals(BlogCategory x, BlogCategory y)
        {
            if (x == null && y == null)
            {
                return true;
            }
            if (x == null || y == null)
            {
                return false;
            }
            return x.Slug.Equals(y.Slug, StringComparison.OrdinalIgnoreCase);
        }

        public int GetHashCode(BlogCategory obj)
        {
            return obj.Slug.GetHashCode();
        }
    }
}