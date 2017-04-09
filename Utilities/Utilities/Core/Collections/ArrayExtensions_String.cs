using System;

namespace Mentula.Utilities.Core.Collections
{
    public static partial class ArrayExtensions
    {
        /// <summary>
        /// Returns whether all elements of a specified <see cref="string"/> meet the specified criteria.
        /// </summary>
        /// <param name="source"> The source <see cref="string"/>. </param>
        /// <param name="selector"> The set of criteria to be met. </param>
        /// <returns> <see langword="true"/> if all elements meet the criteria; otherwise <see langword="false"/>. </returns>
        /// <exception cref="Core.LoggedException"> The source or the selector was <see langword="null"/>. </exception>
        public static bool All(this string source, Predicate<char> selector)
        {
            NullCheck(source, selector, true);

            for (int i = 0; i < source.Length; i++)
            {
                if (!selector(source[i])) return false;
            }

            return true;
        }

        /// <summary>
        /// Returns whether at least 1 element of the <see cref="string"/> meet the specified set of criteria.
        /// </summary>
        /// <param name="source"> The source <see cref="string"/>. </param>
        /// <param name="selector"> The set of criteria to be met. </param>
        /// <returns> <see langword="true"/> if at least 1 element meets to criteria; otherwise <see langword="false"/>. </returns>
        /// <exception cref="Core.LoggedException"> The source or the selector was <see langword="null"/>. </exception>
        public static bool Any(this string source, Predicate<char> selector)
        {
            NullCheck(source, selector, true);

            for (int i = 0; i < source.Length; i++)
            {
                if (selector(source[i])) return true;
            }

            return false;
        }

        /// <summary>
        /// Appends a specified <see cref="char"/> to a <see cref="string"/> but only if is doesn't end with it already.
        /// </summary>
        /// <param name="source"> The source <see cref="string"/>. </param>
        /// <param name="value"> The specfic <see cref="char"/> to be added. </param>
        /// <returns> The result <see cref="string"/>. </returns>
        /// <exception cref="Core.LoggedException"> The source was <see langword="null"/>. </exception>
        public static string AppendIfNecessary(this string source, char value)
        {
            NullCheck(source);

            if (source[source.Length - 1] != value) return source + value;
            else return source;
        }

        /// <summary>
        /// Appends a specified <see cref="char"/> to a <see cref="string"/>
        /// but only if it doesn't end with one of the valid suffixes already.
        /// </summary>
        /// <param name="source"> The source <see cref="string"/>. </param>
        /// <param name="validEnds"> The valid suffixes. </param>
        /// <param name="value"> The specific <see cref="char"/> to be added. </param>
        /// <returns> The result <see cref="string"/>. </returns>
        /// <exception cref="Core.LoggedException"> The source was <see langword="null"/>. </exception>
        public static string AppendIfNecessary(this string source, char[] validEnds, char value)
        {
            NullCheck(source);
            char end = source[source.Length - 1];

            for (int i = 0; i < validEnds.Length; i++)
            {
                if (end == validEnds[i]) return source;
            }

            return source + value;
        }

        /// <summary>
        /// Returns whether the <see cref="string"/> contains a specified element.
        /// </summary>
        /// <param name="source"> The source <see cref="string"/>. </param>
        /// <param name="value"> The value to try an find. </param>
        /// <returns> <see langword="true"/> if the <see cref="string"/> contains an element that meets the set of criteria; otherwise <see langword="false"/>. </returns>
        /// <exception cref="Core.LoggedException"> The source array was <see langword="null"/>. </exception>
        public static bool Contains(this string source, char value)
        {
            NullCheck(source);
            return source.Contains(t =>  t == value);
        }

        /// <summary>
        /// Return whether the <see cref="string"/> contains a elements the meets the specified set of criteria.
        /// </summary>
        /// <param name="source"> The source <see cref="string"/>. </param>
        /// <param name="selector"> The set of criteria to be met. </param>
        /// <returns> <see langword="true"/> when the <see cref="string"/> contains an element that meets the criteria; otherwise <see langword="false"/>. </returns>
        /// <exception cref="Core.LoggedException"> The source or the selector was <see langword="null"/>. </exception>
        public static bool Contains(this string source, Predicate<char> selector)
        {
            NullCheck(source, selector, true);

            for (int i = 0; i < source.Length; i++)
            {
                if (selector(source[i])) return true;
            }

            return false;
        }

        /// <summary>
        /// Returns the amount of elements in the <see cref="string"/> that meet the specfied set of criteria.
        /// </summary>
        /// <param name="source"> The source <see cref="string"/>. </param>
        /// <param name="selector"> The set of criteria to be met. </param>
        /// <returns> The amount of elements in the <see cref="string"/> that have met the specified set of criteria. </returns>
        /// <exception cref="Core.LoggedException"> The source or the selector was <see langword="null"/>. </exception>
        public static int Count(this string source, Predicate<char> selector)
        {
            NullCheck(source, selector, true);
            int result = 0;

            for (int i = 0; i < source.Length; i++)
            {
                if (selector(source[i])) ++result;
            }

            return result;
        }

