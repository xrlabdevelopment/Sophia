using UnityEngine;

namespace Sophia.Platform
{
    public class CharacterMovement : BaseMonoBehaviour
    {
        //--------------------------------------------------------------------------------------
        // Inspector
        [SerializeField]
        private float MovementSpeed = 10.0f;
        [SerializeField]
        private float MaxMovementSpeed = 10.0f;

        //--------------------------------------------------------------------------------------
        // Properties
        public bool IsMoving
        {
            get { return rigid.velocity.magnitude > 0; }
        }

        //--------------------------------------------------------------------------------------
        // Fields
        private Rigidbody rigid = null;

        #region Unity Messages

        //--------------------------------------------------------------------------------------
        private void Awake()
        {
            rigid = getRequiredComponent<Rigidbody>();
        }

        #endregion

        //-------------------------------------------------------------------------------------
        /// <summary>
        /// Move the character in a certain direction
        /// </summary>
        /// <param name="direction">Direction we will move towards</param>
        /// <returns>True if we move, false if not</returns>
        protected bool move(Vector3 direction, float axis, float dTime)
        {
            // If the direction we want to go is a null vector.
            // If the axis ( horizontal or vertical ) is null.
            //      => We do nothing
            if ((direction - Vector3.zero).magnitude < float.Epsilon || (Mathf.Abs(axis) - 1.0f) > float.Epsilon)
                return false;

            Vector3 desired = direction;
            desired.Normalize();
            desired *= (MovementSpeed * axis) * Time.deltaTime;

            if (rigid.isKinematic)
            {
                desired = Vector3.ClampMagnitude(desired, MaxMovementSpeed);
                transform.position += desired;

                return true;
            }

            rigid.AddForce(desired);
            rigid.velocity = Vector3.ClampMagnitude(rigid.velocity, MaxMovementSpeed);

            return true;
        }
    }
}