using System;
using System.Collections.Specialized;
using System.Linq;

namespace ByteDev.Common
{
    internal static class NameValueCollectionExtensions
    {
        public static void AddOrUpdate(this NameValueCollection source, string key, string value)
        {
            if (source == null)
                throw new ArgumentNullException(nameof(source));

            if (source.ContainsKey(key))
                source[key] = value;
            else
                source.Add(key, value);
        }

        public static bool ContainsKey(this NameValueCollection source, string key)
        {
            if (source == null)
                throw new ArgumentNullException(nameof(source));

            if (source.Get(key) == null)
            {
                return source.AllKeys.Contains(key);
            }

            return true;
        }
    }
}