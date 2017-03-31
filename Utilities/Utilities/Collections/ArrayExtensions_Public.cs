using System;

namespace Mentula.Utilities.Collections
{
    public static partial class ArrayExtensions
    {
        /// <summary>
        /// Returns whether all elements of a specified collection meet the specified criteria.
        /// </summary>
        /// <typeparam name="TSource"> The type of elements. </typeparam>
        /// <param name="source"> The source array. </param>
        /// <param name="selector"> The set of criteria to be met. </param>
        /// <returns> <see langword="true"/> if all elements meet the criteria; otherwise <see langword="false"/>. </returns>
        /// <exception cref="Core.LoggedException"> The source or the selector was <see langword="null"/>. </exception>
        public static bool All<TSource>(this TSource[] source, Predicate<TSource> selector)
        {
            NullCheck(source, selector, true);

            for (int i = 0; i < source.Length; i++)
            {
                if (!selector(source[i])) return false;
            }

            return true;
        }

        /// <summary>
        /// Returns whether the collection contains at least 1 element.
        /// </summary>
        /// <typeparam name="TSource"> The type of elements. </typeparam>
        /// <param name="source"> The source array. </param>
        /// <returns> <see langword="true"/> of the collection contains at least 1 element; otherwise <see langword="false"/>. </returns>
        /// <exception cref="Core.LoggedException"> The source was <see langword="null"/>. </exception>
        public static bool Any<TSource>(this TSource[] source)
        {
            NullCheck(source);
            return source.Length > 0;
        }

        /// <summary>
        /// Returns whether at least 1 element of the collection meet the specified set of criteria.
        /// </summary>
        /// <typeparam name="TSource"> The type of elements. </typeparam>
        /// <param name="source"> The source array. </param>
        /// <param name="selector"> The set of criteria to be met. </param>
        /// <returns> <see langword="true"/> if at least 1 element meets to criteria; otherwise <see langword="false"/>. </returns>
        /// <exception cref="Core.LoggedException"> The source or the selector was <see langword="null"/>. </exception>
        public static bool Any<TSource>(this TSource[] source, Predicate<TSource> selector)
        {
            NullCheck(source, selector, true);

            for (int i = 0; i < source.Length; i++)
            {
                if (selector(source[i])) return true;
            }

            return false;
        }

        /// <summary>
        /// Attempts to cast all elements of the source to a specified type.
        /// </summary>
        /// <typeparam name="TSource"> The type of elements. </typeparam>
        /// <typeparam name="TResult"> The result type. </typeparam>
        /// <param name="source"> The source array. </param>
        /// <returns> An array containing all of the element of the source casted to the result type. </returns>
        /// <exception cref="Core.LoggedException"> The source was <see langword="null"/>. </exception>
        /// <exception cref="Core.LoggedException"> One of the elements could not be casted to the result type. </exception>
        public static TResult[] Cast<TSource, TResult>(this TSource[] source)
        {
            return CastInternal(source, (TSource s, out TResult r) =>
            {
                RaiseLinqException(nameof(Cast), new InvalidCastException($"Cannot cast type {typeof(TSource)} to {typeof(TResult)}!"));
                r = default(TResult);
                return false;
            });
        }

        /// <summary>
        /// Attempts to cast all elements of the source to a specified type,
        /// if this fails returns the default value of the result type.
        /// </summary>
        /// <typeparam name="TSource"> The type of elements. </typeparam>
        /// <typeparam name="TResult"> The result type. </typeparam>
        /// <param name="source"> The source array. </param>
        /// <returns> 
        /// An array containing all of the elements of the source casted to the result type.
        /// If the element cast failed returns the default value of the result type.
        /// </returns>
        /// <exception cref="Core.LoggedException"> The source was <see langword="null"/>. </exception>
        public static TResult[] CastOrDefault<TSource, TResult>(this TSource[] source)
        {
            return CastInternal(source, (TSource s, out TResult r) =>
            {
                r = default(TResult);
                return true;
            });
        }