        /// <summary>
        /// Returns the first element in the <see cref="string"/>.
        /// </summary>
        /// <param name="source"> The source <see cref="string"/>. </param>
        /// <returns> The first element in the <see cref="string"/>. </returns>
        /// <exception cref="Core.LoggedException"> The source was <see langword="null"/> or empty. </exception>
        public static char First(this string source)
        {
            NullOrEmptyCheck(source);
            return source[0];
        }

        /// <summary>
        /// Returns the first element in the <see cref="string"/> that meets the specified set of criteria.
        /// </summary>
        /// <param name="source"> The source <see cref="string"/>. </param>
        /// <param name="selector"> The set of criteria to be met. </param>
        /// <returns> The first element of the <see cref="string"/> to meet the criteria. </returns>
        /// <exception cref="Core.LoggedException"> The source or the selector was <see langword="null"/>. </exception>
        /// <exception cref="Core.LoggedException"> No element matches the specified set of criteria. </exception>
        public static char First(this string source, Predicate<char> selector)
        {
            NullCheck(source, selector, true);
            return FirstInternal(source, selector, () => 
            {
                RaiseLinqException(nameof(First), new InvalidOperationException("No match found!"));
                return default(char);
            });
        }

        /// <summary>
        /// Returns the first element in the <see cref="string"/> that meets the specified set of criteria,
        /// Iif no such element if found; return '\0'.
        /// </summary>
        /// <param name="source"> The source <see cref="string"/>. </param>
        /// <param name="selector"> The set of criteria to be met. </param>
        /// <returns> 
        /// The first element of the <see cref="string"/> to meet the criteria,
        /// if no such element is found; return '\0'.
        /// </returns>
        /// <exception cref="Core.LoggedException"> The source or the selector was <see langword="null"/>. </exception>
        public static char FirstOrDefault(this string source, Predicate<char> selector)
        {
            NullCheck(source, selector, true);
            return FirstInternal(source, selector, () => default(char));
        }

        /// <summary>
        /// Returns the index of the specified element in the <see cref="string"/>.
        /// </summary>
        /// <param name="source"> The source <see cref="string"/>. </param>
        /// <param name="value"> The value to get the index from. </param>
        /// <returns> The index of the specified value, if no such element is found; returns -1. </returns>
        /// <exception cref="Core.LoggedException"> The source was <see langword="null"/>. </exception>
        public static int IndexOf(this string source, char value)
        {
            NullCheck(source);
            return source.IndexOf(t => t == value);
        }

        /// <summary>
        /// Return the index of the first element that meets the specified set of criteria.
        /// </summary>
        /// <param name="source"> The source <see cref="string"/>. </param>
        /// <param name="selector"> The set of criteria to be met. </param>
        /// <returns> The index of the first element to meet the specified criteria; otherwise -1. </returns>
        /// <exception cref="Core.LoggedException"> The source or the selector was <see langword="null"/>. </exception>
        public static int IndexOf(this string source, Predicate<char> selector)
        {
            NullCheck(source, selector, true);

            for (int i = 0; i < source.Length; i++)
            {
                if (selector(source[i])) return i;
            }

            return -1;
        }

        /// <summary>
        /// Returns the last element in the <see cref="string"/>.
        /// </summary>
        /// <param name="source"> The source <see cref="string"/>. </param>
        /// <returns> The last element in the <see cref="string"/>. </returns>
        /// <exception cref="Core.LoggedException"> The source was <see langword="null"/> or empty. </exception>
        public static char Last(this string source)
        {
            NullOrEmptyCheck(source);
            return source[source.Length - 1];
        }

        /// <summary>
        /// Returns the last element in the <see cref="string"/> that meets the specified criteria.
        /// </summary>
        /// <param name="source"> The source <see cref="string"/>. </param>
        /// <param name="selector"> The set of cirteria to be met. </param>
        /// <returns> The last element in the <see cref="string"/> that meets the criteria. </returns>
        /// <exception cref="Core.LoggedException"> The source or the selector was <see langword="null"/>. </exception>
        public static char Last(this string source, Predicate<char> selector)
        {
            NullCheck(source, selector, true);
            return LastInternal(source, selector, () =>
            {
                RaiseLinqException(nameof(First), new InvalidOperationException("No match found!"));
                return default(char);
            });
        }

        /// <summary>
        /// Returns the last element in the <see cref="string"/> that meets the specified criteria,
        /// if no such element if found; returns '\0'.
        /// </summary>
        /// <param name="source"> The source <see cref="string"/>. </param>
        /// <param name="selector"> The set of criteria to be met </param>
        /// <returns> 
        /// The last element in the <see cref="string"/> to meet the specified criteria,
        /// if no such element if found; returns '\0'.
        /// </returns>
        /// <exception cref="Core.LoggedException"> The source or the selector was <see langword="null"/>. </exception>
        public static char LastOrDefault(this string source, Predicate<char> selector)
        {
            NullCheck(source, selector, true);
            return LastInternal(source, selector, () => default(char));
        }

