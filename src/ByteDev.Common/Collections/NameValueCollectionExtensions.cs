using System.Linq;
using System.Collections.Specialized;

namespace ByteDev.Common.Collections
{
    public static class NameValueCollectionExtensions
    {
        public static void AddOrUpdate(this NameValueCollection nameValues, string name, string value)
        {
            if (nameValues[name] == null)
            {
                nameValues.Add(name, value);
            }
            else
            {
                nameValues[name] = value;
            }
        }

        public static bool ContainsKey(this NameValueCollection collection, string key)
        {
            if (collection.AllKeys.Contains(key))
                return true;

            var keysWithoutValues = collection.GetValues(null);
            return keysWithoutValues != null && keysWithoutValues.Contains(key);
        }
    }
}
