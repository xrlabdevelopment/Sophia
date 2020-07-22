using System.Collections.Generic;

namespace Sophia
{
    namespace Extensions
    {
        public static class ListExtentions
        {
            //-------------------------------------------------------------------------------------
            /// <summary>
            /// Take the first element in a list
            /// </summary>
            /// <typeparam name="T">Type of the list</typeparam>
            /// <param name="list">The given list</param>
            /// <returns>The first element in this list, if no items are in the list a default value of T will be returned</returns>
            public static T front<T>(this IList<T> list)
            {
                if (list.Count == 0)
                    return default(T);

                return list[0];
            }
            //-------------------------------------------------------------------------------------
            /// <summary>
            /// Take the last element in a list
            /// </summary>
            /// <typeparam name="T">Type of the list</typeparam>
            /// <param name="list">The given list</param>
            /// <returns>The last element in this list, if no items are in the list a default value of T will be returned</returns>
            public static T back<T>(this IList<T> list)
            {
                if (list.Count == 0)
                    return default(T);

                return list[list.Count - 1];
            }
        }
    }
}
