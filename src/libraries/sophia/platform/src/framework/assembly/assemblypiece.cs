using UnityEngine;
using System.Collections.Generic;
using Sophia.Core;

namespace Sophia.Platform
{
    public abstract class AssemblyPiece : BaseMonoBehaviour
    {
        //--------------------------------------------------------------------------------------
        // Fields
        [SerializeField]
        private List<ConnectionPoint> connection_points;
        [SerializeField]
        private List<ConnectionPoint> required_points;
        private AssemblyManager assembly_manager;


        //--------------------------------------------------------------------------------------
        // Properties
        public List<ConnectionPoint> ConnectionPoints
        {
            get { return connection_points; }
            protected set { connection_points = value; }
        }
        public List<ConnectionPoint> RequiredPoints
        {
            get { return required_points; }
            protected set { required_points = value; }
        }
        protected AssemblyManager AssemblyManager
        {
            get { return assembly_manager; }
            set { assembly_manager = value; }
        }
    }
}
