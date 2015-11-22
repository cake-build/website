using System.Collections.Generic;
using System.Linq;
using Cake.Web.Core.Services;

namespace Cake.Web.Models
{
    public sealed class SearchResultViewModel
    {
        public string Term { get; set; }
        public IReadOnlyList<SearchTerm> Results { get; }

        public SearchResultViewModel(string term, IEnumerable<SearchTerm> terms)
        {
            Term = term;
            Results = new List<SearchTerm>(terms ?? Enumerable.Empty<SearchTerm>());
        }
    }
}