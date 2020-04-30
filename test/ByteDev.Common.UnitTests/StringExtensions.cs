using System;
using System.IO;
using System.Xml;
using System.Xml.Schema;

namespace ByteDev.Common.UnitTests
{
    internal static class StringExtensions
    {
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

        public static string FormatWith(this string source, params object[] args)
        {
            if(source == null)
                throw new ArgumentNullException(nameof(source));
            
            return string.Format(source, args);
        }
    }
}