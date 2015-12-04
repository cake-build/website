using System;
using System.Collections.Generic;

namespace Cake.Web.Core.Search
{
    public sealed class PrefixTreeNode<T>
        where T : class, ISearchable
    {
        public char Letter { get; internal set; }
        public bool IsTerminal { get; internal set; }
        public T Data { get; internal set; }
        public PrefixTreeNode<T> Parent { get; internal set; }
        public IList<PrefixTreeNode<T>> Children { get; }

        public PrefixTreeNode()
        {
            Children = new List<PrefixTreeNode<T>>();
        }
    }
}