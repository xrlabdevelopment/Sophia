using Sophia.Platform;
using UnityEngine;

namespace Sophia
{
	public abstract class MonoBehaviourSingleton<T> : BaseMonoBehaviour 
		where T : Component
	{
		//--------------------------------------------------------------------------------------
		// Properties
		public static T Instance
		{
			get	{ return instance; }
		}

        protected abstract bool ShouldDestroyOnLoad { get; }

        //--------------------------------------------------------------------------------------
        // Fields
        private static T instance;

        //--------------------------------------------------------------------------------------
        public static U getAs<U>()
            where U : BaseMonoBehaviour
        {
            return MonoBehaviourSingleton<T>.Instance as U;
        }

        #region Unity Messages

        //--------------------------------------------------------------------------------------
        private void Awake()
		{
			if (instance == null)
			{
				instance = this as T;
                if(!ShouldDestroyOnLoad)
				    DontDestroyOnLoad(this.gameObject);
	
				onAwake();
			}
			else
			{
				Destroy(gameObject);
			}
		}
        //--------------------------------------------------------------------------------------
        private void Start()
		{
			onStart();
		}
		//--------------------------------------------------------------------------------------
		private void Update()
		{
			onUpdate(Time.deltaTime);
		}
		//--------------------------------------------------------------------------------------
		private void OnDestroy()
		{
			onDestroy();
		}
	
		#endregion
	
		//--------------------------------------------------------------------------------------
		/// <summary>
		/// onAwake is called when the script instance is being loaded.
		/// </summary>
		protected abstract void onAwake();
		//--------------------------------------------------------------------------------------
		/// <summary>
		/// Start is called on the frame when a script is enabled just before any of the Update methods are called the first time.
		/// </summary>
		protected abstract void onStart();
		//--------------------------------------------------------------------------------------
		/// <summary>
		/// Update is called every frame, if the MonoBehaviour is enabled.
		/// </summary>
		/// <param name="dTime">The completion time in seconds since the last frame.</param>
		protected abstract void onUpdate(float dTime);
		//--------------------------------------------------------------------------------------
		/// <summary>
		/// Destroying the attached Behaviour will result in the game or Scene receiving OnDestroy.
		/// </summary>
		protected abstract void onDestroy();
	}
}
