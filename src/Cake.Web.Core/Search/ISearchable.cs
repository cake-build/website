using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cake.Web.Core.Search
{
    public interface ISearchable
    {
        string Term { get; }
    }
}
