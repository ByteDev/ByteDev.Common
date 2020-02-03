﻿using System.IO;
using System.Text.RegularExpressions;
using System.Xml;
using System.Xml.Schema;

namespace ByteDev.Common
{
    /// <summary>
    /// Extension methods for <see cref="T:System.String" />.
    /// </summary>
    public static class StringIsExtensions
    {
        /// <summary>
        /// Indicates whether this string is empty.
        /// </summary>
        /// <param name="source">The string to perform this operation on.</param>
        /// <returns>True if empty; otherwise returns false.</returns>
        public static bool IsEmpty(this string source)
        {
            if (source == null)
                return false;

            return source == string.Empty;
        }

        /// <summary>
        /// Indicates whether this string is null or empty.
        /// </summary>
        /// <param name="source">The string to perform this operation on.</param>
        /// <returns>True if null or empty; otherwise returns false.</returns>
        public static bool IsNullOrEmpty(this string source)
        {
            return string.IsNullOrEmpty(source);
        }

        /// <summary>
        /// Indicates whether this string is not null or empty.
        /// </summary>
        /// <param name="source">The string to perform this operation on.</param>
        /// <returns>True if not null or empty; otherwise returns false.</returns>
        public static bool IsNotNullOrEmpty(this string source)
        {
            return !string.IsNullOrEmpty(source);
        }

        /// <summary>
        /// Indicates whether this string is null or contains only white space characters.
        /// </summary>
        /// <param name="source">The string to perform this operation on.</param>
        /// <returns>True if null or only contains white space characters; otherwise returns false.</returns>
        public static bool IsNullOrWhitespaceOnly(this string source)
        {
            return string.IsNullOrEmpty(source) || string.IsNullOrEmpty(source.Trim());
        }

        /// <summary>
        /// Indicates whether this string is an email address.
        /// </summary>
        /// <param name="source">The string to perform this operation on.</param>
        /// <returns>True if is an email address; otherwise returns false.</returns>
        public static bool IsEmailAddress(this string source)
        {
            if (string.IsNullOrEmpty(source))
                return false;
            
            const string pattern = @"^(([^<>()[\]\\.,;:\s@\""]+"
                                   + @"(\.[^<>()[\]\\.,;:\s@\""]+)*)|(\"".+\""))@"
                                   + @"((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}"
                                   + @"\.[0-9]{1,3}\])|(([a-zA-Z\-0-9]+\.)+"
                                   + @"[a-zA-Z]{2,}))$";

            return new Regex(pattern).IsMatch(source);
        }

        /// <summary>
        /// Indicates whether this string is a URL.
        /// </summary>
        /// <param name="source">The string to perform this operation on.</param>
        /// <returns>True if is a URL; otherwise returns false.</returns>
        public static bool IsUrl(this string source)
        {
            if (string.IsNullOrEmpty(source))
                return false;
            
            return new Regex(@"http(s)?://([\w-]+\.)+[\w-]+(/[\w- ./?%&=]*)?").IsMatch(source);
        }

        /// <summary>
        /// Indicates whether this string is an IP address.
        /// </summary>
        /// <param name="source">The string to perform this operation on.</param>
        /// <returns>True if is an IP address; otherwise returns false.</returns>
        public static bool IsIpAddress(this string source)
        {
            if (source == null)
                return false;
            
            const string pattern = @"\b(25[0-5]|2[0-4][0-9]|[01]?[0-9][0-9]?)\.(25[0-5]|2[0-4][0-9]|[01]?[0-9][0-9]?)\.(25[0-5]|2[0-4][0-9]|[01]?[0-9][0-9]?)\.(25[0-5]|2[0-4][0-9]|[01]?[0-9][0-9]?)\b";

            return Regex.IsMatch(source, pattern);
        }

        /// <summary>
        /// Indicates whether this string is a GUID.
        /// </summary>
        /// <param name="source">The string to perform this operation on.</param>
        /// <returns>True if is a GUID; otherwise returns false.</returns>
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

        /// <summary>
        /// Indicates whether this string is XML.
        /// </summary>
        /// <param name="source">The string to perform this operation on.</param>
        /// <returns>True if is XML; otherwise returns false.</returns>
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