using System;
using System.Collections.Concurrent;
using System.Linq;
using Mono.Cecil;

namespace Cake.Web.Docs
{
    public class CakeAssemblyResolver : BaseAssemblyResolver
    {
        private readonly ConcurrentDictionary<string, AssemblyDefinition> _cache;
        public ReaderParameters ReaderParameters { get; }

        public CakeAssemblyResolver()
        {
            _cache = new ConcurrentDictionary<string, AssemblyDefinition>(StringComparer.Ordinal);
            ReaderParameters = new ReaderParameters
            {
                AssemblyResolver = this,
                InMemory = true,
                ReadWrite = false,
                ReadingMode = ReadingMode.Immediate
            };
        }

        public override AssemblyDefinition Resolve(AssemblyNameReference name)
        {
            if (name == null)
                throw new ArgumentNullException(nameof(name));

            AssemblyDefinition assembly;
            if (_cache.TryGetValue(name.FullName, out assembly))
                return assembly;

            assembly = base.Resolve(name, ReaderParameters);
            _cache.AddOrUpdate(
                name.FullName,
                key => assembly,
                (key, old) => assembly
                );

            return assembly;
        }

        protected void RegisterAssembly(AssemblyDefinition assembly)
        {
            if (assembly == null)
                throw new ArgumentNullException(nameof(assembly));

            var name = assembly.Name.FullName;
            if (_cache.ContainsKey(name))
                return;

            _cache.AddOrUpdate(
                name,
                key => assembly,
                (key, old) => assembly
                );
        }

        protected override void Dispose(bool disposing)
        {
            Array.ForEach(
                _cache.Values.ToArray(),
                assembly=> assembly.Dispose()
                );

            _cache.Clear();

            base.Dispose(disposing);
        }
    }
}