        /// <summary>
        /// Attempts to cast all elements of the source to a specified type,
        /// if this fails skips the element.
        /// </summary>
        /// <typeparam name="TSource"> The type of elements. </typeparam>
        /// <typeparam name="TResult"> The result type. </typeparam>
        /// <param name="source"> The source array. </param>
        /// <returns> An array containing all of the element that were able to be casted the the result type. </returns>
        /// <exception cref="Core.LoggedException"> The source was <see langword="null"/>. </exception>
        public static TResult[] CastOrSkip<TSource, TResult>(this TSource[] source)
        {
            return CastInternal(source, (TSource s, out TResult r) =>
            {
                r = default(TResult);
                return false;
            });
        }

        /// <summary>
        /// Concatinates an element to the source collection.
        /// </summary>
        /// <typeparam name="TSource"> The type of elements. </typeparam>
        /// <param name="source"> The source array. </param>
        /// <param name="item"> The element to be added. </param>
        /// <returns> The collection after the element has been added. </returns>
        /// <exception cref="Core.LoggedException"> The source was <see langword="null"/>. </exception>
        public static TSource[] Concat<TSource>(this TSource[] source, TSource item)
        {
            NullCheck(source);

            int index = source.Length;
            Array.Resize(ref source, index + 1);

            source[index] = item;
            return source;
        }

        /// <summary>
        /// Concatinates two array's together.
        /// </summary>
        /// <typeparam name="TSource"> The type of elements. </typeparam>
        /// <param name="source"> The array to be added first. </param>
        /// <param name="collection"> The array to be added second. </param>
        /// <returns> An array that contains all of the elements of the source and the collection. </returns>
        /// <exception cref="Core.LoggedException"> The source or collection was <see langword="null"/>. </exception>
        public static TSource[] Concat<TSource>(this TSource[] source, TSource[] collection)
        {
            NullCheck(source);
            if (collection == null)
            {
                RaiseLinqException(nameof(Concat), new ArgumentNullException("collection", "The collection to concat cannot be null!"));
            }

            TSource[] result = new TSource[source.Length + collection.Length];
            source.CopyTo(result, 0);
            collection.CopyTo(result, source.Length);
            return result;
        }

        /// <summary>
        /// Concatinates a source array with a specified amount of collections.
        /// </summary>
        /// <typeparam name="TSource"> The type of elements. </typeparam>
        /// <param name="source"> The array to be added first. </param>
        /// <param name="collections"> The array's to be added in their specfied order. </param>
        /// <returns> An array that contains all of the elements of the source and the collections. </returns>
        /// <exception cref="Core.LoggedException"> The source, collections or an element in the collections was <see langword="null"/>. </exception>
        public static TSource[] Concat<TSource>(this TSource[] source, params TSource[][] collections)
        {
            NullCheck(source);
            if (collections == null) RaiseLinqException(nameof(Concat), new ArgumentNullException("args", "The collections to concat cannot be null!"));

            TSource[] result = source.Copy();
            for (int i = 0; i < collections.Length; i++)
            {
                TSource[] cur = collections[i];
                if (cur == null) RaiseLinqException(nameof(Concat), new ArgumentNullException("args", $"The collection at index {i} is null!"));

                int j = result.Length;
                Array.Resize(ref result, j + cur.Length);
                cur.CopyTo(result, j);
            }

            return result;
        }

        /// <summary>
        /// Returns whether the collection contains a specified element.
        /// </summary>
        /// <typeparam name="TSource"> The type of elements. </typeparam>
        /// <param name="source"> The array to check. </param>
        /// <param name="value"> The value to try an find. </param>
        /// <returns> <see langword="true"/> if the collections contains an element that meets the set of criteria; otherwise <see langword="false"/>. </returns>
        /// <exception cref="Core.LoggedException"> The source array was <see langword="null"/>. </exception>
        /// <exception cref="NullReferenceException"> An item in the source array was <see langword="null"/>. </exception>
        public static bool Contains<TSource>(this TSource[] source, TSource value)
        {
            NullCheck(source);
            return source.Contains(t => t.Equals(value));
        }

        /// <summary>
        /// Return whether the collections contains a elements the meets the specified set of criteria.
        /// </summary>
        /// <typeparam name="TSource"> The type of elements. </typeparam>
        /// <param name="source"> The source type. </param>
        /// <param name="selector"> The set of criteria to be met. </param>
        /// <returns> <see langword="true"/> when the collection contains an element that meets the criteria; otherwise <see langword="false"/>. </returns>
        /// <exception cref="Core.LoggedException"> The source or the selector was <see langword="null"/>. </exception>
        public static bool Contains<TSource>(this TSource[] source, Predicate<TSource> selector)
        {
            NullCheck(source, selector, true);

            for (int i = 0; i < source.Length; i++)
            {
                if (selector(source[i])) return true;
            }

            return false;
        }

