using System.Collections.Generic;

namespace Cake.Web.Core.Services
{
    /// <summary>
    /// Language provider for C#.
    /// </summary>
    public sealed class LanguageProvider
    {
        private readonly Dictionary<string, string> _lookup = new Dictionary<string, string>
        {
            { "T:System.Boolean", "bool" },
            { "T:System.Byte", "byte" },
            { "T:System.SByte", "sbyte" },
            { "T:System.Char", "char" },
            { "T:System.Decimal", "decimal" },
            { "T:System.Double", "double" },
            { "T:System.Single", "float" },
            { "T:System.Int32", "int" },
            { "T:System.UInt32", "uint" },
            { "T:System.Int64", "long" },
            { "T:System.UInt64", "ulong" },
            { "T:System.Object", "object" },
            { "T:System.Int16", "short" },
            { "T:System.UInt16", "ushort" },
            { "T:System.String", "string" },
            { "T:System.Void", "void" }
        };

        /// <summary>
        /// Gets the alias for a cref.
        /// </summary>
        /// <param name="cref">The cref.</param>
        /// <returns>An alias.</returns>
        public string GetAlias(string cref)
        {
            return _lookup.ContainsKey(cref)
                ? _lookup[cref] : null;
        }
    }
}
