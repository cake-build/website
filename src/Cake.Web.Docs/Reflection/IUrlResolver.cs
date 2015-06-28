namespace Cake.Web.Docs.Reflection
{
    /// <summary>
    /// Represents an URL resolver.
    /// </summary>
    public interface IUrlResolver
    {
        /// <summary>
        /// Gets the URL for a specific cref.
        /// </summary>
        /// <param name="cref">The cref to get an URL for.</param>
        /// <returns>The resolved URL, or <c>null</c>.</returns>
        string GetUrl(string cref);
    }
}
