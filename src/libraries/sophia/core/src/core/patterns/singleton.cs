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
