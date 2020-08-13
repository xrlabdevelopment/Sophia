using System;

namespace Sophia.Core.Extensions
{
    public static class ArrayExtensions
    {
        //-------------------------------------------------------------------------------------
        /// <summary>
        /// Concatenate 2 arrays to each other
        /// </summary>
        /// <typeparam name="T">Type of the array</typeparam>
        /// <param name="array">The array we operator on</param>
        /// <param name="other">The other array we would like to concatenate</param>
        /// <returns>return the new array</returns>
        public static T[] concat<T>(this T[] array, T[] other)
        {
            int original_size = array.Length;

            Array.Resize(ref array, array.Length + other.Length);
            Array.Copy(other, 0, array, original_size, other.Length);

            return array;
        }

        //-------------------------------------------------------------------------------------
        public static void fill<T>(this T[] array, T value)
        {
            for (int i = 0; i < array.Length; ++i)
                array[i] = value;
        }
    }
}
