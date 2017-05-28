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
        /// Represents the value of pi.
        /// </summary>
        public const float PI = 3.14159265358979323846f;
        /// <summary>
        /// Represents the inverse value of pi.
        /// </summary>
        public const float INV_PI = 1.0f / PI;
        /// <summary>
        /// Represents the value of pi times two.
        /// </summary>
        public const float TAU = 6.28318530717958647692f;

        private const float DEG2RAD = 0.01745329251994329576f;
        private const float RAD2DEG = 57.29577951308232087679f;

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
        /// Performs linear interpolation on the specified values.
        /// </summary>
        /// <param name="amount"> A value between 0 and 1 indicating the interpolation amount. </param>
        /// <param name="min"> The specified minimum. </param>
        /// <param name="max"> The specified maximum. </param>
        /// <returns> The interpolated value. </returns>
        public static float Lerp(float amount, float min, float max)
        {
            return min + (max - min) * amount;
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

        /// <summary>
        /// Converts radians to degrees.
        /// </summary>
        /// <param name="rads"> The specified angle in radians to convert. </param>
        /// <returns> The angle in degrees. </returns>
        public static float ToDegrees(float rads)
        {
            return rads * RAD2DEG;
        }

        /// <summary>
        /// Converts degree to radians.
        /// </summary>
        /// <param name="degs"> The specified angle in degrees to convert. </param>
        /// <returns> The angle in radians. </returns>
        public static float ToRadians(float degs)
        {
            return degs * DEG2RAD;
        }

        /// <summary>
        /// Clamps a specified angle between PI and -PI.
        /// </summary>
        /// <param name="rads"> The angle in radians to clamp. </param>
        /// <returns> The new angle in radians. </returns>
        public static float WrapAngle(float rads)
        {
            if (rads > -PI && rads <= PI) return rads;
            rads %= TAU;

            if (rads <= -PI) return rads + TAU;
            if (rads > PI) return rads - TAU;

            return rads;
        }
    }
}