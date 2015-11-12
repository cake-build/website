using Cake.Web.Docs;

namespace Cake.Web.Models
{
    public sealed class PropertyViewModel
    {
        public DocumentedProperty Data { get; }

        public PropertyViewModel(DocumentedProperty data)
        {
            Data = data;
        }
    }
}
