using System;
using System.Collections.Generic;
using System.Linq;

namespace Cake.Web.Core.Content.Blog
{
    public sealed class BlogIndex
    {
        private readonly List<BlogPost> _posts;
        private readonly List<BlogCategory> _categories;
        private readonly Dictionary<string, List<BlogPost>> _categoryToPostsLookup;
        private readonly Dictionary<int, Dictionary<int, List<BlogPost>>> _yearMonthToPostsLookup;
        private readonly Dictionary<string, BlogPost> _idToPostLookup;
        private readonly List<DateTime> _archive;

        public BlogIndex(IEnumerable<BlogPost> posts)
        {
            _posts = new List<BlogPost>(posts);
            _categoryToPostsLookup = new Dictionary<string, List<BlogPost>>(StringComparer.OrdinalIgnoreCase);
            _yearMonthToPostsLookup = new Dictionary<int, Dictionary<int, List<BlogPost>>>();
            _idToPostLookup = new Dictionary<string, BlogPost>(StringComparer.OrdinalIgnoreCase);
            _archive = new List<DateTime>();
            
            var categories = new HashSet<BlogCategory>(new BlogCategoryComparer());

            foreach (var post in _posts)
            {
                // Create lookup by category.
                foreach (var category in post.Categories)
                {
                    var categorySlug = category.Slug;
                    if (!_categoryToPostsLookup.ContainsKey(categorySlug))
                    {
                        _categoryToPostsLookup.Add(categorySlug, new List<BlogPost>());
                    }

                    _categoryToPostsLookup[categorySlug].Add(post);

                    categories.Add(category);
                }

                // Create lookup by year and month.
                AddToArchive(post);

                // Create lookup by ID.
                AddToLookup(post);
            }

            _categories = new List<BlogCategory>(categories);
        }

        public IReadOnlyList<BlogCategory> GetCategories()
        {
            return _categories;
        }

        public IReadOnlyList<DateTime> GetArchive()
        {
            return _archive;
        }

        public IReadOnlyList<BlogPost> GetBlogPosts()
        {
            return _posts;
        }

        public IReadOnlyList<BlogPost> GetBlogPosts(string category, int year, int month)
        {
            if (category != null)
            {
                return GetBlogPostsByCategory(category);
            }
            if (year != 0)
            {
                return GetBlogPostsByYearAndMonth(year, month);
            }
            return _posts;
        }

        public BlogPost GetBlogPost(int year, int month, string slug)
        {
            var id = BlogPost.GetId(year, month, slug);
            return _idToPostLookup.ContainsKey(id) ? _idToPostLookup[id] : null;
        }

        public IReadOnlyList<BlogPost> GetBlogPostsByCategory(string category)
        {
            if (_categoryToPostsLookup.ContainsKey(category))
            {
                return _categoryToPostsLookup[category];
            }
            return new List<BlogPost>();
        }

        public IReadOnlyList<BlogPost> GetBlogPostsByYearAndMonth(int year, int month)
        {
            if (_yearMonthToPostsLookup.ContainsKey(year))
            {
                if (month == 0)
                {
                    // Get for whole year.
                    return _yearMonthToPostsLookup[year]
                        .SelectMany(p => p.Value).ToArray();
                }
                if (_yearMonthToPostsLookup[year].ContainsKey(month))
                {
                    return _yearMonthToPostsLookup[year][month];
                }
            }
            return new List<BlogPost>();
        }

        private void AddToLookup(BlogPost post)
        {
            _idToPostLookup.Add(post.Id, post);
        }

        private void AddToArchive(BlogPost post)
        {
            var year = post.PostedAt.Year;
            var month = post.PostedAt.Month;
            if (!_yearMonthToPostsLookup.ContainsKey(year))
            {
                _yearMonthToPostsLookup.Add(year, new Dictionary<int, List<BlogPost>>());
            }
            if (!_yearMonthToPostsLookup[year].ContainsKey(month))
            {
                _yearMonthToPostsLookup[year].Add(month, new List<BlogPost>());
                _archive.Add(new DateTime(year, month, 1, 0, 0, 0));
            }
            _yearMonthToPostsLookup[year][month].Add(post);
        }
    }
}
