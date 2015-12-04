using System;
using Cake.Web.Core.Search;

namespace Cake.Web.Core.Services
{
    public sealed class SearchTerm : ISearchable
    {
        public string Term { get; set; }
        public Uri Uri { get; set; }
        public string Category { get; set; }
        public bool IsAddin { get; set; }
    }
}