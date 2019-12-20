using Sophia.Core;
using UnityEngine;

namespace Sophia.Platform
{
    public class FPSCameraHandler : BaseMonoBehaviour, ICommandReceiver
    {
        //--------------------------------------------------------------------------------------
        // Inspector
        [SerializeField]
        [Tooltip("Should the cursor be locked to the center of the screen")]
        private bool LockCursor;

        [SerializeField]
        [Tooltip("Sensitivity of mouse movement")]
        private float MouseSensitivity = 100.0f;

        [SerializeField]
        [Tooltip("Offset of the camera position from the targets origin")]
        private Vector3 CameraTargetOffset = Vector3.zero;
        [SerializeField]
        [Tooltip("Target we view the world from")]
        private Transform Target;

        //--------------------------------------------------------------------------------------
        // Fields
        private float x_rotation = 0.0f;

        #region Unity Messages

        //--------------------------------------------------------------------------------------
        private void Awake()
        {
            Debug.Assert(Target != null, "No target was assigned.");
        }

        //--------------------------------------------------------------------------------------
        private void Start()
        {
            if(LockCursor)
                Cursor.lockState = CursorLockMode.Locked;
        }

        //--------------------------------------------------------------------------------------
        private void OnDrawGizmos()
        {
            if (Target == null)
                return;

            Gizmos.color = Color.green;
            Gizmos.DrawWireSphere(Target.position + CameraTargetOffset, 0.2f);

            Gizmos.color = Color.red;
            Gizmos.DrawLine(transform.position, Target.position + CameraTargetOffset);
        }

        #endregion

        //--------------------------------------------------------------------------------------
        /// <summary>
        /// Rotate the camera using the yaw axis
        /// </summary>
        /// <param name="axis">The direction and rotation amount</param>
        public bool rotateCameraOverYaw(float axis)
        {
            float yaw = axis * MouseSensitivity * Time.deltaTime;

            Target.Rotate(Vector3.up * yaw);

            return true;
        }
        //--------------------------------------------------------------------------------------
        /// <summary>
        /// Rotate the camera using the pitch axis
        /// </summary>
        /// <param name="axis">The direction and rotation amount</param>
        public bool rotateCameraOverPitch(float axis)
        {
            float pitch = axis * MouseSensitivity * Time.deltaTime;

            x_rotation -= pitch;
            x_rotation = Mathf.Clamp(x_rotation, -90f, 90f);

            transform.localRotation = Quaternion.Euler(x_rotation, 0.0f, 0.0f);

            return true;
        }
    }
}