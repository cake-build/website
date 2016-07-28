using System.Collections.Generic;
using Cake.Web.Core.Content.Modules;

namespace Cake.Web.Models
{
    public class ModuleViewModel
    {
        public IReadOnlyList<Module> Modules { get; private set; }
        public IReadOnlyList<string> Categories { get; private set; }
        public string Category { get; set; }

        public ModuleViewModel(
            IReadOnlyList<Module> modules,
            IReadOnlyList<string> categories)
        {
            Modules = modules;
            Categories = categories;
        }
    }
}