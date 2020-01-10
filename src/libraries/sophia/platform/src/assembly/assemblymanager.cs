using UnityEngine;

namespace Sophia.Platform
{
    class AssemblyManager
    {
        private const float THRESHOLD = 0.2f;

        public delegate int OnAssemble(GameObject assemble, int idAssemblePiece1, int idAssemblePiece2);

        public bool DoAssemble(GameObject assemblePiece1, GameObject assemblePiece2)
        {
            if (assemblePiece1 == null || assemblePiece2 == null)
            {
                var errorName = assemblePiece1 == null ? "first" : "second";
                Debug.Log("NULL ERROR: the" + errorName + " assemblePiece passed to the DoAssemble(GameObject,GameObject) was NULL.");
                return false;
            }

            var asPiece1 = assemblePiece1.GetComponent<BaseAssemblyPiece>();
            var asPiece2 = assemblePiece2.GetComponent<BaseAssemblyPiece>();

            if (asPiece1 == null || asPiece1 == null)
            {
                var errorName = asPiece1 == null ? assemblePiece1.name : assemblePiece2.name;
                Debug.Log("NULL ERROR: " + errorName + " doesn't contain an IAssemblyPiece.");
                return false;
            }


            for (int i = 0; i < asPiece1.RequiredPoints.Count; i++)
            {
                foreach (var cp2 in asPiece2.ConnectionPoints)
                {
                    if (asPiece1.RequiredPoints[i] == cp2)
                    {
                        if (Vector3.Distance(asPiece1.ConnectionPoints[i].transform.position, cp2.transform.position) <= THRESHOLD)
                        {
                            cp2.transform.parent.parent = asPiece1.transform;
                            return true;
                        }
                    }
                }
            }

            return false;
        }
    }
}