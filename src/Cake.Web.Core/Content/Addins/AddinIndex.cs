using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Cake.Web.Docs;

namespace Cake.Web.Core.Content.Addins
{
    public sealed class AddinIndex
    {
        private readonly List<Addin> _addins;
        private readonly List<string> _categories; 
        private readonly Dictionary<string, List<Addin>> _categoryLookup;
        private readonly List<Addin> _emptyAddinList;

        public int Count
        {
            get { return _addins.Count; }
        }

        public AddinIndex(IEnumerable<Addin> addins)
        {
            _addins = new List<Addin>(addins as Addin[] ?? addins.ToArray());
            _categoryLookup = new Dictionary<string, List<Addin>>(StringComparer.OrdinalIgnoreCase);

            var categories = new HashSet<string>(StringComparer.OrdinalIgnoreCase);
            foreach (var addin in _addins)
            {
                if (addin.Categories.Count > 0)
                {
                    foreach (var category in addin.Categories)
                    {
                        if (!_categoryLookup.ContainsKey(category))
                        {
                            _categoryLookup.Add(category, new List<Addin>());
                        }
                        _categoryLookup[category].Add(addin);
                        categories.Add(category);
                    }
                }
            }

            _categories = new List<string>(categories);
            _emptyAddinList = new List<Addin>();
        }

        public IReadOnlyList<Addin> GetAddins()
        {
            return _addins;
        }

        public IReadOnlyList<Addin> GetAddinsByCategory(string category)
        {
            if (_categoryLookup.ContainsKey(category))
            {
                return _categoryLookup[category];
            }
            return _emptyAddinList;
        }

        public IReadOnlyList<string> GetCategories()
        {
            return _categories;
        }

        public string GetCategoryName(string category)
        {
            // Well... this is hacky.
            return _categories.FirstOrDefault(x => x.Equals(category, StringComparison.OrdinalIgnoreCase));
        }
    }
}
