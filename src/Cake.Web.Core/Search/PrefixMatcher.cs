using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cake.Web.Core.Search
{
    public class SearchableEqualityComparer : IEqualityComparer<ISearchable>
    {
        public bool Equals(ISearchable x, ISearchable y)
        {
            if (x == null && y == null)
            {
                return true;
            }
            if (x == null || y == null)
            {
                return false;
            }
            return x.Term.Equals(y.Term, StringComparison.OrdinalIgnoreCase);
        }

        public int GetHashCode(ISearchable obj)
        {
            return obj.Term.GetHashCode();
        }
    }

    public class PrefixMatcher<T>
        where T : class, ISearchable
    {
        private readonly static HashSet<T> EmptyResult = new HashSet<T>();

        public ISet<T> GetMatches(PrefixTree<T> tree, string word, int maxResults)
        {
            if (string.IsNullOrWhiteSpace(word) || word.Length < 1)
            {
                return EmptyResult;
            }

            var result = new HashSet<T>(new SearchableEqualityComparer());

            var letters = new Queue<char>(word.ToLowerInvariant());
            var root = tree.Root;
            while (letters.Count > 0)
            {
                var letter = letters.Dequeue();
                var node = root.Children.SingleOrDefault(l => l.Letter == letter);
                if (node == null)
                {
                    root = tree.Root;
                    break;
                }
                root = node;
            }

            if (root.IsTerminal)
            {
                result.Add(root.Data);
            }

            if (result.Count < maxResults && root != tree.Root)
            {
                RecursiveSearch(root, result, maxResults - result.Count);
            }

            return result;
        }

        private static void RecursiveSearch(PrefixTreeNode<T> node, HashSet<T> result, int maxResults)
        {
            var stack = new Stack<PrefixTreeNode<T>>();
            stack.Push(node);
            while (stack.Count > 0)
            {
                var current = stack.Pop();
                if (current.IsTerminal)
                {
                    result.Add(current.Data);
                    if (result.Count > maxResults)
                    {
                        break;
                    }
                }

                foreach (var child in current.Children)
                {
                    stack.Push(child);
                }
            }
        }
    }
}
