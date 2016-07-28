using Cake.Core.IO;

namespace Cake.Web.Core.Content.Modules
{
    public interface IModuleReader
    {
        ModuleIndex Read(FilePath path);
    }
}