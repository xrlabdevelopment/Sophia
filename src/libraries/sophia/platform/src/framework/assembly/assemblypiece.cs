using UnityEngine;
using System.Collections.Generic;
using Sophia.Core;

namespace Sophia.Platform
{
    public abstract class AssemblyPiece : BaseMonoBehaviour
    {
        //--------------------------------------------------------------------------------------
        // Fields
        [SerializeField] protected List<ConnectionPoint> connection_points;
        [SerializeField] protected List<ConnectionPoint> required_points;
        protected AssemblyManager assembly_manager;


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
    }
}
