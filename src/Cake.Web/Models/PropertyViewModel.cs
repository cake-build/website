using Cake.Web.Docs;

namespace Cake.Web.Models
{
    public sealed class PropertyViewModel
    {
        private readonly DocumentedProperty _data;

        public DocumentedProperty Data
        {
            get { return _data; }
        }

        public PropertyViewModel(DocumentedProperty data)
        {
            _data = data;
        }
    }
}
