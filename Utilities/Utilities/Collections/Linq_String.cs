using Mentula.Utilities.Core;
using System;

namespace Mentula.Utilities.Collections
{
    public static partial class Linq
    {
        public static bool All(this string source, Predicate<char> selector)
        {
            NullCheck(source, selector, true);

            for (int i = 0; i < source.Length; i++)
            {
                if (!selector(source[i])) return false;
            }

            return true;
        }

        public static bool Any(this string source)
        {
            NullCheck(source);
            return source.Any(t => true);
        }

        public static bool Any(this string source, Predicate<char> selector)
        {
            NullCheck(source, selector, true);

            for (int i = 0; i < source.Length; i++)
            {
                if (selector(source[i])) return true;
            }

            return false;
        }

        public static string AppendIfNecessary(this string source, char value)
        {
            if (source[source.Length - 1] != value) return source + value;
            else return source;
        }

        public static bool Contains(this string source, char value)
        {
            NullCheck(source);
            return source.Contains(t => t.Equals(value));
        }

        public static bool Contains(this string source, Predicate<char> selector)
        {
            NullCheck(source, selector, true);

            for (int i = 0; i < source.Length; i++)
            {
                if (selector(source[i])) return true;
            }

            return false;
        }

        public static int Count(this string source, Predicate<char> selector)
        {
            int result = 0;

            for (int i = 0; i < source.Length; i++)
            {
                if (selector(source[i])) ++result;
            }

            return result;
        }

        public static char First(this string source)
        {
            NullCheck(source);
            return source.First(t => true);
        }

        public static char First(this string source, Predicate<char> selector)
        {
            NullCheck(source, selector, true);
            return FirstInternal(source, selector, () => 
            {
                RaiseLinqEsception(nameof(First), new InvalidOperationException("No match found!"));
                return '\0';
            });
        }

        public static char FirstOrDefault(this string source)
        {
            NullCheck(source);
            return source.FirstOrDefault(t => true);
        }

        public static char FirstOrDefault(this string source, Predicate<char> selector)
        {
            NullCheck(source, selector, true);
            return FirstInternal(source, selector, () => default(char));
        }

        public static int IndexOf(this string source, char value)
        {
            NullCheck(source);
            return source.IndexOf(t => t == value);
        }

        public static int IndexOf(this string source, Predicate<char> selector)
        {
            NullCheck(source, selector, true);

            for (int i = 0; i < source.Length; i++)
            {
                if (selector(source[i])) return i;
            }

            return -1;
        }

        public static char Last(this string source)
        {
            NullCheck(source);
            return source.Last(t => true);
        }

        public static char Last(this string source, Predicate<char> selector)
        {
            NullCheck(source, selector, true);
            return LastInternal(source, selector, () =>
            {
                RaiseLinqEsception(nameof(First), new InvalidOperationException("No match found!"));
                return '\0';
            });
        }

        public static char LastOrDefault(this string source)
        {
            NullCheck(source);
            return source.LastOrDefault(t => true);
        }

        public static char LastOrDefault(this string source, Predicate<char> selector)
        {
            NullCheck(source, selector, true);
            return LastInternal(source, selector, () => default(char));
        }

        public static string[] SplitAndKeep(this string source, params char[] seperators)
        {
            NullCheck(source);
            return source.SplitAndKeep(seperators, false);
        }

        public static string[] SplitAndKeep(this string source, char[] seperators, bool addToPrevious, StringSplitOptions options)
        {
            NullCheck(source);
            if (options == StringSplitOptions.None) return source.SplitAndKeep(seperators, addToPrevious);
            return source.SplitAndKeep(seperators, addToPrevious).RemoveNull(string.Empty);
        }

        public static string[] SplitAndKeep(this string source, char[] seperators, bool addToPrevious)
        {
            NullCheck(source);
            string[] result = new string[0];
            int start = 0, index;

            int startAdder = addToPrevious ? 0 : 1, endAdder = addToPrevious ? 0 : 1;
            while ((index = source.IndexOfAny(seperators, start)) != -1)
            {
                if (index - start > 0) result = result.Add(source.Substring(start - startAdder, index - start + 1));
                start = index == start ? index + 1 : index;
            }

            if (start < source.Length) result = result.Add(source.Substring(start - startAdder));
            return result;
        }

        public static string RemoveValue(this string source, char value)
        {
            return source.RemoveValue(value.ToString());
        }

        public static string RemoveValue(this string source, string value)
        {
            return source.Replace(value, string.Empty);
        }
    }
}