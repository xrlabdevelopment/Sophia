using UnityEngine;

namespace Sophia
{
    [RequireComponent(typeof(Rigidbody))]
    public class PhysxMonoBehaviour : BaseMonoBehaviour
    {
        //-------------------------------------------------------------------------------------
        // Properties
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
        // Fields
        private Rigidbody cached_rigid;
    }
}
