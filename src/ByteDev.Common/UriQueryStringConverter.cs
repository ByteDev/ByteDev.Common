using System.Collections.Specialized;
using System.Linq;
using System.Text;

namespace ByteDev.Common
{
    public class UriQueryStringConverter
    {
        public string ConvertToQueryString(NameValueCollection nameValues)
        {
            if ((nameValues == null) || (nameValues.Count < 1))
                return string.Empty;

            var sb = new StringBuilder();

            var items = nameValues.AllKeys.SelectMany(nameValues.GetValues, (k, v) => new { key = k, value = v });

            foreach (var item in items)
            {
                if (sb.Length > 0)
                {
                    sb.Append("&");
                }
                sb.Append(item.key + "=" + item.value);
            }

            return sb.ToString();
        }
    }
}