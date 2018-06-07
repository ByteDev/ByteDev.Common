using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ByteDev.Common
{
    /// <summary>
    /// Functionality to help return strings when overriding the ToString method.
    /// </summary>
    public class ToStringHelper
    {
        private readonly string _nullValue;

        public ToStringHelper() : this(string.Empty)
        {
        }

        public ToStringHelper(string nullValue)
        {
            _nullValue = nullValue;
        }

        public string ToString(string name, object value)
        {
            if(string.IsNullOrEmpty(name))
                throw new ArgumentNullOrEmptyException(nameof(name));

            if (value == null)
            {
                return FormatNull(name);
            }

            return Format(name, value.ToString());
        }

        public string ToString<T>(string name, IEnumerable<T> values)
        {
            if (values == null)
                return ToString(name, null);

            var s = new StringBuilder("{ ");

            if (values.Any())
            {
                s.Append(values.First());

                foreach (var id in values.Skip(1))
                {
                    s.Append($", {id}");
                }

                s.Append(" }");
            }
            else
            {
                s.Append("}");
            }

            return Format(name, s.ToString());
        }

        private static string Format(string name, string value)
        {
            return $"{name}: {value}";
        }

        private string FormatNull(string name)
        {
            return Format(name, _nullValue);
        }
    }
}