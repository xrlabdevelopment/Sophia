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
        private bool LockCursor = true;
        [SerializeField]
        [Tooltip("Should we rotate the target with the camera")]
        private bool RotateTargetTowardsLookingDirection = true;
        [SerializeField]
        [Tooltip("Should we invert the camera")]
        private bool InvertedCamera = false;

        [SerializeField]
        [Tooltip("Sensitivity of mouse movement")]
        private float MouseSensitivity = 100.0f;

        [SerializeField]
        [Tooltip("Minimum pitch angle in degrees")]
        private float MinPitchAngle = -90.0f;
        [SerializeField]
        [Tooltip("Maximum pitch angle in degrees")]
        private float MaxPitchAngle = 90.0f;

        [SerializeField]
        [Tooltip("Offset of the camera position from the targets origin")]
        private Vector3 CameraTargetOffset = Vector3.zero;
        [SerializeField]
        [Tooltip("Target we view the world from")]
        private Transform Target = null;

        //--------------------------------------------------------------------------------------
        // Fields
        private float x_rotation = 0.0f;
        private float y_rotation = 0.0f;

        #region Unity Messages

        //--------------------------------------------------------------------------------------
        private void Awake()
        {
            Debug.Assert(Target != null, "No target was assigned.");

            x_rotation = transform.localRotation.eulerAngles.x;
            y_rotation = transform.localRotation.eulerAngles.y;

            MinPitchAngle = Mathf.Clamp(MinPitchAngle, -180.0f, MaxPitchAngle);
            MaxPitchAngle = Mathf.Clamp(MaxPitchAngle, MinPitchAngle, 180.0f);
        }

        //--------------------------------------------------------------------------------------
        private void Start()
        {
            if(LockCursor)
                Cursor.lockState = CursorLockMode.Locked;
        }

        //--------------------------------------------------------------------------------------
        private void LateUpdate()
        {
            transform.position = Target.position + CameraTargetOffset;
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

            y_rotation += yaw;

            transform.localEulerAngles = new Vector3(transform.localEulerAngles.x, y_rotation, 0.0f);

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

            x_rotation += pitch;
            x_rotation = Mathf.Clamp(x_rotation, MinPitchAngle, MaxPitchAngle);
            
            transform.localEulerAngles = new Vector3(InvertedCamera ? x_rotation : -x_rotation, transform.localEulerAngles.y, 0.0f);

            return true;
        }
    }
}