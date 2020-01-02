using UnityEngine;

namespace Sophia.Platform
{
    public abstract class GameManager : BaseMonoBehaviour
    {
        #region Unity Messages

        //--------------------------------------------------------------------------------------
        private void Awake()
        {
            onAwake();
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
        private void onDestroy()
        {
            onShutDown();
        }

        #endregion

        protected abstract void onAwake();
        protected abstract void onStart();
        protected abstract void onUpdate(float dTime);
        protected abstract void onShutDown();
    }
}