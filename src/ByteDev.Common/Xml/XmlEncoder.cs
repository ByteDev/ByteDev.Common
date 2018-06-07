using System.Text;

namespace ByteDev.Common.Xml
{
    public class XmlEncoder
    {
        private static class XmlPredefinedEntities
        {
            public const string DoubleQuote = "&quot;";
            public const string Ampersand = "&amp;";
            public const string Apostraphe = "&apos;";
            public const string LessThan = "&lt;";
            public const string GreaterThan = "&gt;";
        }

        public static string Encode(string value)
        {
            if (string.IsNullOrEmpty(value))
            {
                return value;
            }

            var builder = new StringBuilder();

            char[] originalChars = value.Trim().ToCharArray();

            foreach (var c in originalChars)
            {
                switch ((byte)c)
                {
                    case 34:
                        builder.Append(XmlPredefinedEntities.DoubleQuote);
                        break;
                    case 38:
                        builder.Append(XmlPredefinedEntities.Ampersand);
                        break;
                    case 39:
                        builder.Append(XmlPredefinedEntities.Apostraphe);
                        break;
                    case 60:
                        builder.Append(XmlPredefinedEntities.LessThan);
                        break;
                    case 62:
                        builder.Append(XmlPredefinedEntities.GreaterThan);
                        break;
                    default:
                        builder.Append(c);
                        break;
                }
            }
            return builder.ToString();
        }

        /// <summary>
        /// Remove illegal XML characters from a string. For more info see this http://stackoverflow.com/a/12469826/52360
        /// </summary>
        public static string SanitizeForXml(string value)
        {
            if (string.IsNullOrEmpty(value))
            {
                return value;
            }

            var buffer = new StringBuilder(value.Length);

            foreach (char c in value)
            {
                if (IsLegalXmlChar(c))
                {
                    buffer.Append(c);
                }
            }

            return buffer.ToString();
        }

        /// <summary>
        /// Whether a given character is allowed by XML 1.0.
        /// </summary>
        public static bool IsLegalXmlChar(int character)
        {
            return
                (
                    character == 0x9 /* == '\t' == 9   */          ||
                    character == 0xA /* == '\n' == 10  */          ||
                    character == 0xD /* == '\r' == 13  */          ||
                    (character >= 0x20 && character <= 0xD7FF) ||
                    (character >= 0xE000 && character <= 0xFFFD) ||
                    (character >= 0x10000 && character <= 0x10FFFF)
                );
        }
    }
}
