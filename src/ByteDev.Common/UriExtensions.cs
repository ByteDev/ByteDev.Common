using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;

namespace ByteDev.Common
{
    /// <summary>
    /// Extension methods for <see cref="T:System.Uri" />.
    /// </summary>
    public static class UriExtensions
    {
        /// <summary>
        /// Add a path to a Uri that does not have any path.
        /// </summary>
        /// <param name="source">The Uri to perform the operation on.</param>
        /// <param name="path">The path to add.</param>
        /// <returns>A new Uri.</returns>
        /// <exception cref="T:System.ArgumentException"><paramref name="path" /> was null or empty.</exception>
        /// <exception cref="T:System.InvalidOperationException"><paramref name="source" /> already has a path or query.</exception>
        public static Uri AddPath(this Uri source, string path)
        {
            if (string.IsNullOrEmpty(path))
                throw new ArgumentException("Path was null or empty.");
            
            if (!source.HasNoPathAndQuery())
                throw new InvalidOperationException("Source Uri already has a path or query.");                
            
            return new Uri(source, path);
        }

        /// <summary>
        /// Returns a dictionary based on the Uri query string.
        /// </summary>
        /// <param name="source">The Uri to perform the operation on.</param>
        /// <returns>A dictionary of query string name value pairs.</returns>
        public static Dictionary<string, string> GetQueryAsDictionary(this Uri source)
        {
            if (!source.HasQuery())
                return new Dictionary<string, string>();
            
            return source.Query.Substring(1).Split('&').ToDictionary(p => p.Split('=')[0], p => p.Split('=')[1]);
        }

        /// <summary>
        /// Add or modify a query string parameter.
        /// </summary>
        /// <param name="source">The Uri to perform the operation on.</param>
        /// <param name="name">Query string parameter name.</param>
        /// <param name="value">Query string parameter value.</param>
        /// <returns>A Uri with the added or modified query string parameter.</returns>
        /// <exception cref="T:System.ArgumentNullException"><paramref name="source" /> is null.</exception>
        /// <exception cref="T:System.ArgumentException"><paramref name="name" /> was null or empty.</exception>
        public static Uri AddOrModifyQueryStringParam(this Uri source, string name, string value)
        {
            if (source == null)
                throw new ArgumentNullException(nameof(source));

            if (string.IsNullOrEmpty(name))
                throw new ArgumentException("Name was null or empty.", nameof(name));

            var uriBuilder = new UriBuilder(source);
            var nameValues = HttpUtility.ParseQueryString(uriBuilder.Query);

            if (value == null)
                nameValues.Remove(name);
            else
                nameValues.AddOrUpdate(name, value);

            uriBuilder.Query = nameValues.ToString();
            
            return uriBuilder.Uri;
        }

        /// <summary>
        /// Add or modify a set of query string parameters.
        /// </summary>
        /// <param name="source">The Uri to perform the operation on.</param>
        /// <param name="obj">Object with a Name property and any other property that contains the value.</param>
        /// <exception cref="T:System.ArgumentNullException"><paramref name="source" /> is null.</exception>
        /// <exception cref="T:System.ArgumentNullException"><paramref name="obj" /> was null.</exception>
        /// <returns>A Uri with the added or modified query string parameters.</returns>
        public static Uri AddOrModifyQueryStringParams(this Uri source, object obj)
        {
            if (source == null)
                throw new ArgumentNullException(nameof(source));

            if(obj == null)
                throw new ArgumentNullException(nameof(obj));

            var nameValues = obj.GetType().GetRuntimeProperties()
                .Where(p => p.GetValue(obj, null) != null)
                .Select(p => new
                {
                    Name = p.Name,
                    Value = p.GetValue(obj, null).ToString()
                });

            var uri = source;

            foreach (var nameValue in nameValues)
            {
                uri = AddOrModifyQueryStringParam(uri, nameValue.Name, nameValue.Value);
            }

            return uri;
        }

        /// <summary>
        /// Remove query string parameter.
        /// </summary>
        /// <param name="source">The Uri to perform the operation on.</param>
        /// <param name="name">The name of the parameter to remove.</param>
        /// <returns>Uri with any parameter with <paramref name="name" /> removed.</returns>
        /// <exception cref="T:System.ArgumentNullException"><paramref name="source" /> is null.</exception>
        /// <exception cref="T:System.ArgumentException"><paramref name="name" /> was null or empty.</exception>
        public static Uri RemoveQueryStringParam(this Uri source, string name)
        {
            if (source == null)
                throw new ArgumentNullException(nameof(source));

            if (string.IsNullOrEmpty(name))
                throw new ArgumentException("Name was null or empty.", nameof(name));

            var uriBuilder = new UriBuilder(source);
            var nameValues = HttpUtility.ParseQueryString(uriBuilder.Query);

            nameValues.Remove(name);

            uriBuilder.Query = nameValues.ToString();

            return uriBuilder.Uri;
        }

        /// <summary>
        /// Removes any query string from the Uri.
        /// </summary>
        /// <param name="source">The Uri to perform the operation on.</param>
        /// <returns>Uri with no query string.</returns>
        /// <exception cref="T:System.ArgumentNullException"><paramref name="source" /> is null.</exception>
        public static Uri ClearQueryString(this Uri source)
        {
            if (source == null)
                throw new ArgumentNullException(nameof(source));
            
            var uriBuilder = new UriBuilder(source) {Query = string.Empty};
            return uriBuilder.Uri;
        }

        /// <summary>
        /// Indicates whether the Uri has no path and query string.
        /// </summary>
        /// <param name="source">The uri to perform the operation on.</param>
        /// <returns>True if <paramref name="source" /> has no path and no query string; otherwise returns false.</returns>
        public static bool HasNoPathAndQuery(this Uri source)
        {
            return string.IsNullOrEmpty(source.PathAndQuery) || source.PathAndQuery == "/";
        }

        /// <summary>
        /// Indicates whether the Uri has any query string.
        /// </summary>
        /// <param name="source">The uri to perform the operation on.</param>
        /// <returns>True if <paramref name="source" /> has a query string; otherwise returns false.</returns>
        public static bool HasQuery(this Uri source)
        {
            return !string.IsNullOrEmpty(source.Query) && (source.Query != "?");
        }

        /// <summary>
        /// Indicates whether the Uri has any fragment.
        /// </summary>
        /// <param name="source">The uri to perform the operation on.</param>
        /// <returns>True if <paramref name="source" /> has a fragment; otherwise returns false.</returns>
        public static bool HasFragment(this Uri source)
        {
            return !string.IsNullOrEmpty(source.Fragment) && (source.Fragment != "#");
        }
    }
}
