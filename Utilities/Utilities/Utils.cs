namespace Mentula.Utilities
{
    using System;

    /// <summary>
    /// Contains general utility functions.
    /// </summary>
#if !DEBUG
    [System.Diagnostics.DebuggerStepThrough]
#endif
    public static class Utils
    {
        /// <summary>
        /// A global random used throughout this library.
        /// </summary>
        public static Random Rng { get; private set; } = new Random();

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
    }
}