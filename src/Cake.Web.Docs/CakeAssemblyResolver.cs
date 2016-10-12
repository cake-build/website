using System;
using System.Collections.Concurrent;
using System.Linq;
using Mono.Cecil;

namespace Cake.Web.Docs
{
    /// <summary>
    /// Non-locking implementation of <see cref="BaseAssemblyResolver"/>.
    /// </summary>
    public class CakeAssemblyResolver : BaseAssemblyResolver
    {
        private readonly ConcurrentDictionary<string, AssemblyDefinition> _cache;

        /// <summary>
        /// Gets the Assembly Reader Parameters.
        /// </summary>
        public ReaderParameters ReaderParameters { get; }

        /// <summary>
        /// Initializes a new instance of the <see cref="CakeAssemblyResolver"/> class.
        /// </summary>
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

        /// <summary>
        /// Resolves assembly defintion from reference.
        /// </summary>
        /// <param name="name">Assembly reference name.</param>
        /// <returns></returns>
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

        /// <summary>
        /// Registers and caches assembly definition.
        /// </summary>
        /// <param name="assembly"></param>
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

        /// <summary>
        /// Disponses loaded resources.
        /// </summary>
        /// <param name="disposing"></param>
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