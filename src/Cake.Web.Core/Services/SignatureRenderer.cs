using System.Web;
using System.Web.UI;
using Cake.Web.Core.Rendering;
using Cake.Web.Docs;
using Cake.Web.Docs.Reflection.Signatures;

namespace Cake.Web.Core.Services
{
    public sealed class SignatureRenderer
    {
        private readonly LanguageProvider _languageProvider;
        private readonly TypeRenderer _typeRenderer;
        private readonly MethodRenderer _methodRenderer;

        public SignatureRenderer(
            DocumentModel model,
            LanguageProvider language)
        {
            _typeRenderer = new TypeRenderer(model, language);
            _methodRenderer = new MethodRenderer(_typeRenderer);
            _languageProvider = new LanguageProvider();
        }

        public void Render(HtmlTextWriter writer, TypeSignature type, TypeRenderOption option)
        {
            _typeRenderer.Render(writer, type, option);
        }

        public IHtmlString Render(TypeSignature type, TypeRenderOption option)
        {
            return _typeRenderer.Render(type, option);
        }

        public IHtmlString Render(MethodSignature method, MethodRenderOption option)
        {
            return _methodRenderer.Render(_languageProvider, method, option);
        }
    }
}
