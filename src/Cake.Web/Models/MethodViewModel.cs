using Cake.Web.Docs;

namespace Cake.Web.Models
{
    public sealed class MethodViewModel
    {
        public DocumentedMethod Data { get; }

        public MethodViewModel(DocumentedMethod data)
        {
            Data = data;
        }
    }
}
