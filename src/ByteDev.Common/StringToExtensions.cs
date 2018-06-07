using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace ByteDev.Common
{
    public static class StringToExtensions
    {
        public static string ToTitleCase(this string source)
        {
            if (source == null)
                throw new ArgumentNullException(nameof(source));

            return System.Globalization.CultureInfo.InvariantCulture.TextInfo.ToTitleCase(source);
        }

        public static IEnumerable<string> ToLines(this string source)
        {
            if (string.IsNullOrEmpty(source))
            {
                source = string.Empty;
            }
            using (var reader = new StringReader(source))
            {
                string line;
                while ((line = reader.ReadLine()) != null)
                {
                    yield return line;
                }
            }
        }

        public static byte[] ToByteArray(this string source)
        {
            return ToByteArray(source, new UTF8Encoding());
        }

        public static byte[] ToByteArray(this string source, System.Text.Encoding encoding)
        {
            if (string.IsNullOrEmpty(source))
            {
                return new byte[0];
            }
            return encoding.GetBytes(source);
        }

        public static int? ToIntOrDefault(this string source, int? defaultValue)
        {
            int result;
            return int.TryParse(source, out result) ? result : defaultValue;
        }

        public static long? ToLongOrDefault(this string source, long? defaultValue)
        {
            long result;
            return long.TryParse(source, out result) ? result : defaultValue;
        }

        public static DateTime? ToDateTimeOrDefault(this string source, DateTime? defaultValue)
        {
            DateTime result;
            return DateTime.TryParse(source, out result) ? result : defaultValue;
        }

        public static T ToEnum<T>(this string source) where T : struct
        {
            return (T)Enum.Parse(typeof(T), source, true);
        }
    }
}