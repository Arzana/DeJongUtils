namespace Mentula.Utilities
{
    using System;

    public static class Utils
    {
        public static Random Rng { get; private set; } = new Random();

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