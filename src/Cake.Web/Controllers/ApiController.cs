using System;
using System.Web.Mvc;
using Cake.Web.Core.Services;
using Cake.Web.Docs;
using Cake.Web.Docs.Reflection;
using Cake.Web.Models;

namespace Cake.Web.Controllers
{
    public class ApiController : Controller
    {
        private readonly DocumentModel _model;
        private readonly RouteService _router;

        public ApiController(DocumentModel model, RouteService router)
        {
            _model = model;
            _router = router;
        }

        public ActionResult Index()
        {
            return View(new ApiViewModel(_model));
        }

        public ActionResult Namespace(string namespaceId)
        {
            var @namespace = _router.FindNamespacesFromRoutePart(namespaceId);
            return View(new NamespaceViewModel(@namespace));
        }

        public ActionResult Type(string namespaceId, string typeId)
        {
            var type = _router.FindTypeFromRoutePart(typeId);
            return View(new TypeViewModel(type));
        }

        public ActionResult Member(string namespaceId, string typeId, string memberId)
        {
            // HACK: fix this
            var member = _router.FindTypeMemberFromRoutePart(memberId);
            switch (member.Classification)
            {
                case MemberClassification.Method:
                {
                    return View("Method", new MethodViewModel((DocumentedMethod)member));
                }
                case MemberClassification.Property:
                {
                    return View("Property", new PropertyViewModel((DocumentedProperty)member));
                }
                default:
                {
                    throw new InvalidOperationException("Unknown member type.");
                }
            }
        }
    }
}