        /// <summary>
        /// Returns the amount of elements in the collection that meet the specfied set of criteria.
        /// </summary>
        /// <typeparam name="TSource"> The type of elements. </typeparam>
        /// <param name="source"> The source array. </param>
        /// <param name="selector"> The set of criteria to be met. </param>
        /// <returns> The amount of elements in the collection that have met the specified set of criteria. </returns>
        /// <exception cref="Core.LoggedException"> The source or the selector was <see langword="null"/>. </exception>
        public static int Count<TSource>(this TSource[] source, Predicate<TSource> selector)
        {
            NullCheck(source);
            int result = 0;

            for (int i = 0; i < source.Length; i++)
            {
                if (selector(source[i])) ++result;
            }

            return result;
        }

        /// <summary>
        /// Return a copy of the source collection.
        /// </summary>
        /// <typeparam name="TSource"> The type of elements. </typeparam>
        /// <param name="source"> The source array. </param>
        /// <returns> A copy of the source collection. </returns>
        /// <exception cref="Core.LoggedException"> The source was <see langword="null"/>. </exception>
        public static TSource[] Copy<TSource>(this TSource[] source)
        {
            NullCheck(source);
            TSource[] result = new TSource[source.Length];
            source.CopyTo(result, 0);
            return result;
        }

        /// <summary>
        /// Returns the first element in the collection.
        /// </summary>
        /// <typeparam name="TSource"> The type fo elements. </typeparam>
        /// <param name="source"> The source array. </param>
        /// <returns> The first element in the collection. </returns>
        /// <exception cref="Core.LoggedException"> The source was <see langword="null"/> or empty. </exception>
        public static TSource First<TSource>(this TSource[] source)
        {
            NullOrEmptyCheck(source);
            return source[0];
        }

        /// <summary>
        /// Returns the first element in the collection that meets the specified set of criteria.
        /// </summary>
        /// <typeparam name="TSource"> The type of elements. </typeparam>
        /// <param name="source"> The source array. </param>
        /// <param name="selector"> The set of criteria to be met. </param>
        /// <returns> The first element of the collection to meet the criteria. </returns>
        /// <exception cref="Core.LoggedException"> The source or the selector was <see langword="null"/> or empty. </exception>
        /// <exception cref="Core.LoggedException"> No element matches the specified set of criteria. </exception>
        public static TSource First<TSource>(this TSource[] source, Predicate<TSource> selector)
        {
            NullOrEmptyCheck(source, selector, true);
            return FirstInternal(source, selector, () =>
            {
                RaiseLinqException(nameof(First), new InvalidOperationException("No match found!"));
                return default(TSource);
            });
        }

        /// <summary>
        /// Returns the first element in the collection that meets the specified set of criteria,
        /// Iif no such element if found; return the default value of the source type.
        /// </summary>
        /// <typeparam name="TSource"> The type of elements. </typeparam>
        /// <param name="source"> The source array. </param>
        /// <param name="selector"> The set of criteria to be met. </param>
        /// <returns> 
        /// The first element of the collection to meet the criteria,
        /// if no such element is found; return the default value of the source type.
        /// </returns>
        /// <exception cref="Core.LoggedException"> The source or the selector was <see langword="null"/>. </exception>
        public static TSource FirstOrDefault<TSource>(this TSource[] source, Predicate<TSource> selector)
        {
            NullCheck(source, selector, true);
            return FirstInternal(source, selector, () => default(TSource));
        }

        /// <summary>
        /// Returns the index of the specified element in the collection.
        /// </summary>
        /// <typeparam name="TSource"> The type of elements. </typeparam>
        /// <param name="source"> The source array. </param>
        /// <param name="value"> The value to get the index from. </param>
        /// <returns> The index of the specified value, if no such element is found; returns -1. </returns>
        /// <exception cref="Core.LoggedException"> The source was <see langword="null"/>. </exception>
        public static int IndexOf<TSource>(this TSource[] source, TSource value)
        {
            NullCheck(source);
            return source.IndexOf(t => t.Equals(value));
        }

