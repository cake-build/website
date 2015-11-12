using System;
using System.Collections.Concurrent;
using System.Linq;
using Cake.Web.Docs;
using Cake.Web.Docs.Reflection;
using Cake.Web.Docs.Reflection.Signatures;
using Mono.Cecil;

namespace Cake.Web.Core.Services
{
    public sealed class SignatureCache
    {
        private readonly IUrlResolver _resolver;
        private readonly ConcurrentDictionary<DocumentedType, TypeSignature> _documentedTypes;
        private readonly ConcurrentDictionary<TypeReference, TypeSignature> _typeReferences;
        private readonly ConcurrentDictionary<DocumentedMethod, MethodSignature> _documentedMethods;

        public SignatureCache(DocumentModel model, UrlResolver resolver)
        {
            _resolver = resolver;

            _documentedTypes = new ConcurrentDictionary<DocumentedType, TypeSignature>(new DocumentedTypeComparer());
            _typeReferences = new ConcurrentDictionary<TypeReference, TypeSignature>(new TypeReferenceComparer());
            _documentedMethods = new ConcurrentDictionary<DocumentedMethod, MethodSignature>(new DocumentedMethodComparer());

            foreach (var @namespace in model.Assemblies.SelectMany(a => a.Namespaces))
            {
                foreach (var type in @namespace.Types)
                {
                    var typeSignature = type.Definition.GetTypeSignature(_resolver);
                    _documentedTypes.TryAdd(type, typeSignature);
                    _typeReferences.TryAdd(type.Definition, typeSignature);

                    foreach (var method in type.Methods
                        .Concat(type.Constructors)
                        .Concat(type.Operators))
                    {
                        var methodSignature = method.Definition.GetMethodSignature(_resolver);
                        _documentedMethods.TryAdd(method, methodSignature);
                    }
                }
            }
        }

        public TypeSignature GetTypeSignature(DocumentedType type)
        {
            TypeSignature signature;
            if (_documentedTypes.TryGetValue(type, out signature))
            {
                return signature;
            }
            var message = $"Could not find signature for {type.Identity}.";
            throw new InvalidOperationException(message);
        }

        public TypeSignature GetTypeSignature(TypeReference type)
        {
            TypeSignature signature;
            if (!_typeReferences.TryGetValue(type, out signature))
            {
                signature = type.GetTypeSignature(_resolver);
                _typeReferences.TryAdd(type, signature);
            }
            return signature;
        }

        public MethodSignature GetMethodSignature(DocumentedMethod method)
        {
            MethodSignature signature;
            if (_documentedMethods.TryGetValue(method, out signature))
            {
                return signature;
            }
            var message = $"Could not find signature for {method.Identity}.";
            throw new InvalidOperationException(message);
        }
    }
}
