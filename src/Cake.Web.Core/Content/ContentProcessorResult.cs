namespace Cake.Web.Core.Content
{
    public sealed class ContentProcessorResult
    {
        public string Body { get; }

        public ContentProcessorResult(string body)
        {
            Body = body;
        }
    }
}
