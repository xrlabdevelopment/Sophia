namespace Sophia
{
    public static partial class Algorithms
    {
        //-------------------------------------------------------------------------------------
        /// <summary>
        /// Clamp a value between a min an a max value
        /// </summary>
        /// <param name="value">The value to be clamped</param>
        /// <param name="min">Minimum value</param>
        /// <param name="max">Maximum value</param>
        /// <returns>The clamped value</returns>
        public static int clamp(int value, int min, int max)
        {
            return value < min ? min : value > max ? max : value;
        }
        //-------------------------------------------------------------------------------------
        /// <summary>
        /// Clamp a value between a min an a max value
        /// </summary>
        /// <param name="value">The value to be clamped</param>
        /// <param name="min">Minimum value</param>
        /// <param name="max">Maximum value</param>
        /// <returns>The clamped value</returns>
        public static float clamp(float value, float min, float max)
        {
            return value < min ? min : value > max ? max : value;
        }
        //-------------------------------------------------------------------------------------
        /// <summary>
        /// Clamp a value between a min an a max value
        /// </summary>
        /// <param name="value">The value to be clamped</param>
        /// <param name="min">Minimum value</param>
        /// <param name="max">Maximum value</param>
        /// <returns>The clamped value</returns>
        public static double clamp(double value, double min, double max)
        {
            return value < min ? min : value > max ? max : value;
        }
    }
}
