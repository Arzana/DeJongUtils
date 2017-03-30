using System;

namespace Mentula.Utilities.Collections
{
    public static partial class ArrayExtensions
    {
        public static TSource[] Add<TSource>(this TSource[] source, TSource item)
        {
            NullCheck(source);

            int index = source.Length;
            Array.Resize(ref source, index + 1);

            source[index] = item;
            return source;
        }

        public static TSource[] AddRange<TSource>(this TSource[] source, params TSource[] items)
        {
            NullCheck(source);
            return source.Concat(items);
        }

        public static bool All<TSource>(this TSource[] source, Predicate<TSource> selector)
        {
            NullCheck(source, selector, true);

            for (int i = 0; i < source.Length; i++)
            {
                if (!selector(source[i])) return false;
            }

            return true;
        }

        public static bool Any<TSource>(this TSource[] source)
        {
            NullCheck(source);
            return source.Any(t => true);
        }

        public static bool Any<TSource>(this TSource[] source, Predicate<TSource> selector)
        {
            NullCheck(source, selector, true);

            for (int i = 0; i < source.Length; i++)
            {
                if (selector(source[i])) return true;
            }

            return false;
        }

        public static TResult[] Cast<TSource, TResult>(this TSource[] source)
        {
            return CastInternal(source, (TSource s, out TResult r) =>
            {
                RaiseLinqEsception(nameof(Cast), new InvalidCastException($"Cannot cast type {typeof(TSource)} to {typeof(TResult)}!"));
                r = default(TResult);
                return false;
            });
        }

        public static TResult[] CastOrDefault<TSource, TResult>(this TSource[] source)
        {
            return CastInternal(source, (TSource s, out TResult r) =>
            {
                r = default(TResult);
                return true;
            });
        }

        public static TResult[] CastOrSkip<TSource, TResult>(this TSource[] source)
        {
            return CastInternal(source, (TSource s, out TResult r) =>
            {
                r = default(TResult);
                return false;
            });
        }

        public static TSource[] Concat<TSource>(this TSource[] source, TSource[] collection)
        {
            NullCheck(source);
            if (collection == null)
            {
                RaiseLinqEsception(nameof(Concat), new ArgumentNullException("collection", "The collection to concat cannot be null!"));
            }

            TSource[] result = new TSource[source.Length + collection.Length];
            source.CopyTo(result, 0);
            collection.CopyTo(result, source.Length);
            return result;
        }

        public static TSource[] Concat<TSource>(this TSource[] source, params TSource[][] args)
        {
            NullCheck(source);
            if (args == null) RaiseLinqEsception(nameof(Concat), new ArgumentNullException("args", "The collections to concat cannot be null!"));

            TSource[] result = source.Copy();
            for (int i = 0; i < args.Length; i++)
            {
                TSource[] cur = args[i];
                if (cur == null) RaiseLinqEsception(nameof(Concat), new ArgumentNullException("args", $"The collection at index {i} is null!"));

                int j = result.Length;
                Array.Resize(ref result, j + cur.Length);
                cur.CopyTo(result, j);
            }

            return result;
        }

        public static bool Contains<TSource>(this TSource[] source, TSource value)
        {
            NullCheck(source);
            return source.Contains(t => t.Equals(value));
        }

        public static bool Contains<TSource>(this TSource[] source, Predicate<TSource> selector)
        {
            NullCheck(source, selector, true);

            for (int i = 0; i < source.Length; i++)
            {
                if (selector(source[i])) return true;
            }

            return false;
        }

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

        public static TSource[] Copy<TSource>(this TSource[] source)
        {
            NullCheck(source);
            TSource[] result = new TSource[source.Length];
            source.CopyTo(result, 0);
            return result;
        }

        public static TSource First<TSource>(this TSource[] source)
        {
            NullCheck(source);
            return source.First(t => true);
        }

        public static TSource First<TSource>(this TSource[] source, Predicate<TSource> selector)
        {
            NullCheck(source, selector, true);
            return FirstInternal(source, selector, () => { throw new InvalidOperationException("No match found!"); });
        }

