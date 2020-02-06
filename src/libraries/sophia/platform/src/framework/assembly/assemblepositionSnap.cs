namespace Sophia.Platform
{
    using Sophia.Core;
    using UnityEngine;

    public class AssemblePositionSnap : BaseAssemble
    {
        //--------------------------------------------------------------------------------------
        protected override void onAssemble(ConnectionPoint connectionPoint1, AssemblyPiece assembly1, ConnectionPoint connectionPoint2, AssemblyPiece assembly2)
        {
            if (!isMyObject(assembly2))
                return;

            var distanceToMove = connectionPoint2.transform.position - connectionPoint1.transform.position;
            assembly2.transform.position += distanceToMove;
        }
    }
}
