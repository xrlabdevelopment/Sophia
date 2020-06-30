using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Sophia
{
    namespace Extensions
    {
        /// <summary>
        /// This class provides useful extension methods for collections, mostly IEnumerable.
        /// </summary>
        public static class CollectionExtensions
        {
            //-------------------------------------------------------------------------------------
            /// <summary>
            /// Returns all elements of the source which are of FilterType.
            /// </summary>
            public static IEnumerable<TFilter> filterByType<T, TFilter>(this IEnumerable<T> source)
                where T : class
                where TFilter : class, T
            {
                return source.Where(item => item is TFilter).Cast<TFilter>();
            }

            //-------------------------------------------------------------------------------------
            /// <summary>
            /// Removes all the elements in the list that does not satisfy the predicate.
            /// </summary>
            /// <typeparam name="T">The type of elements in the list.</typeparam>
            /// <param name="source">The list to remove elements from.</param>
            /// <param name="predicate">The predicate used to filter elements. 
            /// All elements that don't satisfy the predicate will be matched.</param>
            public static void removeAllBut<T>(this List<T> source, Predicate<T> predicate)
            {
                Predicate<T> inverse = item => !predicate(item);

                source.RemoveAll(inverse);
            }

            //-------------------------------------------------------------------------------------
            /// <summary>
            /// Returns whether this source is empty.
            /// </summary>
            public static bool isEmpty<T>(this ICollection<T> collection)
            {
                return collection.Count == 0;
            }

            //-------------------------------------------------------------------------------------
            /// <summary>
            /// Add all elements of other to the given source.
            /// </summary>
            public static void addRange<T>(this ICollection<T> collection, IEnumerable<T> other)
            {
                if (other == null)//nothing to add
                {
                    return;
                }

                foreach (T obj in other)
                {
                    collection.Add(obj);
                }
            }

            //-------------------------------------------------------------------------------------
            /// <summary>
            /// Returns a pretty string representation of the given list. The resulting string looks something like
            /// <c>[a, b, c]</c>.
            /// </summary>
            public static string listToString<T>(this IEnumerable<T> source)
            {
                if (source == null)
                {
                    return "null";
                }

                if (!source.Any())
                {
                    return "[]";
                }

                if (source.Count() == 1)
                {
                    return "[" + source.First() + "]";
                }

                string s = "";

                s += source.butFirst().Aggregate(s, (res, x) => res + ", " + x.listToString());
                s = "[" + source.First().listToString() + s + "]";

                return s;
            }

            //-------------------------------------------------------------------------------------
            private static string listToString(this object obj)
            {
                string objAsString = obj as string;

                if (objAsString != null)
                    return objAsString;

                var objAsList = obj as IEnumerable;

                return objAsList == null ? obj.ToString() : objAsList.Cast<object>().listToString();
            }

            //-------------------------------------------------------------------------------------
            /// <summary>
            /// Returns an enumerable of all elements of the given list	but the first,
            /// keeping them in order.
            /// </summary>
            public static IEnumerable<T> butFirst<T>(this IEnumerable<T> source)
            {
                return source.Skip(1);
            }

            //-------------------------------------------------------------------------------------
            /// <summary>
            /// Returns an enumerable of all elements in the given 
            /// list but the last, keeping them in order.
            /// </summary>
            public static IEnumerable<T> butLast<T>(this IEnumerable<T> source)
            {
                var lastX = default(T);
                bool first = true;

                foreach (T x in source)
                {
                    if (first)
                    {
                        first = false;
                    }
                    else
                    {
                        yield return lastX;
                    }

                    lastX = x;
                }
            }

            //-------------------------------------------------------------------------------------
            /// <summary>
            /// Returns a enumerable with elements in order, but the first element is moved to the end.
            /// </summary>
            public static IEnumerable<T> rotateLeft<T>(this IEnumerable<T> source)
            {
                IList<T> enumeratedList = source as IList<T> ?? source.ToList();
                return enumeratedList.butFirst().Concat(enumeratedList.Take(1));
            }

            //-------------------------------------------------------------------------------------
            /// <summary>
            /// Returns a enumerable with elements in order, but the last element is moved to the front.
            /// </summary>
            public static IEnumerable<T> rotateRight<T>(this IEnumerable<T> source)
            {
                IList<T> enumeratedList = source as IList<T> ?? source.ToList();
                yield return enumeratedList.Last();

                foreach (T item in enumeratedList.butLast())
                {
                    yield return item;
                }
            }

            //-------------------------------------------------------------------------------------
            /// <summary>
            /// Returns a random element from a source.
            /// </summary>
            /// <typeparam name="T">The type of items generated from the source.</typeparam>
            /// <param name="source">The list.</param>
            /// <returns>A item ramdonly selected from the source.</returns>
            public static T randomItem<T>(this IEnumerable<T> source)
            {
                return randomItem(source, SophiaRandom.GlobalRandom);
            }
            //-------------------------------------------------------------------------------------
            /// <summary>
            /// Returns a random element from a source.
            /// </summary>
            /// <typeparam name="T">The type of items generated from the source.</typeparam>
            /// <param name="source">The list.</param>
            /// <param name="random">The random generator to use.</param>
            /// <returns>A item randomly selected from the source.</returns>
            public static T randomItem<T>(this IEnumerable<T> source, IRandom random)
            {
                return source.sampleRandom(1, random).First();
            }

            //-------------------------------------------------------------------------------------
            /// <summary>
            /// Returns a random sample from a source.
            /// </summary>
            /// <typeparam name="T">The type of elements of the source.</typeparam>
            /// <param name="source">The source from which to sample.</param>
            /// <param name="sampleCount">The number of samples to return.</param>
            /// <returns>Generates a ransom subset from a given source.</returns>
            public static IEnumerable<T> sampleRandom<T>(this IEnumerable<T> source, int sampleCount)
            {
                return sampleRandom(source, sampleCount, SophiaRandom.GlobalRandom);
            }
            //-------------------------------------------------------------------------------------
            /// <summary>
            /// Returns a random sample from a source.
            /// </summary>
            /// <typeparam name="T">The type of elements of the source.</typeparam>
            /// <param name="source">The source from which to sample.</param>
            /// <param name="sampleCount">The number of samples to return.</param>
            /// <param name="random">The random generator to use.</param>
            /// <returns>Generates a ransom subset from a given source.</returns>
            public static IEnumerable<T> sampleRandom<T>(this IEnumerable<T> source,int sampleCount,IRandom random)
            {
                if (source == null)
                {
                    throw new ArgumentNullException("source");
                }

                if (sampleCount < 0)
                {
                    throw new ArgumentOutOfRangeException("sampleCount");
                }

                /* Reservoir sampling. */
                List<T> samples = new List<T>();

                //Must be 1, otherwise we have to use Range(0, i + 1)
                int i = 1;

                foreach (T item in source)
                {
                    if (i <= sampleCount)
                    {
                        samples.Add(item);
                    }
                    else
                    {
                        // Randomly replace elements in the reservoir with a decreasing probability.
                        int r = random.Next(i);

                        if (r < sampleCount)
                        {
                            samples[r] = item;
                        }
                    }

                    i++;
                }

                return samples;
            }

            //-------------------------------------------------------------------------------------
            /// <summary>
            /// Shuffles a list.
            /// </summary>
            /// <typeparam name="T">The type of items in the list.</typeparam>
            /// <param name="list">The list to shuffle.</param>
            public static void shuffle<T>(this IList<T> list)
            {
                list.shuffle(SophiaRandom.GlobalRandom);
            }
            //-------------------------------------------------------------------------------------
            /// <summary>
            /// Shuffles a list.
            /// </summary>
            /// <typeparam name="T">The type of items in the list.</typeparam>
            /// <param name="list">The list to shuffle.</param>
            /// <param name="random">The random generator to use.</param>
            public static void shuffle<T>(this IList<T> list, IRandom random)
            {
                int n = list.Count;

                while (n > 1)
                {
                    n--;
                    int k = random.Next(0, n + 1);
                    T value = list[k];
                    list[k] = list[n];
                    list[n] = value;
                }
            }

            //-------------------------------------------------------------------------------------
            /// <summary>
            /// Returns the last n elements from a source.
            /// </summary>
            public static IEnumerable<T> takeLast<T>(this IEnumerable<T> source, int n)
            {
                int count = source.Count();

                if (count <= n)
                    return source;

                return source.Skip(count - n);
            }
        }
    }
}
