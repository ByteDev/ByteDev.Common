using System.Text;

namespace ByteDev.Common.Xml
{
    /// <summary>
    /// Represents encoder of XML strings.
    /// </summary>
    public static class XmlEncoder
    {
        private static class XmlPredefinedEntities
        {
            public const string DoubleQuote = "&quot;";
            public const string Ampersand = "&amp;";
            public const string Apostraphe = "&apos;";
            public const string LessThan = "&lt;";
            public const string GreaterThan = "&gt;";
        }

        /// <summary>
        /// XML encode a string.
        /// </summary>
        /// <param name="value">The string to encode.</param>
        /// <returns>XML encoded string.</returns>
        public static string Encode(string value)
        {
            if (string.IsNullOrEmpty(value))
                return value;

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
        /// Remove illegal XML characters from <paramref name="value" />.
        /// </summary>
        /// <param name="value">String to sanitize.</param>
        /// <returns>Sanitized string.</returns>
        public static string SanitizeForXml(string value)
        {
            if (string.IsNullOrEmpty(value))
                return value;

            var buffer = new StringBuilder(value.Length);

            foreach (char c in value)
            {
                if (IsLegalXmlChar(c))
                    buffer.Append(c);
            }

            return buffer.ToString();
        }

        /// <summary>
        /// Indicates whether a given character is allowed by XML 1.0.
        /// </summary>
        /// <param name="character">Character to check.</param>
        /// <returns>True if the character is legal; otherwise returns false.</returns>
        public static bool IsLegalXmlChar(int character)
        {
            return
                (
                    character == 0x9 /* == '\t' == 9   */          ||
                    character == 0xA /* == '\n' == 10  */          ||
                    character == 0xD /* == '\r' == 13  */          ||
                    character >= 0x20 && character <= 0xD7FF ||
                    character >= 0xE000 && character <= 0xFFFD ||
                    character >= 0x10000 && character <= 0x10FFFF
                );
        }
    }
}
