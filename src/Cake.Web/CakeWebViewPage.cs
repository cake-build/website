using System.Web.Mvc;
using Cake.Web.Core;

namespace Cake.Web
{
    public abstract class CakeWebViewPage<T> : WebViewPage<T>
    {
        public ApiServices Api { get; private set; }

        public override void InitHelpers()
        {
            Api = DependencyResolver.Current.GetService<ApiServices>();
            base.InitHelpers();
        }

        public override void Execute()
        {
        }
    }
}
