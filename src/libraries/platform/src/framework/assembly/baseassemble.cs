namespace Sophia.Platform.Framework
{
    using Sophia.Core;
    using UnityEngine;

    public abstract class BaseAssemble : BaseMonoBehaviour
    {
        //--------------------------------------------------------------------------------------
        // Fields
        private AssemblyManager assembly_manager = null;

        //--------------------------------------------------------------------------------------
        private void Start()
        {
            assembly_manager = AssemblyManager.Instance;
            if (assembly_manager != null)
                assembly_manager.OnAssemble += onAssemble;
            else
                Debug.Log(string.Format("The assemblePiece with name {0} doesn't have an assembly manager script", name));
        }

        //--------------------------------------------------------------------------------------
        protected abstract void onAssemble(ConnectionPoint connectionPoint1, AssemblyPiece assembly1, ConnectionPoint connectionPoint2, AssemblyPiece assembly2);
    }
}
