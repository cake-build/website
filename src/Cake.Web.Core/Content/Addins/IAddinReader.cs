using System.Collections.Generic;
using Cake.Core.IO;

namespace Cake.Web.Core.Content.Addins
{
    public interface IAddinReader
    {
        AddinIndex Read(FilePath path);
    }
}