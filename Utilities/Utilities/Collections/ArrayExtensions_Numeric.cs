namespace Mentula.Utilities.Collections
{
    public static partial class ArrayExtensions
    {
        public static float Average(this sbyte[] source)
        {
            NullCheck(source);
            return source.Sum() / (float)source.Length;
        }

        public static float Average(this byte[] source)
        {
            NullCheck(source);
            return source.Sum() / (float)source.Length;
        }

        public static float Average(this short[] source)
        {
            NullCheck(source);
            return source.Sum() / (float)source.Length;
        }

        public static float Average(this int[] source)
        {
            NullCheck(source);
            return source.Sum() / (float)source.Length;
        }

        public static float Average(this long[] source)
        {
            NullCheck(source);
            return source.Sum() / (float)source.Length;
        }

        public static float Average(this ushort[] source)
        {
            NullCheck(source);
            return source.Sum() / (float)source.Length;
        }

        public static float Average(this uint[] source)
        {
            NullCheck(source);
            return source.Sum() / (float)source.Length;
        }

        public static float Average(this ulong[] source)
        {
            NullCheck(source);
            return source.Sum() / (float)source.Length;
        }

        public static decimal Average(this decimal[] source)
        {
            NullCheck(source);
            return source.Sum() / source.Length;
        }

        public static float Average(this float[] source)
        {
            NullCheck(source);
            return source.Sum() / source.Length;
        }

        public static double Average(this double[] source)
        {
            NullCheck(source);
            return source.Sum() / (float)source.Length;
        }

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

        public static int Sum(this byte[] source)
        {
            NullCheck(source);
            int sum = 0;

            for (int i = 0; i < source.Length; i++)
            {
                sum += source[i];
            }

            return sum;
        }

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

        public static int Sum(this ushort[] source)
        {
            NullCheck(source);
            int sum = 0;

            for (int i = 0; i < source.Length; i++)
            {
                sum += source[i];
            }

            return sum;
        }

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