using System;
using System.Collections.Generic;
using System.Linq;

namespace Cake.Web.Core.Dsl
{
    public sealed class DslModel
    {
        private readonly Dictionary<string, DslCategory> _cache;
        private readonly List<DslCategory> _categories;

        public IReadOnlyList<DslCategory> Categories
        {
            get { return _categories; }
        }

        public DslModel(IEnumerable<DslCategory> categories)
        {
            _categories = new List<DslCategory>(categories);
            _cache = _categories.ToDictionary(x => x.Slug, x => x, StringComparer.OrdinalIgnoreCase);
        }

        public DslCategory FindCategory(string slug)
        {
            DslCategory category;
            return _cache.TryGetValue(slug, out category) ? category : null;
        }
    }
}
