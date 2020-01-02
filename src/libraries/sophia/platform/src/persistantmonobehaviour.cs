using UnityEngine;

namespace Sophia.Platform
{
    public abstract class PersistantMonoBehaviour : BaseMonoBehaviour
    {
        #region Unity Messages

        //--------------------------------------------------------------------------------------
        protected virtual void Awake()
        {
            DontDestroyOnLoad(this.gameObject);
        }

        #endregion
    }
}