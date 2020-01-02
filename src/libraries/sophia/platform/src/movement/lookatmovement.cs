using Sophia.Core;
using UnityEngine;

namespace Sophia.Platform
{
    [RequireComponent(typeof(Rigidbody))]
    public class LookAtMovement : BaseMonoBehaviour
    {
        //--------------------------------------------------------------------------------------
        // Inspector
        [SerializeField]
        private float RotationSpeed = 10.0f;    

        //--------------------------------------------------------------------------------------
        // Fields
        private Rigidbody rigid = null;

        private Vector3 last_look_direction = Vector3.zero;

        #region Unity Messages

        //--------------------------------------------------------------------------------------
        private void Awake()
        {
            rigid = getRequiredComponent<Rigidbody>();
        }

        //--------------------------------------------------------------------------------------
        private void Update()
        {
            if (rigid.velocity.normalized != Vector3.zero && rigid.velocity.normalized != last_look_direction)
                last_look_direction = rigid.velocity.normalized;

            if (last_look_direction != Vector3.zero)
                transform.rotation = Quaternion.RotateTowards(transform.rotation, Quaternion.LookRotation(last_look_direction), RotationSpeed * Time.deltaTime);
        }

        #endregion
    }
}
