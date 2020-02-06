namespace Sophia.Core
{
    public static class Math
    {
        public static int clamp(int value, int min, int max)
        {
            return value < min ? min : value > max ? max : value;
        }
        public static float clamp(float value, float min, float max)
        {
            return value < min ? min : value > max ? max : value;
        }
        public static double clamp(double value, double min, double max)
        {
            return value < min ? min : value > max ? max : value;
        }
    }
}
