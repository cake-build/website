using Cake.Core.IO;

namespace Cake.Web.Core.Content.Blog
{
    public interface IBlogReader
    {
        BlogIndex Parse(DirectoryPath path);
    }
}