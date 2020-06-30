using System;

namespace Sophia
{
    namespace Extensions
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

                Array.Resize<T>(ref array, array.Length + other.Length);
                Array.Copy(other, 0, array, original_size, other.Length);

                return array;
            }
        }
    }
}
