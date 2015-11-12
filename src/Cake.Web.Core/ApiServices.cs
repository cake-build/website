using Cake.Web.Core.Rendering;
using Cake.Web.Core.Services;
using Cake.Web.Docs;

namespace Cake.Web.Core
{
    public sealed class ApiServices
    {
        public DocumentModel Model { get; }
        public DocumentModelResolver ModelResolver { get; }
        public UrlResolver UrlResolver { get; }
        public SignatureRenderer SignatureRenderer { get; }
        public LanguageProvider LanguageProvider { get; }
        public SyntaxRenderer SyntaxRenderer { get; }
        public SignatureCache SignatureResolver { get; }

        public ApiServices(
            DocumentModel model,
            DocumentModelResolver documentModelResolver,
            UrlResolver urlResolver,
            SignatureRenderer signatureRenderer,
            LanguageProvider languageProvider,
            SyntaxRenderer syntaxRenderer,
            SignatureCache signatureResolver)
        {
            Model = model;
            ModelResolver = documentModelResolver;
            UrlResolver = urlResolver;
            SignatureRenderer = signatureRenderer;
            LanguageProvider = languageProvider;
            SyntaxRenderer = syntaxRenderer;
            SignatureResolver = signatureResolver;
        }
    }
}