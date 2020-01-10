using UnityEngine;
using System.Collections.Generic;

namespace Sophia.Platform
{
    public class BaseAssemblyPiece: BaseMonoBehaviour, Sophia.Core.IAssemblyPiece
    {
        public List<GameObject> ConnectionPoints = new List<GameObject>();
        public List<GameObject> RequiredPoints = new List<GameObject>();
    }
}