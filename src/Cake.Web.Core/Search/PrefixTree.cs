using System.Collections.Generic;
using System.Linq;

namespace Cake.Web.Core.Search
{
    public sealed class PrefixTree<T>
        where T : class
    {
        public PrefixTreeNode<T> Root { get; }

        public PrefixTree()
        {
            Root = new PrefixTreeNode<T> { Letter = '\0' };
        }

        public void Add(string word, T data)
        {
            var letters = new Queue<char>(word);
            var root = Root;
            while (letters.Count > 0)
            {
                var letter = letters.Dequeue();
                var node = root.Children.SingleOrDefault(l => l.Letter == letter);
                if (node == null)
                {
                    node = new PrefixTreeNode<T>
                    {
                        Letter = letter,
                        Parent = root,
                    };
                    root.Children.Add(node);
                }
                root = node;
            }
            root.IsTerminal = true;
            root.Data = data;
        }
    }
}