        /// <summary>
        /// Return the index of the first element that meets the specified set of criteria.
        /// </summary>
        /// <typeparam name="TSource"> The type of elements. </typeparam>
        /// <param name="source"> The source array. </param>
        /// <param name="selector"> The set of criteria to be met. </param>
        /// <returns> The index of the first element to meet the specified criteria; otherwise -1. </returns>
        /// <exception cref="Core.LoggedException"> The source or the selector was <see langword="null"/>. </exception>
        public static int IndexOf<TSource>(this TSource[] source, Predicate<TSource> selector)
        {
            NullCheck(source, selector, true);

            for (int i = 0; i < source.Length; i++)
            {
                if (selector(source[i])) return i;
            }

            return -1;
        }

        /// <summary>
        /// Returns the last element in the collection.
        /// </summary>
        /// <typeparam name="TSource"> The type of elements. </typeparam>
        /// <param name="source"> The source array. </param>
        /// <returns> The last element in the collection. </returns>
        /// <exception cref="Core.LoggedException"> The source was <see langword="null"/> or empty. </exception>
        public static TSource Last<TSource>(this TSource[] source)
        {
            NullOrEmptyCheck(source);
            return source[source.Length - 1];
        }

        /// <summary>
        /// Returns the last element in the collection that meets the specified criteria.
        /// </summary>
        /// <typeparam name="TSource"> The type of elements. </typeparam>
        /// <param name="source"> The source array. </param>
        /// <param name="selector"> The set of cirteria to be met. </param>
        /// <returns> The last element in the collection that meets the criteria. </returns>
        /// <exception cref="Core.LoggedException"> The source or the selector was <see langword="null"/> or empty. </exception>
        public static TSource Last<TSource>(this TSource[] source, Predicate<TSource> selector)
        {
            NullOrEmptyCheck(source, selector, true);
            return LastInternal(source, selector, () =>
            {
                RaiseLinqException(nameof(Last), new InvalidOperationException("No match found!"));
                return default(TSource);
            });
        }

        /// <summary>
        /// Returns the last element in the collection that meets the specified criteria,
        /// if no such element if found; returns the default value of the source type.
        /// </summary>
        /// <typeparam name="TSource"> The type of elements. </typeparam>
        /// <param name="source"> The source array. </param>
        /// <param name="selector"> The set of criteria to be met </param>
        /// <returns> 
        /// The last element in the collection to meet the specified criteria,
        /// if no such element if found; returns the default value of the source type.
        /// </returns>
        /// <exception cref="Core.LoggedException"> The source or the selector was <see langword="null"/>. </exception>
        public static TSource LastOrDefault<TSource>(this TSource[] source, Predicate<TSource> selector)
        {
            NullCheck(source, selector, true);
            return LastInternal(source, selector, () => default(TSource));
        }

        /// <summary>
        /// Populates a new collection with a specified length contains only the specified element. 
        /// </summary>
        /// <typeparam name="T"> The type of elements. </typeparam>
        /// <param name="element"> The elements to be added. </param>
        /// <param name="count"> The length of the new collection. </param>
        /// <returns> A new collection populated only with the specified value. </returns>
        /// <exception cref="Core.LoggedException"> Count was smaller that zero. </exception>
        public static T[] Populate<T>(T element, int count)
        {
            if (count < 0) RaiseLinqException(nameof(Populate), new ArgumentException("count must be at least zero!"));
            T[] result = new T[count];

            for (int i = 0; i < count; i++)
            {
                result[i] = element;
            }

            return result;
        }

        /// <summary>
        /// Returns a random element in the collection.
        /// </summary>
        /// <typeparam name="TSource"> The type of elements. </typeparam>
        /// <param name="source"> The source array. </param>
        /// <returns> A random element in the collection. </returns>
        /// <exception cref="Core.LoggedException"> The source was <see langword="null"/> or empty. </exception>
        public static TSource Random<TSource>(this TSource[] source)
        {
            NullOrEmptyCheck(source);
            return source[Utils.Rng.Next(source.Length)];
        }

