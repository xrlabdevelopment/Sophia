namespace Sophia.Platform
{
    using Sophia.Core;
    using UnityEngine;

    public class AssembleParent : BaseAssemble
    {

    //--------------------------------------------------------------------------------------
        protected override void onAssemble(ConnectionPoint connectionPoint1, AssemblyPiece assembly1, ConnectionPoint connectionPoint2, AssemblyPiece assembly2)
        {
            assembly2.transform.parent = assembly1.transform;
        }
    }
}
