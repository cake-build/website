using System;
using System.Collections.Generic;
using System.Linq;

namespace Cake.Web.Core.Dsl
{
    public sealed class DslModel
    {
        private readonly Dictionary<string, DslCategory> _cache;

        public IReadOnlyList<DslCategory> Categories { get; }

        public DslModel(IEnumerable<DslCategory> categories)
        {
            Categories = new List<DslCategory>(categories);
            _cache = Categories.ToDictionary(x => x.Slug, x => x, StringComparer.OrdinalIgnoreCase);
        }

        public DslCategory FindCategory(string slug)
        {
            DslCategory category;
            return _cache.TryGetValue(slug, out category) ? category : null;
        }
    }
}