        /// <summary>
        /// Removes all elements equal to the specified value.
        /// </summary>
        /// <typeparam name="TSource"> The type of elements. </typeparam>
        /// <param name="source"> The source array. </param>
        /// <param name="value"> The value to be removed. </param>
        /// <returns> The source whithout elements equal to the specified value. </returns>
        /// <exception cref="Core.LoggedException"> The source was <see langword="null"/>. </exception>
        public static TSource[] Remove<TSource>(this TSource[] source, TSource value)
        {
            NullCheck(source);
            int newLength = source.Count(c => !Equals(c, value));
            if (newLength != source.Length)
            {
                TSource[] result = new TSource[newLength];

                for (int i = 0, j = 0; i < source.Length && j < newLength; i++)
                {
                    TSource old = source[i];
                    if (!Equals(old, value)) result[j++] = old;
                }

                return result;
            }

            return source;
        }

        /// <summary>
        /// Removes the element at the specified index.
        /// </summary>
        /// <typeparam name="TSource"> The type of elements. </typeparam>
        /// <param name="source"> The source array. </param>
        /// <param name="index"> The index at which to removed the element. </param>
        /// <returns> The source collection whithout the removed element. </returns>
        /// <exception cref="Core.LoggedException"> The source was <see langword="null"/>. </exception>
        public static TSource[] RemoveAt<TSource>(this TSource[] source, int index)
        {
            NullOrEmptyCheck(source);

            for (int i = index; i < source.Length - 1; i++)
            {
                source[i] = source[i + 1];
            }

            Array.Resize(ref source, source.Length - 1);
            return source;
        }

        /// <summary>
        /// Removes all elements equal to the default value of the source type.
        /// </summary>
        /// <typeparam name="TSource"> The type of elements. </typeparam>
        /// <param name="source"> The source array. </param>
        /// <returns> The source collection without the null (or default) values. </returns>
        /// <exception cref="Core.LoggedException"> The source was <see langword="null"/>. </exception>
        public static TSource[] RemoveNull<TSource>(this TSource[] source)
        {
            NullCheck(source);
            return source.Remove(default(TSource));
        }

        /// <summary>
        /// Reverses the collection.
        /// </summary>
        /// <typeparam name="TSource"> The type of elements. </typeparam>
        /// <param name="source"> The source array. </param>
        /// <returns> The source with all elements reversed. </returns>
        /// <exception cref="Core.LoggedException"> The source was <see langword="null"/>. </exception>
        public static TSource[] Reverse<TSource>(this TSource[] source)
        {
            NullCheck(source);
            TSource[] result = new TSource[source.Length];

            for (int i = 0, j = source.Length; i < source.Length; i++, j--)
            {
                result[i] = source[j];
            }

            return result;
        }

        /// <summary>
        /// Gets a specific part of the elements in the collection.
        /// </summary>
        /// <typeparam name="TSource"> The type of elements. </typeparam>
        /// <typeparam name="TResult"> The result type. </typeparam>
        /// <param name="source"> The source array. </param>
        /// <param name="selector"> A function that specifies what part of the elements to return. </param>
        /// <returns> The specified part of the source collection. </returns>
        /// <exception cref="Core.LoggedException"> The source or the selector was <see langword="null"/>. </exception>
        public static TResult[] Select<TSource, TResult>(this TSource[] source, Func<TSource, TResult> selector)
        {
            NullCheck(source, selector, true);
            TResult[] result = new TResult[source.Length];

            for (int i = 0; i < source.Length; i++)
            {
                result[i] = selector(source[i]);
            }

            return result;
        }

        /// <summary>
        /// Gets a specific part of the elements in the collection (also suplies the index of the current element).
        /// </summary>
        /// <typeparam name="TSource"> The type of elements. </typeparam>
        /// <typeparam name="TResult"> The result type. </typeparam>
        /// <param name="source"> The source array. </param>
        /// <param name="selector"> A function that specifies what part of the elements to return. </param>
        /// <returns> The specified part of the source collection. </returns>
        /// <exception cref="Core.LoggedException"> The source or the selector was <see langword="null"/>. </exception>
        public static TResult[] Select<TSource, TResult>(this TSource[] source, Func<TSource, int, TResult> selector)
        {
            NullCheck(source, selector, true);
            TResult[] result = new TResult[source.Length];

            for (int i = 0; i < source.Length; i++)
            {
                result[i] = selector(source[i], i);
            }

            return result;
        }

