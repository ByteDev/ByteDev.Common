using System;
using System.Text;
using System.Text.RegularExpressions;
namespace ByteDev.Common
{
    public static class StringExtensions
    {
        public static string RemoveStartsWith(this string source, string value)
        {
            if(value == null)
                throw new ArgumentNullException(nameof(value));
            if (value == string.Empty)
                return source;

            if (source.StartsWith(value))
                return source.Substring(value.Length);

            return source;
        }

        public static string RemoveEndsWith(this string source, string value)
        {
            if (value == null)
                throw new ArgumentNullException(nameof(value));
            if (value == string.Empty)
                return source;

            if (source.EndsWith(value))
                return source.Substring(0, source.Length - value.Length);

            return source;
        }

        public static string ReplaceToken(this string source, string tokenName, object value)
        {
            if (source == null)
                throw new ArgumentNullException(nameof(source));

            return source.Replace($"{{{tokenName}}}", value.ToString());
        }

        public static string FormatWith(this string source, params object[] args)
        {
            if(source == null)
                throw new ArgumentNullException(nameof(source));
            
            return string.Format(source, args);
        }

        public static string SafeSubstring(this string source, int startIndex, int length)
        {
            if (string.IsNullOrEmpty(source))
                return string.Empty;

            if (length < 1)
                return string.Empty;

            if (source.Length <= startIndex) 
                return string.Empty;

            if (source.Length - startIndex <= length) 
                return source.Substring(startIndex);

            return source.Substring(startIndex, length);
        }

        public static string Left(this string source, int length)
        {
            return source.SafeSubstring(0, length);
        }

        public static string Right(this string source, int length)
        {
            if (length > source.Length)
                return source;

            return source.SafeSubstring(source.Length - length, length);
        }

        /// <summary>
        /// Takes the length of characters from the left. Uses an appended
        /// ellipsis if the max length minus 3 is reached.
        /// </summary>
        public static string TakeFirstWithEllipsis(this string source, int maxLength)
        {
            if (source == null)
                throw new ArgumentNullException(nameof(source));

            if (maxLength < 1)
            {
                return string.Empty;
            }
            
            const string ellipsis = "...";

            if(maxLength <= ellipsis.Length)
            {
                throw new ArgumentOutOfRangeException(nameof(maxLength), $"Max length cannot be between 1 and {ellipsis.Length} (ellipsis length).");
            }
            
            if (maxLength < source.Length)
            {
                return source.Substring(0, maxLength - ellipsis.Length) + ellipsis;
            }
            return source;
        }

        /// <summary>
        /// Truncates the given string by stripping out the center and replacing it with an 
        /// elipsis so that the beginning and end of the string are retained. For example, 
        /// "This string has too many characters for its own good."InnerTruncate(32) yields 
        /// "This string has...its own good." 
        /// </summary>
        public static string InnerTruncate(this string source, int maxLength)
        {
            if (string.IsNullOrEmpty(source) || source.Length <= maxLength)
            {
                return source;
            }

            var charsInEachHalf = (maxLength - 3) / 2;

            var right = source.Substring(source.Length - charsInEachHalf, charsInEachHalf).TrimStart();

            var left = source.Substring(0, (maxLength - 3) - right.Length).TrimEnd();

            return $"{left}...{right}";
        }

        /// <summary>
        /// Removes all text between any brackets (and the brackets themselves).
        /// Example: "(Something) in (brackets) again" becomes " in  again".
        /// </summary>
        public static string RemoveBracketedText(this string source)
        {
            if (string.IsNullOrEmpty(source))
            {
                return source;
            }
            int posOpenBracket;

            while ((posOpenBracket = source.IndexOf("(", StringComparison.Ordinal)) >= 0)
            {
                var posCloseBracket = source.IndexOf(")", posOpenBracket, StringComparison.Ordinal);

                if (posCloseBracket > 0)
                {
                    source = source.Substring(0, posOpenBracket) + source.Substring(posCloseBracket + 1);
                }
                else
                {
                    source = source.Substring(0, posOpenBracket);
                    break;
                }
            }
            return source;
        }

        public static string RemoveWhiteSpace(this string source)
        {
            if (string.IsNullOrEmpty(source))
                return source;

            return Regex.Replace(source, @"\s+", "");
        }

        public static string ReplaceLastOccurance(this string source, string find, string replace)
        {
            if (string.IsNullOrEmpty(source))
                return source;

            var pos = source.LastIndexOf(find, StringComparison.InvariantCulture);

            if (pos <= 0)
            {
                return source;
            }
            return source.Remove(pos, find.Length).Insert(pos, replace);
        }

        public static string Pluralize(this string source, int number)
        {
            if (string.IsNullOrEmpty(source))
                return string.Empty;

            number = Math.Abs(number); // -1 should be singular, too
            return source + (number == 1 ? string.Empty : "s");
        }

        public static int CountOccurences(this string source, string value)
        {
            if (string.IsNullOrEmpty(value))
            {
                throw new ArgumentException("Value was null or empty.", nameof(value));
            }
            return Regex.Matches(source, Regex.Escape(value)).Count;
        }

        public static string Obscure(this string source, int beginCharsToShow, int endCharsToShow)
        {
            if (source == null)
                throw new ArgumentNullException(nameof(source));

            var len = source.Length;
            var sb = new StringBuilder(len);

            for (var pos = 0; pos < len; pos++)
            {
                sb.Append(pos < beginCharsToShow || len - pos <= endCharsToShow ? source[pos] : '*');
            }
            return sb.ToString();
        }

        public static string Repeat(this string source, int count)
        {
            if (source == null)
                throw new ArgumentNullException(nameof(source));

            return new StringBuilder().Insert(0, source, count).ToString();
        }

        public static string AddSlashSuffix(this string source)
        {
            if (source == null)
                return "/";

            if (!source.EndsWith("/"))
            {
                source += "/";
            }
            return source;
        }

        public static string RemoveSlashPrefix(this string source)
        {
            if (source == null)
                return null;

            if (source.StartsWith("/"))
            {
                source = source.Substring(1);
            }
            return source;
        }

        public static string Reverse(this string source)
        {
            if (source == null)
                return null;

            var charArray = source.ToCharArray();
            Array.Reverse(charArray);
            return new string(charArray);
        } 
    }
}

