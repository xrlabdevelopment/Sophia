namespace Sophia.Platform
{
    using System.Collections.Generic;
    using Sophia.Core;
    using UnityEngine;

    public abstract class AssemblyManager : BaseMonoBehaviour
    {
        protected readonly float THRESHOLD = 0.05f;

        //--------------------------------------------------------------------------------------
        // Delegates
        public delegate void Assemble(ConnectionPoint connectionPoint1, AssemblyPiece assembly1, ConnectionPoint connectionPoint2, AssemblyPiece assembly2);
        public Assemble OnAssemble;

        //--------------------------------------------------------------------------------------
        // Properties
        public GameObject RegisterAssemblyObject { set { assembly_objects_in_scene.Add(value); } }

        //--------------------------------------------------------------------------------------
        // Fields
        protected List<GameObject> assembly_objects_in_scene = new List<GameObject>();

        //--------------------------------------------------------------------------------------
        public abstract bool assemble(GameObject assemblePiece1, GameObject assemblePiece2);
    }
}
