using System;
using System.Linq;
using System.Xml.Linq;

namespace ByteDev.Common.Xml
{
    public static class XElementExtensions
    {
        public static bool HasDescendants(this XElement source, string elementName)
        {
            var xElements = source.Descendants(elementName);
            return xElements.Any();
        }

        public static string Read(this XElement source, string elementName)
        {
            var childElements = source.Descendants(elementName).ToList();
            if (!childElements.Any())
            {
                throw new NotSupportedException($"Element '{elementName}' does not exist.");
            }
            return childElements.First().Value;
        }

        public static string ReadSoft(this XElement source, string elementName)
        {
            var childElements = source.Descendants(elementName).ToList();
            return !childElements.Any() ? string.Empty : childElements.First().Value;
        }

        public static XElement GetSingleElement(this XElement element, string elementName)
        {
            var childElements = element.Descendants(elementName).ToList();
            if (childElements.Count != 1)
            {
                throw new NotSupportedException($"Found {childElements.Count} '{elementName}' elements in '{element}'.");
            }
            return childElements.First();
        }
    }
}
