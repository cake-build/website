using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cake.Web.Core.Search
{
    public class PrefixMatcher<T>
        where T : class
    {
        private readonly static List<T> EmptyResult = new List<T>();

        public IReadOnlyList<T> GetMatches(PrefixTree<T> tree, string word)
        {
            if (word.Length < 4)
            {
                return EmptyResult;
            }

            var result = new List<T>();

            var letters = new Queue<char>(word);
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

            if (root != tree.Root)
            {
                result.AddRange(RecursiveSearch(root));
            }

            return result;
        }

        private static IEnumerable<T> RecursiveSearch(PrefixTreeNode<T> node)
        {
            var stack = new Stack<PrefixTreeNode<T>>();
            stack.Push(node);
            while (stack.Count > 0)
            {
                var current = stack.Pop();
                if (current.IsTerminal)
                {
                    yield return current.Data;
                }
                else
                {
                    foreach (var child in current.Children)
                    {
                        stack.Push(child);
                    }
                }
            }
        }
    }
}
