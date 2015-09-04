using System.Collections.Generic;
using Cake.Web.Core.Content.Addins;

namespace Cake.Web.Models
{
    public class AddinViewModel
    {
        public IReadOnlyList<Addin> Addins { get; }
        public IReadOnlyList<string> Categories { get; }
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