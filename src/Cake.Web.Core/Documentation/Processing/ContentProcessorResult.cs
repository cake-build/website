namespace Cake.Web.Core.Documentation.Processing
{
    public sealed class ContentProcessorResult
    {
        private readonly string _body;

        public string Body
        {
            get { return _body; }
        }

        public ContentProcessorResult(string body)
        {
            _body = body;
        }
    }
}
