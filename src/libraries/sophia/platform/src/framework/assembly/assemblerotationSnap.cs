namespace Sophia.Platform
{
    using Sophia.Core;
    using UnityEngine;

    public class AssembleRotationSnap : BaseAssemble
    {
        //--------------------------------------------------------------------------------------
        protected override void onAssemble(ConnectionPoint connectionPoint1, AssemblyPiece assembly1, ConnectionPoint connectionPoint2, AssemblyPiece assembly2)
        {
            if (!isMyObject(assembly2))
                return;

            assembly2.transform.rotation = assembly1.transform.rotation;
        }
    }
}
