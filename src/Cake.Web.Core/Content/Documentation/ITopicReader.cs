using Cake.Core.IO;

namespace Cake.Web.Core.Content.Documentation
{
    public interface ITopicReader
    {
        TopicTree Read(FilePath path);
    }
}