        public static TSource FirstOrDefault<TSource>(this TSource[] source)
        {
            NullCheck(source);
            return source.FirstOrDefault(t => true);
        }

        public static TSource FirstOrDefault<TSource>(this TSource[] source, Predicate<TSource> selector)
        {
            NullCheck(source, selector, true);
            return FirstInternal(source, selector, () => default(TSource));
        }

        public static int IndexOf<TSource>(this TSource[] source, TSource value)
        {
            NullCheck(source);
            return source.IndexOf(t => t.Equals(value));
        }

        public static int IndexOf<TSource>(this TSource[] source, Predicate<TSource> selector)
        {
            NullCheck(source, selector, true);

            for (int i = 0; i < source.Length; i++)
            {
                if (selector(source[i])) return i;
            }

            return -1;
        }

        public static TSource Last<TSource>(this TSource[] source)
        {
            NullCheck(source);
            return source.Last(t => true);
        }

        public static TSource Last<TSource>(this TSource[] source, Predicate<TSource> selector)
        {
            NullCheck(source, selector, true);
            return LastInternal(source, selector, () => 
            {
                RaiseLinqEsception(nameof(Last), new InvalidOperationException("No match found!"));
                return default(TSource);
            });
        }

        public static TSource LastOrDefault<TSource>(this TSource[] source)
        {
            NullCheck(source);
            return source.LastOrDefault(t => true);
        }

        public static TSource LastOrDefault<TSource>(this TSource[] source, Predicate<TSource> selector)
        {
            NullCheck(source, selector, true);
            return LastInternal(source, selector, () => default(TSource));
        }

        public static TSource Random<TSource>(this TSource[] source)
        {
            NullCheck(source);
            return source[Utils.Rng.Next(source.Length)];
        }

        public static TSource[] RemoveAt<TSource>(this TSource[] source, int index)
        {
            NullOrEmptyCheck(source);
            int newLength = source.Length - 1;

            if (index != newLength)
            {
                TSource last = source[newLength];
                source[index] = last;
            }

            Array.Resize(ref source, newLength);
            return source;
        }

        public static TSource[] RemoveNull<TSource>(this TSource[] source)
        {
            NullCheck(source);
            return source.RemoveNull(default(TSource));
        }

        public static TSource[] RemoveNull<TSource>(this TSource[] source, TSource nullValue)
        {
            NullCheck(source);
            int newLength = source.Count(c => !Equals(c, nullValue));
            if (newLength != source.Length)
            {
                TSource[] result = new TSource[newLength];

                for (int i = 0, j = 0; i < source.Length && j < newLength; i++)
                {
                    TSource old = source[i];
                    if (!Equals(old, nullValue)) result[j++] = old;
                }

                return result;
            }

            return source;
        }

        public static T[] Repeat<T>(T element, int count)
        {
            if (count < 0) RaiseLinqEsception(nameof(Repeat), new ArgumentException("count must be at least zero!"));
            T[] result = new T[count];

            for (int i = 0; i < count; i++)
            {
                result[i] = element;
            }

            return result;
        }

        public static TSource[] Reverse<TSource>(this TSource[] source)
        {
            TSource[] result = new TSource[source.Length];

            for (int i = 0, j = source.Length; i < source.Length; i++, j--)
            {
                result[i] = source[j];
            }

            return result;
        }

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

        public static TResult[] SelectMany<TSource, TResult>(this TSource[] source, Func<TSource, TResult[]> selector)
        {
            NullCheck(source, selector, true);
            TResult[] result = new TResult[0];

            for (int i = 0; i < source.Length; i++)
            {
                result.Concat(selector(source[i]));
            }

            return result;
        }

        public static TResult[] SelectMany<TSource, TResult>(this TSource[] source, Func<TSource, int, TResult[]> selector)
        {
            NullCheck(source, selector, true);
            TResult[] result = new TResult[0];

            for (int i = 0; i < source.Length; i++)
            {
                result.Concat(selector(source[i], i));
            }

            return result;
        }

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