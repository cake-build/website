using System;
using System.Collections.Generic;
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
            List<DocumentedNamespace> namespaces;
            if (!_router.TryFindNamespacesFromRoutePart(namespaceId, out namespaces))
            {
                return HttpNotFound($"Namespace not found, namespaceId: {namespaceId}");
            }
            return View(new NamespaceViewModel(namespaces));
        }

        public ActionResult Type(string namespaceId, string typeId)
        {
            DocumentedType type;
            if (!_router.TryFindTypeFromRoutePart(typeId, out type))
            {
                return HttpNotFound($"Type not found, namespaceId: {namespaceId}, typeId: {typeId}");
            }
            return View(new TypeViewModel(type));
        }

        public ActionResult Member(string namespaceId, string typeId, string memberId)
        {
            DocumentedMember member;
            if (!_router.TryFindTypeMemberFromRoutePart(memberId, out member))
            {
                return HttpNotFound($"Member not found, namespaceId: {namespaceId}, typeId: {typeId}, memberId: {memberId}");
            }
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