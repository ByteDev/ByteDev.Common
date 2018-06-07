using System.IO;
using System.Text.RegularExpressions;
using System.Xml;
using System.Xml.Schema;

namespace ByteDev.Common
{
    public static class StringIsExtensions
    {
        public static bool IsEmpty(this string source)
        {
            if (source == null)
            {
                return false;
            }
            return source == string.Empty;
        }

        public static bool IsNullOrEmpty(this string source)
        {
            return string.IsNullOrEmpty(source);
        }

        public static bool IsNotNullOrEmpty(this string source)
        {
            return !string.IsNullOrEmpty(source);
        }

        public static bool IsNullOrWhitespaceOnly(this string source)
        {
            return string.IsNullOrEmpty(source) || string.IsNullOrEmpty(source.Trim());
        }

        public static bool IsEmailAddress(this string source)
        {
            if (string.IsNullOrEmpty(source))
            {
                return false;
            }
            const string pattern = @"^(([^<>()[\]\\.,;:\s@\""]+"
                                   + @"(\.[^<>()[\]\\.,;:\s@\""]+)*)|(\"".+\""))@"
                                   + @"((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}"
                                   + @"\.[0-9]{1,3}\])|(([a-zA-Z\-0-9]+\.)+"
                                   + @"[a-zA-Z]{2,}))$";

            var regex = new Regex(pattern);
            return regex.IsMatch(source);
        }

        public static bool IsUrl(this string source)
        {
            if (string.IsNullOrEmpty(source))
                return false;

            var regex = new Regex(@"http(s)?://([\w-]+\.)+[\w-]+(/[\w- ./?%&=]*)?");
            return regex.IsMatch(source);
        }

        public static bool IsIpAddress(this string source)
        {
            if (source == null)
                return false;
            
            const string pattern = @"\b(25[0-5]|2[0-4][0-9]|[01]?[0-9][0-9]?)\.(25[0-5]|2[0-4][0-9]|[01]?[0-9][0-9]?)\.(25[0-5]|2[0-4][0-9]|[01]?[0-9][0-9]?)\.(25[0-5]|2[0-4][0-9]|[01]?[0-9][0-9]?)\b";

            return Regex.IsMatch(source, pattern);
        }

        public static bool IsGuid(this string source)
        {
            if (string.IsNullOrEmpty(source))
                return false;

            const string pattern = "^[A-Fa-f0-9]{32}$|" +
                                   "^({|\\()?[A-Fa-f0-9]{8}-([A-Fa-f0-9]{4}-){3}[A-Fa-f0-9]{12}(}|\\))?$|" +
                                   "^({)?[0xA-Fa-f0-9]{3,10}(, {0,1}[0xA-Fa-f0-9]{3,6}){2}, {0,1}({)([0xA-Fa-f0-9]{3,4}, {0,1}){7}[0xA-Fa-f0-9]{3,4}(}})$";

            var format = new Regex(pattern);
            var match = format.Match(source);

            return match.Success;
        }

        public static bool IsXml(this string source)
        {
            if (string.IsNullOrEmpty(source))
                return false;

            var settings = new XmlReaderSettings
            {
                CheckCharacters = true,
                ConformanceLevel = ConformanceLevel.Document,
                DtdProcessing = DtdProcessing.Ignore,
                IgnoreComments = true,
                IgnoreProcessingInstructions = true,
                IgnoreWhitespace = true,
                ValidationFlags = XmlSchemaValidationFlags.None,
                ValidationType = ValidationType.None,
            };

            using (var reader = XmlReader.Create(new StringReader(source), settings))
            {
                try
                {
                    while (reader.Read()) { }
                    return true;
                }
                catch (XmlException)
                {
                    return false;
                }
            }
        }

    }
}