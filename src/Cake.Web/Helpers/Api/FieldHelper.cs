using System.Web;
using System.Web.Mvc;
using Cake.Web.Core;
using Cake.Web.Docs;

namespace Cake.Web.Helpers.Api
{
    public static class FieldHelper
    {
        public static IHtmlString FieldName(this ApiServices context, DocumentedField field)
        {
            if (field != null)
            {
                return MvcHtmlString.Create(field.Definition.Name);
            }
            return MvcHtmlString.Create(string.Empty);
        }
    }
}