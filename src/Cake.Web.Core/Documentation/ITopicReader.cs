using Cake.Core.IO;

namespace Cake.Web.Core.Documentation
{
    public interface ITopicReader
    {
        TopicTree Read(FilePath path);
    }
}
