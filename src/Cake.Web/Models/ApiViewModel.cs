using System.Collections.Generic;
using Cake.Web.Docs;

namespace Cake.Web.Models
{
    public sealed class ApiViewModel
    {
        private readonly List<AssemblyViewModel> _assemblies;

        public IReadOnlyList<AssemblyViewModel> Assemblies
        {
            get { return _assemblies; }
        }

        public ApiViewModel(DocumentModel model)
        {
            _assemblies = new List<AssemblyViewModel>();
            foreach (var assembly in model.Assemblies)
            {
                _assemblies.Add(new AssemblyViewModel(assembly));
            }
        }
    }
}