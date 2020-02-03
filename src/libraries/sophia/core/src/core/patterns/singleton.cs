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
                Debug.Assert(instance != null, "Instance not yet created!");
                return instance;
            }
        }

        //--------------------------------------------------------------------------------------
        // Fields
        private static T instance;

        //--------------------------------------------------------------------------------------
        public static U getAs<U>()
            where U : class, new()
        {
            return Singleton<T>.Instance as U;
        }

        //--------------------------------------------------------------------------------------
        public void createInstance()
        {
            instance = new T();
        }
        //--------------------------------------------------------------------------------------
        public void destroyInstance()
        {
            instance = null;
        }
    }
}
