using UnityEngine;
using System.Collections.Generic;
using Sophia.Core;

namespace Sophia.Platform
{
    public class AssemblyPiece : BaseMonoBehaviour
    {
        //--------------------------------------------------------------------------------------
        // Properties
        public List<ConnectionPoint> ConnectionPoints
        {
            get { return connection_points; }
        }
        public List<ConnectionPoint> RequiredPoints
        {
            get { return required_points; }
        }

        //--------------------------------------------------------------------------------------
        // Fields
        private List<ConnectionPoint> connection_points;
        private List<ConnectionPoint> required_points;

        #region Unity Messages

        //--------------------------------------------------------------------------------------
        private void Awake()
        {
            connection_points = new List<ConnectionPoint>();
            required_points = new List<ConnectionPoint>();

            AssemblyManager.Instance.RegisterAssemblyObject = this.gameObject;
        }

        #endregion
    }
}
