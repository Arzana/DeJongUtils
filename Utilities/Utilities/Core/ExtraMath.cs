namespace DeJong.Utilities.Core
{
    using static System.Math;

    /// <summary>
    /// Contains aditional math functions.
    /// </summary>
#if !DEBUG
    [System.Diagnostics.DebuggerStepThrough]
#endif
    public static class ExtraMath
    {
        /// <summary>
        /// Clamps a integral value between a minimum and a maximum. 
        /// </summary>
        /// <param name="value"> The value to be clamped. </param>
        /// <param name="min"> The minimum value. </param>
        /// <param name="max"> The maximum value. </param>
        /// <returns>
        /// <paramref name="min"/> if the value is smaller than the specified minimum;
        /// <paramref name="max"/> if the value is greater than the specified maximum;
        /// otherwise, <see langword="false"/>.
        /// </returns>
        public static int Clamp(int value, int min, int max)
        {
            return Max(min, Min(max, value));
        }

        /// <summary>
        /// Performs inverse linear interpolation on the specified values.
        /// </summary>
        /// <param name="value"> The value to inverse lerp. </param>
        /// <param name="min"> The specified minimum. </param>
        /// <param name="max"> The specfied maximum. </param>
        /// <returns> The original value before the interpolation. </returns>
        public static float InvLerp(float value, float min, float max)
        {
            return (value - min) / (max - min);
        }
    }
}