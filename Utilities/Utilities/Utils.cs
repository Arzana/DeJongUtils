namespace DeJong.Utilities
{
    using System;
    using System.Runtime.CompilerServices;

    /// <summary>
    /// Contains general utility functions.
    /// </summary>
#if !DEBUG
    [System.Diagnostics.DebuggerStepThrough]
#endif
    public static class Utils
    {
        /// <summary>
        /// The base hash to be used with <see cref="ComputeHash{T}(int, T)"/>.
        /// </summary>
        public const int HASH_BASE = unchecked((int)2166136261);
        private const int HASH_MOD = 16777619;

        /// <summary>
        /// A global random used throughout this library.
        /// </summary>
        public static Random Rng { get; private set; } = new Random();

        /// <summary>
        /// Computes the new hash value with a specified object.
        /// </summary>
        /// <typeparam name="T"> The type of object. </typeparam>
        /// <param name="prevHash"> The previous hash value (<see cref="HASH_BASE"/> as start value). </param>
        /// <param name="obj"> The object to use with computing the new hash. </param>
        /// <returns> The new hash value. </returns>
        /// <remarks>
        /// Example use:
        /// <code>
        /// public override int GetHashCode()
        /// {
        ///     unchecked
        ///     {
        ///         int hash = HASH_BASE;
        ///         hash += ComputeHash(hash, field0);
        ///         hash += ComputeHash(hash, field1);
        ///         return hash;
        ///     }
        /// }
        /// </code>
        /// </remarks>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static int ComputeHash<T>(int prevHash, T obj)
        {
            return prevHash * HASH_MOD ^ obj.GetHashCode();
        }

        /// <summary>
        /// Tries to convert from a specified source to a destination type.
        /// </summary>
        /// <typeparam name="TSource"> The soure type. </typeparam>
        /// <typeparam name="TResult"> The destination type. </typeparam>
        /// <param name="value"> The value to be converted. </param>
        /// <param name="result"> The variable to be assigned the value. </param>
        /// <returns> <see langword="true"/> if the cast was successgull; otherwise, <see langword="false"/>. </returns>
        public static bool TryConvert<TSource, TResult>(TSource value, out TResult result)
        {
            try
            {
                result = (TResult)Convert.ChangeType(value, typeof(TResult));
                return true;
            }
            catch (Exception)
            {
                result = default(TResult);
                return false;
            }
        }

        /// <summary>
        /// Propts the user the message 'Press any key to continue...' and waits for the user to press a key.
        /// </summary>
        public static void PressAnyKeyToContinue()
        {
            Console.WriteLine("Press any key to continue...");
            Console.ReadKey();
        }
    }
}