using Sophia.Core;
using UnityEngine;

namespace Sophia.Platform
{
    public enum ManagerPlatform
    {
        Desktop,
        Mobile,
        VR
    }
    public enum ManagerPurpose
    {
        Menu,
        Game
    }

    public abstract class ApplicationManager : Sophia.Platform.Singleton<ApplicationManager>
    {
        //-------------------------------------------------------------------------------------
        // Properties
        public abstract ManagerPlatform Platform { get; }
        public abstract ManagerPurpose Purpose { get; }

        public abstract EventQueue EventQueue { get; }
        public abstract EventDispatch EventDispatch { get; }

        //--------------------------------------------------------------------------------------
        public static T getAs<T>()
            where T : ApplicationManager
        {
            return (T)ApplicationManager.Instance;
        }

        //--------------------------------------------------------------------------------------
        public abstract void subscribeCommandReceiver(ICommandReceiver receiver);
        //--------------------------------------------------------------------------------------
        public abstract void unsubscribeCommandReceiver(ICommandReceiver receiver);
    }
}
