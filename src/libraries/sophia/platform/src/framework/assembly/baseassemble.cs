namespace Sophia.Platform
{
    using Sophia.Core;
    using UnityEngine;

    public abstract class BaseAssemble : BaseMonoBehaviour
    {
        //--------------------------------------------------------------------------------------
        // Fields
        private AssemblyManager assembly_manager = null;

        #region Unity Messages
        //--------------------------------------------------------------------------------------
        private void Awake()
        {
            assembly_manager = ApplicationManager.Instance.AssemblyManager;
        }

        private void Start()
        {
            if (assembly_manager != null)
                assembly_manager.OnAssemble += onAssemble;
            else
                Debug.Log(string.Format("The assemblePiece with name {0} doesn't have an assembly manager script", name));
        }
        #endregion

        //--------------------------------------------------------------------------------------
        protected Transform findRoot(Transform assemblyObject, string tag)
        {
            var root_not_found = false;
            var check_object = assemblyObject.transform;
            var prev_object = assemblyObject.transform;
            while (!root_not_found)
            {
                if (!check_object.CompareTag(tag))
                    root_not_found = true;
                else if (check_object.transform.parent == null)
                {
                    root_not_found = true;
                    prev_object = check_object;
                }
                else
                {
                    prev_object = check_object;
                    check_object = check_object.transform.parent;
                    continue;
                }
            }
            return prev_object;
        }
        //--------------------------------------------------------------------------------------
        protected abstract void onAssemble(ConnectionPoint connectionPoint1, AssemblyPiece assembly1, ConnectionPoint connectionPoint2, AssemblyPiece assembly2);
    }
}
