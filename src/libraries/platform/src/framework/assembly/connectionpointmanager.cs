using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Sophia.Platform.Framework
{
    public class ConnectionPointManager : Singleton<ConnectionPointManager>
    {
        //--------------------------------------------------------------------------------------
        // Properties
        private List<GameObject> connection_point_pool = new List<GameObject>();

        //--------------------------------------------------------------------------------------
        public void registerConnectionPoint(GameObject connectionPoint)
        {
            connection_point_pool.Add(connectionPoint);
            if ((connection_point_pool.Count & 1) == 0)
            {
                ConnectionPoint cp1 = connection_point_pool[connection_point_pool.Count - 2].GetComponent<ConnectionPoint>();
                ConnectionPoint cp2 = connection_point_pool[connection_point_pool.Count - 1].GetComponent<ConnectionPoint>();
                cp1.connectTo(cp2.gameObject);
                cp2.connectTo(cp1.gameObject);
            }
        }

        //--------------------------------------------------------------------------------------
        protected override void onAwake()
        {
            // Nothing to implement
        }

        //--------------------------------------------------------------------------------------
        protected override void onDestroy()
        {
            // Nothing to implement
        }

        //--------------------------------------------------------------------------------------
        protected override void onStart()
        {
            // Nothing to implement
        }

        //--------------------------------------------------------------------------------------
        protected override void onUpdate(float dTime)
        {
            // Nothing to implement
        }
    }
}
