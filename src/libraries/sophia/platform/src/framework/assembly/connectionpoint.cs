using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Sophia.Platform
{
    public abstract class ConnectionPoint : BaseMonoBehaviour
    {
        //--------------------------------------------------------------------------------------
        // Fields
        [SerializeField]
        private GameObject other_connection = null;
        protected ConnectionPointManager connection_point_manager = null;
        public string TypeId = "";

        //--------------------------------------------------------------------------------------

        #region Unity Messages

        //--------------------------------------------------------------------------------------
        protected void Awake()
        {
            connection_point_manager.registerConnectionPoint(gameObject);
        }

        #endregion

        //--------------------------------------------------------------------------------------
        public void connectTo(GameObject connectionPoint)
        {
            if (other_connection == null)
                other_connection = connectionPoint;
        }
    }
}
