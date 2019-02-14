using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Web;
using ByteDev.Common.Collections;

namespace ByteDev.Common
{
    public static class UriExtensions
    {
        public static Uri AddPath(this Uri source, string path)
        {
            if (string.IsNullOrEmpty(path))
            {
                throw new ArgumentException("Path was null or empty.");
            }

            if (!source.HasNoPathAndQuery())
            {
                throw new InvalidOperationException("Source Uri already has a path or query.");                
            }
            return new Uri(source, path);
        }

        public static Dictionary<string, string> GetQueryAsDictionary(this Uri source)
        {
            if (!source.HasQuery())
            {
                return new Dictionary<string, string>();
            }
            return source.Query.Substring(1).Split('&').ToDictionary(p => p.Split('=')[0], p => p.Split('=')[1]);
        }

        public static Uri AddOrModifyQueryStringParam(this Uri source, string name, string value)
        {
            if (source == null)
                throw new ArgumentNullException(nameof(source));

            if (string.IsNullOrEmpty(name))
                throw new ArgumentException("Name was be null or empty.", nameof(name));

            var uriBuilder = new UriBuilder(source);
            var nameValues = HttpUtility.ParseQueryString(uriBuilder.Query);

            if (value == null)
            {
                nameValues.Remove(name);
            }
            else
            {
                nameValues.AddOrUpdate(name, value);
            }

            uriBuilder.Query = nameValues.ToString();
            
            return uriBuilder.Uri;
        }

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

        public static Uri ClearQueryString(this Uri source)
        {
            if (source == null)
                throw new ArgumentNullException(nameof(source));
            
            var uriBuilder = new UriBuilder(source) {Query = string.Empty};
            return uriBuilder.Uri;
        }

        public static bool HasNoPathAndQuery(this Uri source)
        {
            return string.IsNullOrEmpty(source.PathAndQuery) || (source.PathAndQuery == "/");
        }

        public static bool HasQuery(this Uri source)
        {
            return !string.IsNullOrEmpty(source.Query) && (source.Query != "?");
        }

        public static bool HasFragment(this Uri source)
        {
            return !string.IsNullOrEmpty(source.Fragment) && (source.Fragment != "#");
        }
    }
}
