using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using ByteDev.Common.Collections;

namespace ByteDev.Common
{
    public class UriPathBuilder
    {
        private readonly IList<string> _paths;
        private readonly NameValueCollection _nameValues;

        public UriPathBuilder()
        {
            _paths = new List<string>();
            _nameValues = new NameValueCollection();
        }

        public UriPathBuilder AddPath(string path)
        {
            _paths.Add(path);
            return this;
        }

        public UriPathBuilder AddOrModifyQueryStringParam(string name, string value)
        {
            _nameValues.AddOrModify(name, value);
            return this;
        }

        public string Build()
        {
            const string host = "http://local/";

            var uri = AppendPaths(new Uri(host), _paths);

            var uriWithQs = AppendQueryString(uri);

            return uriWithQs.ToString().Substring(host.Length - 1);
        }

        private Uri AppendQueryString(Uri uri)
        {
            var uriBuilder = new UriBuilder(uri)
            {
                Query = new UriQueryStringConverter().ConvertToQueryString(_nameValues)
            };
            return uriBuilder.Uri;
        }

        private static Uri AppendPaths(Uri uri, IList<string> paths)
        {
            return new Uri(paths.Aggregate(uri.AbsoluteUri, (current, path) => $"{current.TrimEnd('/')}/{path.TrimStart('/')}"));
        }
    }
}