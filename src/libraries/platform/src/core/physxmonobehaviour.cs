using UnityEngine;

namespace Sophia.Platform
{
    [RequireComponent(typeof(Rigidbody))]
    public class PhysxMonoBehaviour : BaseMonoBehaviour
    {
        //-------------------------------------------------------------------------------------
        public new Rigidbody rigid
        {
            get
            {
                return cached_rigid != null
                    ? cached_rigid
                    : (cached_rigid = getRequiredComponent<Rigidbody>());
            }
        }

        //-------------------------------------------------------------------------------------
        private Rigidbody cached_rigid;
    }
}
