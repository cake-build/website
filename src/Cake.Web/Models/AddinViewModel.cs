using System.Collections.Generic;
using Cake.Web.Core.Content.Addins;
using Cake.Web.Docs;

namespace Cake.Web.Models
{
    public class AddinViewModel
    {
        public IReadOnlyList<Addin> Addins { get; private set; }
        public IReadOnlyList<string> Categories { get; private set; }
        public string Category { get; set; }

        public AddinViewModel(
            IReadOnlyList<Addin> addins,
            IReadOnlyList<string> categories)
        {
            Addins = addins;
            Categories = categories;
        }
    }
}