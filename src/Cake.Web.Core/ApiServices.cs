using Cake.Web.Core.Rendering;
using Cake.Web.Core.Services;
using Cake.Web.Docs;

namespace Cake.Web.Core
{
    public sealed class ApiServices
    {
        private readonly DocumentModel _model;
        private readonly DocumentModelResolver _documentModelResolver;
        private readonly UrlResolver _urlResolver;
        private readonly SignatureRenderer _signatureRenderer;
        private readonly LanguageProvider _languageProvider;
        private readonly SyntaxRenderer _syntaxRenderer;
        private readonly SignatureCache _signatureResolver;

        public DocumentModel Model
        {
            get { return _model; }
        }

        public DocumentModelResolver ModelResolver
        {
            get { return _documentModelResolver; }
        }

        public UrlResolver UrlResolver
        {
            get { return _urlResolver; }
        }

        public SignatureRenderer SignatureRenderer
        {
            get { return _signatureRenderer; }
        }

        public LanguageProvider LanguageProvider
        {
            get { return _languageProvider; }
        }

        public SyntaxRenderer SyntaxRenderer
        {
            get { return _syntaxRenderer; }
        }

        public SignatureCache SignatureResolver
        {
            get { return _signatureResolver; }
        }

        public ApiServices(
            DocumentModel model,
            DocumentModelResolver documentModelResolver,
            UrlResolver urlResolver, 
            SignatureRenderer signatureRenderer,
            LanguageProvider languageProvider, 
            SyntaxRenderer syntaxRenderer, 
            SignatureCache signatureResolver)
        {
            _model = model;
            _documentModelResolver = documentModelResolver;
            _urlResolver = urlResolver;
            _signatureRenderer = signatureRenderer;
            _languageProvider = languageProvider;
            _syntaxRenderer = syntaxRenderer;
            _signatureResolver = signatureResolver;
        }
    }
}