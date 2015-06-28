using Cake.Web.Docs;

namespace Cake.Web.Models
{
    public sealed class MethodViewModel
    {
        private readonly DocumentedMethod _data;

        public DocumentedMethod Data
        {
            get { return _data; }
        }

        public MethodViewModel(DocumentedMethod data)
        {
            _data = data;
        }
    }
}
