namespace Sophia.Platform
{
    using Sophia.Core;
    using UnityEngine;

    public abstract class BaseAssemble : BaseMonoBehaviour
    {
        //--------------------------------------------------------------------------------------
        // Fields
        private AssemblyManager assembly_manager = null;

        //--------------------------------------------------------------------------------------
        private void Awake()
        {
            assembly_manager = ApplicationManager.Instance.AssemManager;
        }

        private void Start()
        {
            if (assembly_manager != null)
                assembly_manager.OnAssemble += onAssemble;
            else
                Debug.Log(string.Format("The assemblePiece with name {0} doesn't have an assembly manager script", name));
        }

        //--------------------------------------------------------------------------------------
        protected bool isMyObject(AssemblyPiece assembly2)
        {
            return true;
        }
        //--------------------------------------------------------------------------------------
        protected abstract void onAssemble(ConnectionPoint connectionPoint1, AssemblyPiece assembly1, ConnectionPoint connectionPoint2, AssemblyPiece assembly2);
    }
}
