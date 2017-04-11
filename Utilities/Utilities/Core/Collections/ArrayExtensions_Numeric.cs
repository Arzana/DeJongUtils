namespace DeJong.Utilities.Core.Collections
{
    public static partial class ArrayExtensions
    {
        /// <summary>
        /// Returns the average of the specified numeric collection.
        /// </summary>
        /// <param name="source"> The collection to take the average from. </param>
        /// <returns> The average of the collection as a <see cref="float"/>. </returns>
        /// <exception cref="Core.LoggedException"> The source was <see langword="null"/>. </exception>
        public static float Average(this sbyte[] source)
        {
            NullCheck(source);
            return source.Sum() / (float)source.Length;
        }

        /// <summary>
        /// Returns the average of the specified numeric collection.
        /// </summary>
        /// <param name="source"> The collection to take the average from. </param>
        /// <returns> The average of the collection as a <see cref="float"/>. </returns>
        /// <exception cref="Core.LoggedException"> The source was <see langword="null"/>. </exception>
        public static float Average(this byte[] source)
        {
            NullCheck(source);
            return source.Sum() / (float)source.Length;
        }

        /// <summary>
        /// Returns the average of the specified numeric collection.
        /// </summary>
        /// <param name="source"> The collection to take the average from. </param>
        /// <returns> The average of the collection as a <see cref="float"/>. </returns>
        /// <exception cref="Core.LoggedException"> The source was <see langword="null"/>. </exception>
        public static float Average(this short[] source)
        {
            NullCheck(source);
            return source.Sum() / (float)source.Length;
        }

        /// <summary>
        /// Returns the average of the specified numeric collection.
        /// </summary>
        /// <param name="source"> The collection to take the average from. </param>
        /// <returns> The average of the collection as a <see cref="float"/>. </returns>
        /// <exception cref="Core.LoggedException"> The source was <see langword="null"/>. </exception>
        public static float Average(this int[] source)
        {
            NullCheck(source);
            return source.Sum() / (float)source.Length;
        }

        /// <summary>
        /// Returns the average of the specified numeric collection.
        /// </summary>
        /// <param name="source"> The collection to take the average from. </param>
        /// <returns> The average of the collection as a <see cref="float"/>. </returns>
        /// <exception cref="Core.LoggedException"> The source was <see langword="null"/>. </exception>
        public static float Average(this long[] source)
        {
            NullCheck(source);
            return source.Sum() / (float)source.Length;
        }

        /// <summary>
        /// Returns the average of the specified numeric collection.
        /// </summary>
        /// <param name="source"> The collection to take the average from. </param>
        /// <returns> The average of the collection as a <see cref="float"/>. </returns>
        /// <exception cref="Core.LoggedException"> The source was <see langword="null"/>. </exception>
        public static float Average(this ushort[] source)
        {
            NullCheck(source);
            return source.Sum() / (float)source.Length;
        }

        /// <summary>
        /// Returns the average of the specified numeric collection.
        /// </summary>
        /// <param name="source"> The collection to take the average from. </param>
        /// <returns> The average of the collection as a <see cref="float"/>. </returns>
        /// <exception cref="Core.LoggedException"> The source was <see langword="null"/>. </exception>
        public static float Average(this uint[] source)
        {
            NullCheck(source);
            return source.Sum() / (float)source.Length;
        }

        /// <summary>
        /// Returns the average of the specified numeric collection.
        /// </summary>
        /// <param name="source"> The collection to take the average from. </param>
        /// <returns> The average of the collection as a <see cref="float"/>. </returns>
        /// <exception cref="Core.LoggedException"> The source was <see langword="null"/>. </exception>
        public static float Average(this ulong[] source)
        {
            NullCheck(source);
            return source.Sum() / (float)source.Length;
        }

        /// <summary>
        /// Returns the average of the specified numeric collection.
        /// </summary>
        /// <param name="source"> The collection to take the average from. </param>
        /// <returns> The average of the collection as a <see cref="float"/>. </returns>
        /// <exception cref="Core.LoggedException"> The source was <see langword="null"/>. </exception>
        public static decimal Average(this decimal[] source)
        {
            NullCheck(source);
            return source.Sum() / source.Length;
        }

        /// <summary>
        /// Returns the average of the specified numeric collection.
        /// </summary>
        /// <param name="source"> The collection to take the average from. </param>
        /// <returns> The average of the collection as a <see cref="float"/>. </returns>
        /// <exception cref="Core.LoggedException"> The source was <see langword="null"/>. </exception>
        public static float Average(this float[] source)
        {
            NullCheck(source);
            return source.Sum() / source.Length;
        }

        /// <summary>
        /// Returns the average of the specified numeric collection.
        /// </summary>
        /// <param name="source"> The collection to take the average from. </param>
        /// <returns> The average of the collection as a <see cref="float"/>. </returns>
        /// <exception cref="Core.LoggedException"> The source was <see langword="null"/>. </exception>
        public static double Average(this double[] source)
        {
            NullCheck(source);
            return source.Sum() / (float)source.Length;
        }

        /// <summary>
        /// Returns the biggest element of the specified numeric collection.
        /// </summary>
        /// <param name="source"> The collection to use. </param>
        /// <returns> The biggest element in the collection. </returns>
        /// <exception cref="Core.LoggedException"> The source was <see langword="null"/>. </exception>
        public static sbyte Max(this sbyte[] source)
        {
            NullOrEmptyCheck(source);
            sbyte max = source[0];

            for (int i = 1; i < source.Length; i++)
            {
                if (source[i] > max) max = source[i];
            }

            return max;
        }

        /// <summary>
        /// Returns the biggest element of the specified numeric collection.
        /// </summary>
        /// <param name="source"> The collection to use. </param>
        /// <returns> The biggest element in the collection. </returns>
        /// <exception cref="Core.LoggedException"> The source was <see langword="null"/> or empty. </exception>
        public static byte Max(this byte[] source)
        {
            NullOrEmptyCheck(source);
            byte max = source[0];

            for (int i = 1; i < source.Length; i++)
            {
                if (source[i] > max) max = source[i];
            }

            return max;
        }

        /// <summary>
        /// Returns the biggest element of the specified numeric collection.
        /// </summary>
        /// <param name="source"> The collection to use. </param>
        /// <returns> The biggest element in the collection. </returns>
        /// <exception cref="Core.LoggedException"> The source was <see langword="null"/> or empty. </exception>
        public static short Max(this short[] source)
        {
            NullOrEmptyCheck(source);
            short max = source[0];

            for (int i = 1; i < source.Length; i++)
            {
                if (source[i] > max) max = source[i];
            }

            return max;
        }

        /// <summary>
        /// Returns the biggest element of the specified numeric collection.
        /// </summary>
        /// <param name="source"> The collection to use. </param>
        /// <returns> The biggest element in the collection. </returns>
        /// <exception cref="Core.LoggedException"> The source was <see langword="null"/> or empty. </exception>
        public static int Max(this int[] source)
        {
            NullOrEmptyCheck(source);
            int max = source[0];

            for (int i = 1; i < source.Length; i++)
            {
                if (source[i] > max) max = source[i];
            }

            return max;
        }

        /// <summary>
        /// Returns the biggest element of the specified numeric collection.
        /// </summary>
        /// <param name="source"> The collection to use. </param>
        /// <returns> The biggest element in the collection. </returns>
        /// <exception cref="Core.LoggedException"> The source was <see langword="null"/> or empty. </exception>
        public static long Max(this long[] source)
        {
            NullOrEmptyCheck(source);
            long max = source[0];

            for (int i = 1; i < source.Length; i++)
            {
                if (source[i] > max) max = source[i];
            }

            return max;
        }

        /// <summary>
        /// Returns the biggest element of the specified numeric collection.
        /// </summary>
        /// <param name="source"> The collection to use. </param>
        /// <returns> The biggest element in the collection. </returns>
        /// <exception cref="Core.LoggedException"> The source was <see langword="null"/> or empty. </exception>
        public static ushort Max(this ushort[] source)
        {
            NullOrEmptyCheck(source);
            ushort max = source[0];

            for (int i = 1; i < source.Length; i++)
            {
                if (source[i] > max) max = source[i];
            }

            return max;
        }

        /// <summary>
        /// Returns the biggest element of the specified numeric collection.
        /// </summary>
        /// <param name="source"> The collection to use. </param>
        /// <returns> The biggest element in the collection. </returns>
        /// <exception cref="Core.LoggedException"> The source was <see langword="null"/> or empty. </exception>
        public static uint Max(this uint[] source)
        {
            NullOrEmptyCheck(source);
            uint max = source[0];

            for (int i = 1; i < source.Length; i++)
            {
                if (source[i] > max) max = source[i];
            }

            return max;
        }

        /// <summary>
        /// Returns the biggest element of the specified numeric collection.
        /// </summary>
        /// <param name="source"> The collection to use. </param>
        /// <returns> The biggest element in the collection. </returns>
        /// <exception cref="Core.LoggedException"> The source was <see langword="null"/> or empty. </exception>
        public static ulong Max(this ulong[] source)
        {
            NullOrEmptyCheck(source);
            ulong max = source[0];

            for (int i = 1; i < source.Length; i++)
            {
                if (source[i] > max) max = source[i];
            }

            return max;
        }

        /// <summary>
        /// Returns the biggest element of the specified numeric collection.
        /// </summary>
        /// <param name="source"> The collection to use. </param>
        /// <returns> The biggest element in the collection. </returns>
        /// <exception cref="Core.LoggedException"> The source was <see langword="null"/> or empty. </exception>
        public static decimal Max(this decimal[] source)
        {
            NullOrEmptyCheck(source);
            decimal max = source[0];

            for (int i = 1; i < source.Length; i++)
            {
                if (source[i] > max) max = source[i];
            }

            return max;
        }

        /// <summary>
        /// Returns the biggest element of the specified numeric collection.
        /// </summary>
        /// <param name="source"> The collection to use. </param>
        /// <returns> The biggest element in the collection. </returns>
        /// <exception cref="Core.LoggedException"> The source was <see langword="null"/> or empty. </exception>
        public static float Max(this float[] source)
        {
            NullOrEmptyCheck(source);
            float max = source[0];

            for (int i = 1; i < source.Length; i++)
            {
                if (source[i] > max) max = source[i];
            }

            return max;
        }

        /// <summary>
        /// Returns the biggest element of the specified numeric collection.
        /// </summary>
        /// <param name="source"> The collection to use. </param>
        /// <returns> The biggest element in the collection. </returns>
        /// <exception cref="Core.LoggedException"> The source was <see langword="null"/> or empty. </exception>
        public static double Max(this double[] source)
        {
            NullOrEmptyCheck(source);
            double max = source[0];

            for (int i = 1; i < source.Length; i++)
            {
                if (source[i] > max) max = source[i];
            }

            return max;
        }

        /// <summary>
        /// Returns the smallest element of the specified numeric collection.
        /// </summary>
        /// <param name="source"> The collection to use. </param>
        /// <returns> The smallest element in the collection. </returns>
        /// <exception cref="Core.LoggedException"> The source was <see langword="null"/> or empty. </exception>
        public static sbyte Min(this sbyte[] source)
        {
            NullOrEmptyCheck(source);
            sbyte min = source[0];

            for (int i = 1; i < source.Length; i++)
            {
                if (source[i] < min) min = source[i];
            }

            return min;
        }

        /// <summary>
        /// Returns the smallest element of the specified numeric collection.
        /// </summary>
        /// <param name="source"> The collection to use. </param>
        /// <returns> The smallest element in the collection. </returns>
        /// <exception cref="Core.LoggedException"> The source was <see langword="null"/> or empty. </exception>
        public static byte Min(this byte[] source)
        {
            NullOrEmptyCheck(source);
            byte min = source[0];

            for (int i = 1; i < source.Length; i++)
            {
                if (source[i] < min) min = source[i];
            }

            return min;
        }

        /// <summary>
        /// Returns the smallest element of the specified numeric collection.
        /// </summary>
        /// <param name="source"> The collection to use. </param>
        /// <returns> The smallest element in the collection. </returns>
        /// <exception cref="Core.LoggedException"> The source was <see langword="null"/> or empty. </exception>
        public static short Min(this short[] source)
        {
            NullOrEmptyCheck(source);
            short min = source[0];

            for (int i = 1; i < source.Length; i++)
            {
                if (source[i] < min) min = source[i];
            }

            return min;
        }

        /// <summary>
        /// Returns the smallest element of the specified numeric collection.
        /// </summary>
        /// <param name="source"> The collection to use. </param>
        /// <returns> The smallest element in the collection. </returns>
        /// <exception cref="Core.LoggedException"> The source was <see langword="null"/> or empty. </exception>
        public static int Min(this int[] source)
        {
            NullOrEmptyCheck(source);
            int min = source[0];

            for (int i = 1; i < source.Length; i++)
            {
                if (source[i] < min) min = source[i];
            }

            return min;
        }

        /// <summary>
        /// Returns the smallest element of the specified numeric collection.
        /// </summary>
        /// <param name="source"> The collection to use. </param>
        /// <returns> The smallest element in the collection. </returns>
        /// <exception cref="Core.LoggedException"> The source was <see langword="null"/> or empty. </exception>
        public static long Min(this long[] source)
        {
            NullOrEmptyCheck(source);
            long min = source[0];

            for (int i = 1; i < source.Length; i++)
            {
                if (source[i] < min) min = source[i];
            }

            return min;
        }

        /// <summary>
        /// Returns the smallest element of the specified numeric collection.
        /// </summary>
        /// <param name="source"> The collection to use. </param>
        /// <returns> The smallest element in the collection. </returns>
        /// <exception cref="Core.LoggedException"> The source was <see langword="null"/> or empty. </exception>
        public static ushort Min(this ushort[] source)
        {
            NullOrEmptyCheck(source);
            ushort min = source[0];

            for (int i = 1; i < source.Length; i++)
            {
                if (source[i] < min) min = source[i];
            }

            return min;
        }

        /// <summary>
        /// Returns the smallest element of the specified numeric collection.
        /// </summary>
        /// <param name="source"> The collection to use. </param>
        /// <returns> The smallest element in the collection. </returns>
        /// <exception cref="Core.LoggedException"> The source was <see langword="null"/> or empty. </exception>
        public static uint Min(this uint[] source)
        {
            NullOrEmptyCheck(source);
            uint min = source[0];

            for (int i = 1; i < source.Length; i++)
            {
                if (source[i] < min) min = source[i];
            }

            return min;
        }

        /// <summary>
        /// Returns the smallest element of the specified numeric collection.
        /// </summary>
        /// <param name="source"> The collection to use. </param>
        /// <returns> The smallest element in the collection. </returns>
        /// <exception cref="Core.LoggedException"> The source was <see langword="null"/> or empty. </exception>
        public static ulong Min(this ulong[] source)
        {
            NullOrEmptyCheck(source);
            ulong min = source[0];

            for (int i = 1; i < source.Length; i++)
            {
                if (source[i] < min) min = source[i];
            }

            return min;
        }

        /// <summary>
        /// Returns the smallest element of the specified numeric collection.
        /// </summary>
        /// <param name="source"> The collection to use. </param>
        /// <returns> The smallest element in the collection. </returns>
        /// <exception cref="Core.LoggedException"> The source was <see langword="null"/> or empty. </exception>
        public static decimal Min(this decimal[] source)
        {
            NullOrEmptyCheck(source);
            decimal min = source[0];

            for (int i = 1; i < source.Length; i++)
            {
                if (source[i] < min) min = source[i];
            }

            return min;
        }

        /// <summary>
        /// Returns the smallest element of the specified numeric collection.
        /// </summary>
        /// <param name="source"> The collection to use. </param>
        /// <returns> The smallest element in the collection. </returns>
        /// <exception cref="Core.LoggedException"> The source was <see langword="null"/> or empty. </exception>
        public static float Min(this float[] source)
        {
            NullOrEmptyCheck(source);
            float min = source[0];

            for (int i = 1; i < source.Length; i++)
            {
                if (source[i] < min) min = source[i];
            }

            return min;
        }

        /// <summary>
        /// Returns the smallest element of the specified numeric collection.
        /// </summary>
        /// <param name="source"> The collection to use. </param>
        /// <returns> The smallest element in the collection. </returns>
        /// <exception cref="Core.LoggedException"> The source was <see langword="null"/> or empty. </exception>
        public static double Min(this double[] source)
        {
            NullOrEmptyCheck(source);
            double min = source[0];

            for (int i = 1; i < source.Length; i++)
            {
                if (source[i] < min) min = source[i];
            }

            return min;
        }

        /// <summary>
        /// Returns the sum of elements of the specified numeric collection.
        /// </summary>
        /// <param name="source"> The collection to use. </param>
        /// <returns> The sum of all elements in the collection. </returns>
        /// <exception cref="Core.LoggedException"> The source was <see langword="null"/>. </exception>
        public static int Sum(this sbyte[] source)
        {
            NullCheck(source);
            int sum = 0;

            for (int i = 0; i < source.Length; i++)
            {
                sum += source[i];
            }

            return sum;
        }

        /// <summary>
        /// Returns the sum of elements of the specified numeric collection.
        /// </summary>
        /// <param name="source"> The collection to use. </param>
        /// <returns> The sum of all elements in the collection. </returns>
        /// <exception cref="Core.LoggedException"> The source was <see langword="null"/>. </exception>
        public static uint Sum(this byte[] source)
        {
            NullCheck(source);
            uint sum = 0;

            for (int i = 0; i < source.Length; i++)
            {
                sum += source[i];
            }

            return sum;
        }

        /// <summary>
        /// Returns the sum of elements of the specified numeric collection.
        /// </summary>
        /// <param name="source"> The collection to use. </param>
        /// <returns> The sum of all elements in the collection. </returns>
        /// <exception cref="Core.LoggedException"> The source was <see langword="null"/>. </exception>
        public static int Sum(this short[] source)
        {
            NullCheck(source);
            int sum = 0;

            for (int i = 0; i < source.Length; i++)
            {
                sum += source[i];
            }

            return sum;
        }

        /// <summary>
        /// Returns the sum of elements of the specified numeric collection.
        /// </summary>
        /// <param name="source"> The collection to use. </param>
        /// <returns> The sum of all elements in the collection. </returns>
        /// <exception cref="Core.LoggedException"> The source was <see langword="null"/>. </exception>
        public static int Sum(this int[] source)
        {
            NullCheck(source);
            int sum = 0;

            for (int i = 0; i < source.Length; i++)
            {
                sum += source[i];
            }

            return sum;
        }

        /// <summary>
        /// Returns the sum of elements of the specified numeric collection.
        /// </summary>
        /// <param name="source"> The collection to use. </param>
        /// <returns> The sum of all elements in the collection. </returns>
        /// <exception cref="Core.LoggedException"> The source was <see langword="null"/>. </exception>
        public static long Sum(this long[] source)
        {
            NullCheck(source);
            long sum = 0;

            for (int i = 0; i < source.Length; i++)
            {
                sum += source[i];
            }

            return sum;
        }

        /// <summary>
        /// Returns the sum of elements of the specified numeric collection.
        /// </summary>
        /// <param name="source"> The collection to use. </param>
        /// <returns> The sum of all elements in the collection. </returns>
        /// <exception cref="Core.LoggedException"> The source was <see langword="null"/>. </exception>
        public static uint Sum(this ushort[] source)
        {
            NullCheck(source);
            uint sum = 0;

            for (int i = 0; i < source.Length; i++)
            {
                sum += source[i];
            }

            return sum;
        }

        /// <summary>
        /// Returns the sum of elements of the specified numeric collection.
        /// </summary>
        /// <param name="source"> The collection to use. </param>
        /// <returns> The sum of all elements in the collection. </returns>
        /// <exception cref="Core.LoggedException"> The source was <see langword="null"/>. </exception>
        public static uint Sum(this uint[] source)
        {
            NullCheck(source);
            uint sum = 0;

            for (int i = 0; i < source.Length; i++)
            {
                sum += source[i];
            }

            return sum;
        }

        /// <summary>
        /// Returns the sum of elements of the specified numeric collection.
        /// </summary>
        /// <param name="source"> The collection to use. </param>
        /// <returns> The sum of all elements in the collection. </returns>
        /// <exception cref="Core.LoggedException"> The source was <see langword="null"/>. </exception>
        public static ulong Sum(this ulong[] source)
        {
            NullCheck(source);
            ulong sum = 0;

            for (int i = 0; i < source.Length; i++)
            {
                sum += source[i];
            }

            return sum;
        }

        /// <summary>
        /// Returns the sum of elements of the specified numeric collection.
        /// </summary>
        /// <param name="source"> The collection to use. </param>
        /// <returns> The sum of all elements in the collection. </returns>
        /// <exception cref="Core.LoggedException"> The source was <see langword="null"/>. </exception>
        public static decimal Sum(this decimal[] source)
        {
            NullCheck(source);
            decimal sum = 0;

            for (int i = 0; i < source.Length; i++)
            {
                sum += source[i];
            }

            return sum;
        }

        /// <summary>
        /// Returns the sum of elements of the specified numeric collection.
        /// </summary>
        /// <param name="source"> The collection to use. </param>
        /// <returns> The sum of all elements in the collection. </returns>
        /// <exception cref="Core.LoggedException"> The source was <see langword="null"/>. </exception>
        public static float Sum(this float[] source)
        {
            NullCheck(source);
            float sum = 0;

            for (int i = 0; i < source.Length; i++)
            {
                sum += source[i];
            }

            return sum;
        }

        /// <summary>
        /// Returns the sum of elements of the specified numeric collection.
        /// </summary>
        /// <param name="source"> The collection to use. </param>
        /// <returns> The sum of all elements in the collection. </returns>
        /// <exception cref="Core.LoggedException"> The source was <see langword="null"/>. </exception>
        public static double Sum(this double[] source)
        {
            NullCheck(source);
            double sum = 0;

            for (int i = 0; i < source.Length; i++)
            {
                sum += source[i];
            }
            
            return sum;
        }
    }
}