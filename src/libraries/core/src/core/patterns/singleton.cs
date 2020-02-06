using System.Diagnostics;

namespace Sophia.Core
{
    public class Singleton<T>
        where T : class, new()
    {
        //--------------------------------------------------------------------------------------
        // Properties
        public static T Instance
        {
            get
            {
                Debug.Assert(instance != null, "Instance not created!");
                return instance;
            }
        }

        //--------------------------------------------------------------------------------------
        // Fields
        private static T instance;

        //--------------------------------------------------------------------------------------
        /// <summary>
        /// Convert the singleton to an inherited type
        /// </summary>
        /// <typeparam name="U">The type we wanna retrieve</typeparam>
        /// <returns>The requested object</returns>
        public static U getAs<U>()
            where U : class, new()
        {
            return Singleton<T>.Instance as U;
        }

        //--------------------------------------------------------------------------------------
        /// <summary>
        /// Create the singleton instance
        /// </summary>
        public static void createInstance()
        {
            instance = new T();
        }
        //--------------------------------------------------------------------------------------
        /// <summary>
        /// Destroy the singleton instance
        /// </summary>
        public static void destroyInstance()
        {
            instance = null;
        }
    }
}
