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
        private void OnEnable()
        {
            onEnableAndActive();
        }
        //--------------------------------------------------------------------------------------
        private void OnDisable()
        {
            onDisable();
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
        protected abstract void onEnableAndActive();
        protected abstract void onDisable();
        protected abstract void onUpdate(float dTime);
        protected abstract void onShutDown();
    }
}