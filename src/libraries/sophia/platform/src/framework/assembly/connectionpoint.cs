using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Sophia.Platform
{
    public class ConnectionPoint : BaseMonoBehaviour
    {
        //--------------------------------------------------------------------------------------
        // Properties
        private GameObject other_connection = null;

        #region Unity Messages

        //--------------------------------------------------------------------------------------
        private void Awake()
        {
            ConnectionPointManager.Instance.registerConnectionPoint(gameObject);
        }

        #endregion

        //--------------------------------------------------------------------------------------
        public void connectTo(GameObject connectionPoint)
        {
            other_connection = connectionPoint;
        }
    }
}