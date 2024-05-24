﻿using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Arebis.Core.Extensions
{
    /// <summary>
    /// Provides extension methods to the String class.
    /// </summary>
    public static class StringExtensions
    {
        /// <summary>
        /// Returns the beginning of the value up to the given marker.
        /// If the marker is not present, returns the entire value.
        /// </summary>
        [return: NotNullIfNotNull("value")]
        public static string? UpTo(this string? value, string marker)
        {
            if (value == null) return null;

            int index = value.IndexOf(marker);
            if (index < 0)
                return value;
            else
                return value.Substring(0, index);
        }

        /// <summary>
        /// Returns an array with all index position where the searchedString appears.
        /// </summary>
        [return: NotNullIfNotNull("value")]
        public static int[]? AllIndexesOf(this string? value, string searchedString, StringComparison comparisonType)
        {
            if (value == null) return null;

            List<int> indexes = new List<int>();
            int pos = 0;
            while (true)
            {
                pos = value.IndexOf(searchedString, pos, comparisonType);
                if (pos == -1) break;
                indexes.Add(pos);
                pos += searchedString.Length;
            }
            return indexes.ToArray();
        }

        /// <summary>
        /// Returns a RegEx to match the given pattern with wildcards * and ?.
        /// </summary>
        public static Regex GetLikeRegex(this string pattern, RegexOptions options)
        {
            pattern = Regex.Escape(pattern);
            pattern = "^" + pattern.Replace("\\*", ".*").Replace("\\?", ".") + "$";
            return new Regex(pattern, options);
        }

        /// <summary>
        /// Whether the given string is a valid Email address.
        /// </summary>
        public static bool IsValidEmailAddress(this string str)
        {
            Regex regex = new Regex(@"^[\w-\.]+@([\w-]+\.)+[\w-]{2,4}$");
            return regex.IsMatch(str);
        }

        /// <summary>
        /// Performs a string pattern matching on a partern with wildcards * and ?.
        /// </summary>
        public static bool Like(this string? str, string pattern)
        {
            // Null never matches, but empty string or whitespaces could:
            if (str == null) return false;

            return GetLikeRegex(pattern, RegexOptions.IgnoreCase | RegexOptions.Singleline).IsMatch(str);
        }

        /// <summary>
        /// Returns a section of the string (similar to Substring() method, but with endindex instead of length argument.
        /// </summary>
        [return: NotNullIfNotNull("value")]
        public static string? Sectionstring(this string? value, int startIndex, int endIndex)
        {
            if (value == null) return null;

            return value.Substring(startIndex, endIndex - startIndex + 1);
        }

        /// <summary>
        /// Translates a PascalCased name into camelCase.
        /// </summary>
        [return: NotNullIfNotNull("value")]
        public static string? ToCamelCase(this string? value)
        {
            if (String.IsNullOrEmpty(value)) return value;

            char[] chars = value.ToCharArray();
            int conversionsDone = 0;
            for (int i = 0; i < chars.Length; i++)
            {
                if ((chars[i] >= 'A') && (chars[i] <= 'Z'))
                {
                    chars[i] = Char.ToLowerInvariant(chars[i]);
                    conversionsDone++;
                }
                else if (chars[i] == '_')
                {
                    continue;
                }
                else
                {
                    if (conversionsDone > 1)
                    {
                        chars[i - 1] = Char.ToUpperInvariant(chars[i - 1]);
                    }
                    break;
                }
            }
            return new String(chars);
        }

        /// <summary>
        /// Capitalizes every first letter of a word.
        /// </summary>
        /// <param name="value">The string to capitalize.</param>
        /// <param name="culture">Culture to use for capitalization. If null, uses current UI culture.</param>
        /// <param name="andLowerNextChars">Whether non-first word characters should be lowered.</param>
        /// <param name="capitalizeAfterSymbol">Whether to capitalize after a symbol, even if no whitespace was encountered.</param>
        [return: NotNullIfNotNull("value")]
        public static string? ToCapitalizedWords(this string? value, CultureInfo? culture = null, bool andLowerNextChars = false, bool capitalizeAfterSymbol = false)
        {
            if (String.IsNullOrWhiteSpace(value)) return value;

            culture = culture ?? CultureInfo.CurrentUICulture ?? CultureInfo.CurrentCulture ?? CultureInfo.InvariantCulture;

            var chars = value.ToCharArray();
            var capitalize = true;

            for (int i = 0; i < chars.Length; i++)
            {
                if (chars[i] == ' ')
                {
                    capitalize = true;
                }
                else if ("\'-+-*/.,;!?&".IndexOf(chars[i]) >= 0)
                {
                    if (capitalizeAfterSymbol)
                        capitalize = true;
                }
                else if (capitalize)
                {
                    chars[i] = Char.ToUpper(chars[i], culture);
                    capitalize = false;
                }
                else if (andLowerNextChars)
                {
                    chars[i] = Char.ToLower(chars[i], culture);
                }
            }

            return new String(chars);
        }

        /// <summary>
        /// Translates the (multiline) text into an array of lines.
        /// </summary>
        public static string[] ToLines(this string? text)
        {
            if (text == null) return new string[0];

            string temp = text;
            temp = temp.Replace("\r\n", "\n");
            temp = temp.Replace('\r', '\n');
            return temp.Split('\n');
        }

        /// <summary>
        /// Truncates the string to the given max length.
        /// </summary>
        [return: NotNullIfNotNull("value")]
        public static string? Truncate(this string? value, int maxLength, bool trimEnd = false)
        {
            if (value == null) return null;

            string result;
            if (value.Length <= maxLength) result = value;
            else result = value.Substring(0, maxLength);

            if (trimEnd) result = result.TrimEnd(' ', '\r', '\n', '\t');
            return result;
        }

        /// <summary>
        /// Translates the string with a switch/case like structure.
        /// First arg is the string to compare to, second arg is the return value
        /// if the string matches the first arg.
        /// If the count of cases is odd, the last value represents a default value,
        /// if the count of cases is even, when no match, null is returned.
        /// </summary>
        public static string? CaseTranslate(this string? str, StringComparison comparisonType, params string?[] cases)
        {
            // Search for a matching case:
            for (int i = 0; i < cases.Length - 1; i += 2)
            {
                if (String.Equals(str, cases[i], comparisonType))
                    return cases[i + 1];
            }

            // If no matching case found, return default value:
            if ((cases.Length % 2) == 1)
                return cases[^1];
            else
                return null;
        }

        /// <summary>
        /// Returns the given string shortened (truncated) to the given max length. If shortened, a tail can be appended.
        /// I.e. "United Kingdom".MaxLength(12, "...") would return "United Ki...".
        /// </summary>
        [return: NotNullIfNotNull("value")]
        public static string? MaxLength(this string? value, int length, string? tailOnTrunc = null)
        {
            if (value == null) return null;
            if (value.Length <= length) return value;
            if (tailOnTrunc == null)
            {
                return value[..length];
            }
            else
            {
                return string.Concat(value.AsSpan(0, length - tailOnTrunc.Length), tailOnTrunc);
            }
        }

        /// <summary>
        /// Shortens the given string to the given length minus the length of the prefix and
        /// the postfix. The returned string is made of prefix + shortened string + postfix.
        /// </summary>
        public static string Shorten(this string? s, int length, string? prefix, string? postfix)
        {
            return prefix + Shorten(s, length - (prefix?.Length ?? 0) - (postfix?.Length ?? 0)) + postfix;
        }

        /// <summary>
        /// Shortens the given string to the given length if needed. This is done by first 
        /// removing vowels, then, if still too long, trimming the string.
        /// </summary>
        [return: NotNullIfNotNull("value")]
        public static string? Shorten(this string? value, int length)
        {
            if (value == null) return null;

            if (value.Length <= length) return value;

            int toremove = value.Length - length;

            // First try to, remove vowels at the end:
            StringBuilder sb = new StringBuilder(value.Length);
            char[] vowels = new char[] { 'A', 'E', 'I', 'O', 'U', 'a', 'e', 'i', 'o', 'u' };
            int pos = value.Length;
            while (pos > 0)
            {

                pos--;

                if (toremove > 0)
                {
                    // Skip vowels except first one or one just before an underscore:
                    if (Array.Exists(vowels, (Predicate<char>)delegate (char c) { return c == value[pos]; })
                        && (pos > 0)
                        && (value[pos - 1] != '_')
                        )
                    {
                        toremove--;
                        continue;
                    }
                }

                // Append next char (or all next chars if done):
                if (toremove > 0)
                {
                    sb.Insert(0, value[pos]);
                }
                else
                {
                    // Append vowels & non-vowels one name sufficiently shortened:
                    sb.Insert(0, value.Substring(0, pos + 1));
                    pos = 0;
                }
            }

            // If sufficiently reduced, return it:
            if (sb.Length <= length) return sb.ToString();

            // Otherwise, trim last part:
            return sb.ToString().Substring(0, length);
        }

        /// <summary>
        /// Generates an identifier based on the given string. The identifier is
        /// guaranteed to start with a letter or an underscore, and contains only
        /// letters, numbers and underscores.
        /// </summary>
        [return: NotNullIfNotNull("value")]
        public static string? ToIdentifier(this string? value)
        {
            if (value == null) return null;

            StringBuilder identifier = new StringBuilder(value.Length);
            int pos = -1;
            foreach (char c in value.ToCharArray())
            {
                pos++;
                if (((c >= '0') && (c <= '9')))
                {
                    if (pos == 0) identifier.Append("Id");
                    identifier.Append(c);
                }
                else if (((c >= 'a') && (c <= 'z')) || ((c >= 'A') && (c <= 'Z')))
                    identifier.Append(c);
                else if (c == ' ')
                    identifier.Append("");
                else
                    identifier.Append('_');
            }
            if (identifier.Length == 0) identifier.Append("BlankIdentifier");
            return identifier.ToString();
        }

        /// <summary>
        /// Returns the requested substring, unless the string is too short, then returns
        /// the best match. If the string is null, returns null.
        /// </summary>
        [return: NotNullIfNotNull("value")]
        public static string? TrySubstring(this string? value, int startIndex, int length)
        {
            if (value == null)
                return value;
            else if (value.Length <= startIndex)
                return String.Empty;
            else if (value.Length < (startIndex + length))
                return value.Substring(startIndex);
            else
                return value.Substring(startIndex, length);
        }

        /// <summary>
        /// If null or empty, return alternative value.
        /// </summary>
        [return: NotNullIfNotNull(nameof(altValue))]
        public static string? IfNullOrEmpty(this string? value, string? altValue)
        {
            return (String.IsNullOrEmpty(value)) ? altValue : value;
        }

        /// <summary>
        /// If null or white space, return alternative value.
        /// </summary>
        [return:NotNullIfNotNull(nameof(altValue))]
        public static string? IfNullOrWhiteSpace(this string? value, string? altValue)
        {
            return (String.IsNullOrWhiteSpace(value)) ? altValue : value;
        }

        /// <summary>
        /// Returns string chuncks of equal size (except for the first or last part that could be shorter).
        /// </summary>
        /// <param name="str">The string to chunck.</param>
        /// <param name="chunkSize">The size of each chunck.</param>
        /// <param name="alignRight">Whether to align left or right. Right align means the first chunk is allowed to be shorter, while left align means the last chunck is allowed to be shorter.</param>
        /// <returns>Chuncks of the given string.</returns>
        public static IEnumerable<string> Chunked(this string? str, int chunkSize, bool alignRight = false)
        {
            if (str == null) yield break;
            if (str.Length == 0) yield break;
            if (chunkSize <= 0) { yield return str; yield break; }

            int chuncks = str.Length / chunkSize;
            int lastlen = str.Length % chunkSize;
            if (alignRight && lastlen > 0)
            {
                yield return str.Substring(0, lastlen);
                for (int i = lastlen; i < str.Length; i += chunkSize)
                {
                    yield return str.Substring(i, chunkSize);
                }
            }
            else
            {
                var upTo = str.Length - lastlen - 1;
                for (int i = 0; i <= upTo; i += chunkSize)
                {
                    yield return str.Substring(i, chunkSize);
                }
                if (lastlen > 0)
                    yield return str.Substring(str.Length - lastlen);
            }
        }

        /// <summary>
        /// Removes all not listed chars from the given value.
        /// </summary>
        /// <param name="value">The value to 'clean'.</param>
        /// <param name="allowedChars">A string listing all allowed chars. Groups "0-9", "a-z" and "A-Z" are supported. Pipe chars (|) are removed unless a double pipe is present. I.e: "0123456789ABCDEFabcdef" for hex numbers. I.e: "0-9|A-Z|,|.||" allowing numbers, capital letters, comma, dot and pipe.</param>
        /// <returns>The cleaned value.</returns>
        [return: NotNullIfNotNull("value")]
        public static string? UsingOnlyChars(this string? value, string allowedChars)
        {
            if (String.IsNullOrEmpty(value)) return value;

            // Could enhance performance by caching latest allowedChars input strings and their substituted result.

            allowedChars = allowedChars.Replace("0-9", "0123456789");
            allowedChars = allowedChars.Replace("a-z", "abcdefghijklmnopqrstuvwxyz");
            allowedChars = allowedChars.Replace("A-Z", "ABCDEFGHIJKLMNOPQRSTUVWXYZ");
            var containsDoublePipe = allowedChars.Contains("||");
            allowedChars = allowedChars.Replace("|", "");
            if (containsDoublePipe) allowedChars = allowedChars + "|";

            var result = new StringBuilder(value.Length);
            foreach (var c in value)
            {
                if (allowedChars.IndexOf(c) >= 0)
                    result.Append(c);
            }

            return result.ToString();
        }

        /// <summary>
        /// Returns the string with only characters between upper and lowerbound ASCII codes. Other characters are replaced by the replacementChar.
        /// I.e. "Cérémony".ToAsciiString(32, 127, '?') => "C?r?mony"
        /// </summary>
        /// <param name="value">The value to transform.</param>
        /// <param name="lowerbound">Lowest ASCII code to allow.</param>
        /// <param name="upperbound">Highest ASCII code to allow.</param>
        /// <param name="replacementChar">Unallowed characters are replaced by this one.</param>
        [return: NotNullIfNotNull("value")]
        public static string? ToAsciiString(this string? value, int lowerbound, int upperbound, char replacementChar)
        {
            if (String.IsNullOrEmpty(value)) return value;

            var chars = value.ToCharArray();

            for (int i = 0; i < chars.Length; i++)
            {
                if ((int)chars[i] < lowerbound)
                {
                    chars[i] = replacementChar;
                }
                else if ((int)chars[i] > upperbound)
                {
                    chars[i] = replacementChar;
                }
            }

            return new String(chars);
        }

        /// <summary>
        /// If the string equals the match, return the substitution, otherwise return the original string.
        /// I.e to replace empty string by null: someString?.Trim().If("", null)
        /// </summary>
        public static string? If(this string? str, string? match, string? substitution)
        {
            if (str == null && match == null) return substitution;
            else if (str == null) return null;
            else if (str.Equals(match)) return substitution;
            else return str;
        }

        /// <summary>
        /// Converts the given base name to a name that does not exist in the existingNames collection.
        /// It does this by either returning the base name itself, or appending " (2)" or a higher number
        /// until a unique value is found.
        /// </summary>
        /// <param name="baseName">The base name.</param>
        /// <param name="existingNames">Existing names to check uniqueness.</param>
        /// <returns>A string based on baseName that does not appear in existingNames.</returns>
        public static string ToUniqueNameWithin(this string baseName, IEnumerable<string> existingNames)
        {
            if (existingNames.Contains(baseName))
            {
                var i = 2;
                var hs = existingNames.ToHashSet();
                while(true)
                {
                    var name = baseName + " (" + i++ + ")";
                    if (!hs.Contains(name)) return name;
                }
            }
            else
            {
                return baseName;
            }
        }
    }
}
