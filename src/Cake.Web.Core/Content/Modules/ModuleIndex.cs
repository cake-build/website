using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cake.Web.Core.Content.Modules
{
    public sealed class ModuleIndex
    {
        private readonly List<Module> _modules;
        private readonly List<string> _categories;
        private readonly Dictionary<string, List<Module>> _categoryLookup;
        private readonly List<Module> _emptyModuleList;

        public int Count => _modules.Count;

        public ModuleIndex(IEnumerable<Module> modules)
        {
            _modules = new List<Module>(modules as Module[] ?? modules.ToArray());
            _categoryLookup = new Dictionary<string, List<Module>>(StringComparer.OrdinalIgnoreCase);

            var categories = new HashSet<string>(StringComparer.OrdinalIgnoreCase);
            foreach (var module in _modules)
            {
                if (module.Categories.Count > 0)
                {
                    foreach (var category in module.Categories)
                    {
                        if (!_categoryLookup.ContainsKey(category))
                        {
                            _categoryLookup.Add(category, new List<Module>());
                        }
                        _categoryLookup[category].Add(module);
                        categories.Add(category);
                    }
                }
            }

            _categories = new List<string>(categories);
            _emptyModuleList = new List<Module>();
        }

        public IReadOnlyList<Module> GetModules()
        {
            return _modules;
        }

        public IReadOnlyList<Module> GetModulesByCategory(string category)
        {
            if (_categoryLookup.ContainsKey(category))
            {
                return _categoryLookup[category];
            }
            return _emptyModuleList;
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
