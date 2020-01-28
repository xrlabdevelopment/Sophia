namespace Sophia.Platform
{
    using Sophia.Core;
    using UnityEngine;

    public class AssemblePositionSnap: BaseAssemble
    {

        //--------------------------------------------------------------------------------------
        protected override void onAssemble(ConnectionPoint connectionPoint1, AssemblyPiece assembly1, ConnectionPoint connectionPoint2, AssemblyPiece assembly2)
        {
            assembly2.transform.position = assembly1.transform.position;
        }
    }
}
