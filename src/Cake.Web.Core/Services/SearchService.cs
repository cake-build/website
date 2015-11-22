using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Cake.Web.Core.Search;
using Cake.Web.Docs;

namespace Cake.Web.Core.Services
{
    public sealed class SearchService
    {
        private readonly RouteService _route;
        private readonly PrefixTree<SearchTerm> _tree;
        private readonly PrefixMatcher<SearchTerm> _matcher;
        private readonly ReaderWriterLockSlim _lock;

        public SearchService(RouteService route)
        {
            _route = route;
            _tree = new PrefixTree<SearchTerm>();
            _matcher = new PrefixMatcher<SearchTerm>();
            _lock = new ReaderWriterLockSlim(LockRecursionPolicy.NoRecursion);
        }

        public IEnumerable<SearchTerm> Find(string query)
        {
            try
            {
                _lock.EnterReadLock();
                return _matcher.GetMatches(_tree, query);
            }
            finally
            {
                _lock.ExitReadLock();
            }
        }

        public void BuildIndex(DocumentModel model)
        {
            try
            {
                _lock.EnterWriteLock();
                foreach (var assembly in model.Assemblies)
                {
                    foreach (var @namespace in assembly.Namespaces)
                    {
                        var item = new SearchTerm
                        {
                            Word = @namespace.Name,
                            Uri = new Uri(_route.GetRoutePart(@namespace), UriKind.Relative)
                        };
                        _tree.Add(@namespace.Name, item);
                    }
                }
            }
            finally
            {
                _lock.ExitWriteLock();
            }
        }
    }
}
