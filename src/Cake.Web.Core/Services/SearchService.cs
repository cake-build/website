using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Cake.Web.Core.Search;
using Cake.Web.Docs;
using Cake.Web.Docs.Reflection;

namespace Cake.Web.Core.Services
{
    public sealed class SearchService
    {
        private readonly IUrlResolver _resolver;
        private readonly PrefixMatcher<SearchTerm> _matcher;
        private readonly ReaderWriterLockSlim _lock;
        private readonly PrefixTree<SearchTerm> _tree;

        public SearchService(IUrlResolver resolver)
        {
            _resolver = resolver;
            _matcher = new PrefixMatcher<SearchTerm>();
            _lock = new ReaderWriterLockSlim(LockRecursionPolicy.NoRecursion);
            _tree = new PrefixTree<SearchTerm>();
        }

        public ISet<SearchTerm> Find(string query, int maxResults)
        {
            try
            {
                _lock.EnterReadLock();
                return _matcher.GetMatches(_tree, query, maxResults);
            }
            finally
            {
                _lock.ExitReadLock();
            }
        }

        public void Build(DocumentModel model)
        {
            try
            {
                _lock.EnterWriteLock();
                foreach (var assembly in model.Assemblies)
                {
                    foreach (var @namespace in assembly.Namespaces)
                    {
                        IndexNamespace(@namespace);
                        foreach (var type in @namespace.Types)
                        {
                            ImportType(type);
                            foreach (var method in type.Methods)
                            {
                                ImportMethod(method);
                            }
                            foreach (var property in type.Properties)
                            {
                                ImportProperty(property);
                            }
                        }
                    }
                }
            }
            finally
            {
                _lock.ExitWriteLock();
            }
        }

        private void IndexNamespace(DocumentedNamespace @namespace)
        {
            _tree.Add(new SearchTerm
            {
                Term = @namespace.Name,
                Uri = new Uri(_resolver.GetUrl(@namespace.Identity), UriKind.Relative),
                Category = "Namespace",
                IsAddin = @namespace.Metadata.IsOwnedByAddin
            });
        }

        private void ImportType(DocumentedType type)
        {
            _tree.Add(new SearchTerm
            {
                Term = type.Definition.Name,
                Uri = new Uri(_resolver.GetUrl(type.Identity), UriKind.Relative),
                Category = type.TypeClassification.ToString(),
                IsAddin = type.Metadata.IsOwnedByAddin
            });
        }

        private void ImportMethod(DocumentedMethod method)
        {
            _tree.Add(new SearchTerm
            {
                Term = method.Definition.Name,
                Uri = new Uri(_resolver.GetUrl(method.Identity), UriKind.Relative),
                Category =
                    method.Metadata.IsAlias
                        ? method.Metadata.IsPropertyAlias ? "Property alias" : "Method alias"
                        : method.MethodClassification.ToString(),
                IsAddin = method.Metadata.IsOwnedByAddin
            });
        }

        private void ImportProperty(DocumentedProperty property)
        {
            _tree.Add(new SearchTerm
            {
                Term = property.Definition.Name,
                Uri = new Uri(_resolver.GetUrl(property.Identity), UriKind.Relative),
                Category = "Property",
                IsAddin = property.Metadata.IsOwnedByAddin
            });
        }
    }
}