        /// <summary>
        /// Splits a <see cref="string"/> into substrings that are based on the characters in an array (keeps the separators).
        /// </summary>
        /// <param name="source"> The source <see cref="string"/>. </param>
        /// <param name="seperators"> 
        /// A character array that delimits the substrings in the <see cref="string"/>,
        /// an empty array that contains no delimiters, or <see langword="null"/>.
        /// </param>
        /// <returns>
        /// An array whose elements contain the substrings from this instance that are delimited
        /// by one or more characters in separator.
        /// </returns>
        /// <exception cref="Core.LoggedException"> The source was <see langword="null"/>. </exception>
        public static string[] SplitAndKeep(this string source, params char[] seperators)
        {
            NullCheck(source);
            return source.SplitAndKeep(seperators, false);
        }

        /// <summary>
        /// Splits a <see cref="string"/> into substrings based on the characters in an array (keeps the separators).
        /// You can specify whether the substrings include empty array elements.
        /// </summary>
        /// <param name="source"> The source <see cref="string"/>. </param>
        /// <param name="seperators">
        /// A character array that delimits the substrings in this <see cref="string"/>,
        /// an empty array that contains no delimiters, or <see langword="null"/>.
        /// </param>
        /// <param name="addToPrevious"> Whether to add the separators to the previous substring or the next substring. </param>
        /// <param name="options">
        /// <see cref="StringSplitOptions.RemoveEmptyEntries"/> to omit empty array elements from the array returned;
        /// or <see cref="StringSplitOptions.None"/> to include empty array elements in the array returned.
        /// </param>
        /// <returns>
        /// An array whose elements contain the substrings in this <see cref="string"/> that are delimited
        /// by one or more characters in separator.
        /// </returns>
        /// <exception cref="ArgumentException"> option is not one of the <see cref="StringSplitOptions"/> values. </exception>
        /// <exception cref="Core.LoggedException"> The source was <see langword="null"/>. </exception>
        public static string[] SplitAndKeep(this string source, char[] seperators, bool addToPrevious, StringSplitOptions options)
        {
            NullCheck(source);
            if (options == StringSplitOptions.None) return source.SplitAndKeep(seperators, addToPrevious);
            return source.SplitAndKeep(seperators, addToPrevious).Remove(string.Empty);
        }

        /// <summary>
        /// Splits a <see cref="string"/> into substrings based on the characters in an array (keeps the separators).
        /// </summary>
        /// <param name="source"> The source <see cref="string"/>. </param>
        /// <param name="seperators">
        /// A character array that delimits the substrings in this <see cref="string"/>,
        /// an empty array that contains no delimiters, or <see langword="null"/>.
        /// </param>
        /// <param name="addToPrevious"> Whether to add the separators to the previous substring or the next substring. </param>
        /// <returns>
        /// An array whose elements contain the substrings in this <see cref="string"/> that are delimited
        /// by one or more characters in separator.
        /// </returns>
        /// <exception cref="Core.LoggedException"> The source was <see langword="null"/>. </exception>
        public static string[] SplitAndKeep(this string source, char[] seperators, bool addToPrevious)
        {
            NullCheck(source);
            string[] result = new string[0];
            int start = 0, index;

            int startAdder = addToPrevious ? 0 : 1, endAdder = addToPrevious ? 0 : 1;
            while ((index = source.IndexOfAny(seperators, start)) != -1)
            {
                if (index - start > 0) result = result.Concat(source.Substring(start - startAdder, index - start + 1));
                start = index == start ? index + 1 : index;
            }

            if (start < source.Length) result = result.Concat(source.Substring(start - startAdder));
            return result;
        }

        /// <summary>
        /// Removes all elements equal to the specified <see cref="char"/>s from the <see cref="string"/>.
        /// </summary>
        /// <param name="source"> The source <see cref="string"/>. </param>
        /// <param name="values"> The <see cref="char"/>s to be removed. </param>
        /// <returns> An <see cref="string"/> without the specified <see cref="char"/>s. </returns>
        public static string Remove(this string source, params char[] values)
        {
            string result = source;

            for (int i = 0; i < values.Length; i++)
            {
                result = result.Replace(values[i].ToString(), string.Empty);
            }

            return result;
        }

        /// <summary>
        /// Removes all substrings equal to the specified substrings from the <see cref="string"/>.
        /// </summary>
        /// <param name="source"> The source <see cref="string"/>. </param>
        /// <param name="values"> The substrings to be removed. </param>
        /// <returns> An <see cref="string"/> without the specified substrings. </returns>
        public static string Remove(this string source, params string[] values)
        {
            string result = source;

            for (int i = 0; i < values.Length; i++)
            {
                result = result.Replace(values[i], string.Empty);
            }

            return result;
        }
    }
}