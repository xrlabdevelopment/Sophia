namespace Sophia.Platform
{
    using Sophia.Core;
    using UnityEngine;

    [RequireComponent(typeof(AssemblyManager))]
    public abstract class BaseAssemble : BaseMonoBehaviour
    {
        //--------------------------------------------------------------------------------------
        // private variables
        private AssemblyManager assembly_manager;

        //--------------------------------------------------------------------------------------
        private void Start()
        {
            assembly_manager = this.GetComponent<AssemblyManager>();
            if (assembly_manager != null)
                assembly_manager.OnAssemble += onAssemble;
            else
                Debug.Log(string.Format("The assemblePiece with name {0} doesn't have an assembly manager script", this.name));
        }

        protected abstract void onAssemble(ConnectionPoint connectionPoint1, AssemblyPiece assembly1, ConnectionPoint connectionPoint2, AssemblyPiece assembly2);
    }
}