        /// <summary>
        /// Gets a specific part of the elements in the collection.
        /// </summary>
        /// <typeparam name="TSource"> The type of elements. </typeparam>
        /// <typeparam name="TResult"> The result type. </typeparam>
        /// <param name="source"> The source array. </param>
        /// <param name="selector"> A function that specifies what part of the elements to return. </param>
        /// <returns> The specified part of the source collection. </returns>
        /// <exception cref="Core.LoggedException"> The source or the selector was <see langword="null"/>. </exception>
        public static TResult[] Select<TSource, TResult>(this TSource[] source, Func<TSource, TResult[]> selector)
        {
            NullCheck(source, selector, true);
            TResult[] result = new TResult[0];

            for (int i = 0; i < source.Length; i++)
            {
                result.Concat(selector(source[i]));
            }

            return result;
        }

        /// <summary>
        /// Gets a specific part of the elements in the collection (also suplies the index of the current element).
        /// </summary>
        /// <typeparam name="TSource"> The type of elements. </typeparam>
        /// <typeparam name="TResult"> The result type. </typeparam>
        /// <param name="source"> The source array. </param>
        /// <param name="selector"> A function that specifies what part of the elements to return. </param>
        /// <returns> The specified part of the source collection. </returns>
        /// <exception cref="Core.LoggedException"> The source or the selector was <see langword="null"/>. </exception>
        public static TResult[] Select<TSource, TResult>(this TSource[] source, Func<TSource, int, TResult[]> selector)
        {
            NullCheck(source, selector, true);
            TResult[] result = new TResult[0];

            for (int i = 0; i < source.Length; i++)
            {
                result.Concat(selector(source[i], i));
            }

            return result;
        }

        /// <summary>
        /// Performce a basic bubblesort on the collection (performace warning).
        /// </summary>
        /// <typeparam name="TSource"> The type of elements. </typeparam>
        /// <param name="source"> The source array. </param>
        /// <param name="comparitor"> A function that returns whether the second given element should be before the first given element. </param>
        /// <returns> A sorted version of the source collection. </returns>
        /// <exception cref="Core.LoggedException"> The source or the comparitor was <see langword="null"/>. </exception>
        public static TSource[] Sort<TSource>(this TSource[] source, Func<TSource, TSource, bool> comparitor)
        {
            NullCheck(source, comparitor, true);
            TSource[] result = source.Copy();
            TSource temp;

            for (int write = 0; write < result.Length; write++)
            {
                for (int sort = 0; sort < result.Length - 1; sort++)
                {
                    if (comparitor(result[sort], result[sort + 1]))
                    {
                        temp = result[sort + 1];
                        result[sort + 1] = result[sort];
                        result[sort] = temp;
                    }
                }
            }

            return result;
        }

        /// <summary>
        /// Gets all elements in the collection that meet the specified criteria.
        /// </summary>
        /// <typeparam name="TSource"> The type of elements. </typeparam>
        /// <param name="source"> The source array. </param>
        /// <param name="selector"> The set of criteria to be met. </param>
        /// <returns> A collection containing only the elements that met the specified criteria. </returns>
        /// <exception cref="Core.LoggedException"> The source or the selector was <see langword="null"/>. </exception>
        public static TSource[] Where<TSource>(this TSource[] source, Predicate<TSource> selector)
        {
            NullCheck(source, selector, true);
            TSource[] result = new TSource[source.Length];
            int j = 0;

            for (int i = 0; i < source.Length; i++)
            {
                if (selector(source[i])) result[j++] = source[i];
            }

            Array.Resize(ref result, j);
            return result;
        }

        /// <summary>
        /// Gets all elements in the collection that meet the specified criteria (also suplies the index of the current element).
        /// </summary>
        /// <typeparam name="TSource"> The type of elements. </typeparam>
        /// <param name="source"> The source array. </param>
        /// <param name="selector"> The set of criteria to be met. </param>
        /// <returns> A collection containing only the elements that met the specified criteria. </returns>
        /// <exception cref="Core.LoggedException"> The source or the selector was <see langword="null"/>. </exception>
        public static TSource[] Where<TSource>(this TSource[] source, Func<TSource, int, bool> selector)
        {
            NullCheck(source, selector, true);
            TSource[] result = new TSource[source.Length];
            int j = 0;

            for (int i = 0; i < source.Length; i++)
            {
                if (selector(source[i], i)) result[j++] = source[i];
            }

            Array.Resize(ref result, j);
            return result;
        }
    }
}