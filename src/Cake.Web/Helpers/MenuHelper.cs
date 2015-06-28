using System;
using System.Web.Mvc;

namespace Cake.Web.Helpers
{
    public static class MenuHelpers
    {
        public static string ActiveMenu(this HtmlHelper helper, string menu)
        {
            if (helper.ViewBag != null)
            {
                var value = helper.ViewBag.Menu as string;
                if (value != null)
                {
                    if (value.Equals(menu, StringComparison.OrdinalIgnoreCase))
                    {
                        return "selected";
                    }
                }
            }
            return string.Empty;
        }
    }
}