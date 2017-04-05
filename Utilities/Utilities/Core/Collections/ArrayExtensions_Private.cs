using Mentula.Utilities.Core;
using System;

namespace Mentula.Utilities.Core.Collections
{
#if !DEBUG
    [System.Diagnostics.DebuggerStepThrough]
#endif
    public static partial class ArrayExtensions
    {
        private delegate bool InvalidCastPredicate<TSource, TResult>(TSource element, out TResult result);

        private static TResult[] CastInternal<TSource, TResult>(TSource[] source, InvalidCastPredicate<TSource, TResult> predicate)
        {
            NullCheck(source, predicate, true);

            TResult[] result = new TResult[source.Length];
            int j = 0;

            for (int i = 0; i < source.Length; i++)
            {
                if (!Utils.TryConvert(source[i], out result[j]))
                {
                    if (!predicate(source[i], out result[j])) continue;
                }

                j++;
            }

            if (j != source.Length) Array.Resize(ref result, j);
            return result;
        }

        private static TSource FirstInternal<TSource>(TSource[] source, Predicate<TSource> predicate, Func<TSource> onFailure)
        {
            for (int i = 0; i < source.Length; i++)
            {
                if (predicate(source[i])) return source[i];
            }

            return onFailure();
        }

        private static char FirstInternal(string source, Predicate<char> predicate, Func<char> onFailure)
        {
            for (int i = 0; i < source.Length; i++)
            {
                if (predicate(source[i])) return source[i];
            }

            return onFailure();
        }

        private static TSource LastInternal<TSource>(TSource[] source, Predicate<TSource> predicate, Func<TSource> onFailure)
        {
            int j = -1;

            for (int i = 0; i < source.Length; i++)
            {
                if (predicate(source[i])) j = i;
            }

            if (j == -1) return onFailure();
            return source[j];
        }

        private static char LastInternal(string source, Predicate<char> predicate, Func<char> onFailure)
        {
            int j = -1;

            for (int i = 0; i < source.Length; i++)
            {
                if (predicate(source[i])) j = i;
            }

            if (j == -1) return onFailure();
            return source[j];
        }

        private static void NullOrEmptyCheck<T>(T[] array, Delegate del = null, bool hasDel = false)
        {
            NullCheckInternal(array == null || array.Length < 1, del, hasDel);
        }

        private static void NullCheck<T>(T[] array, Delegate del = null, bool hasDel = false)
        {
            NullCheckInternal(array == null, del, hasDel);
        }

        private static void NullCheck(string array, Delegate del = null, bool hasDel = false)
        {
            NullCheckInternal(string.IsNullOrEmpty(array), del, hasDel);
        }

        private static void NullCheckInternal(bool arrayNull, Delegate del, bool hasDel)
        {
            string arg = arrayNull ? "array" : string.Empty;

            if (hasDel && del == null)
            {
                if (arrayNull) arg += " and ";
                arg += "delegate";
            }

            if (!string.IsNullOrEmpty(arg))
            {
                RaiseLinqException(nameof(NullCheckInternal), new ArgumentNullException($"The {arg} cannot be null!", (Exception)null));
            }
        }

        private static void RaiseLinqException(string method, Exception inner)
        {
            LoggedException.Raise(nameof(ArrayExtensions), $"{method} has encountered an unhandled exception", inner);
        }
    }
}