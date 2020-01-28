namespace Sophia.Platform
{
    using Sophia.Core;
    using UnityEngine;

    public class AssemblyManager: MonoBehaviour
    {
        //--------------------------------------------------------------------------------------
        // Constants
        private readonly float THRESHOLD = 0.2f;

        //--------------------------------------------------------------------------------------
        // Delegates
        public delegate void Assemble(ConnectionPoint connectionPoint1, AssemblyPiece assembly1, ConnectionPoint connectionPoint2, AssemblyPiece assembly2);

        public Assemble OnAssemble;

        //--------------------------------------------------------------------------------------
        public bool assemble(GameObject assemblePiece1, GameObject assemblePiece2)
        {
            if (assemblePiece1 == null || assemblePiece2 == null)
            {
                Debug.Log(string.Format("The {0} assemblePiece passed to the assemble(GameObject,GameObject) was NULL.", assemblePiece1 == null ? "first" : "second"));
                return false;
            }

            AssemblyPiece as_piece1 = assemblePiece1.GetComponent<AssemblyPiece>();
            AssemblyPiece as_piece2 = assemblePiece2.GetComponent<AssemblyPiece>();

            if (as_piece1 == null || as_piece1 == null)
            {
                Debug.Log(string.Format("{0} doesn't contain an IAssemblyPiece.", as_piece1 == null ? assemblePiece1.name : assemblePiece2.name));
                return false;
            }

            foreach (ConnectionPoint cp1 in as_piece1.RequiredPoints)
            {
                foreach (ConnectionPoint cp2 in as_piece2.ConnectionPoints)
                {
                    if(cp1 != cp2)
                        continue;
                    
                    if (Vector3.Distance(cp1.transform.position, cp2.transform.position) <= THRESHOLD)
                    {
                       // cp2.transform.parent.parent = as_piece1.transform;
                        if(OnAssemble != null)
                            OnAssemble.Invoke(cp1, as_piece1, cp2, as_piece2);

                        return true;
                    }
                }
            }

            return false;
        }
    }
}
