using System.Collections;
using Sophia.Core;
using UnityEngine;

namespace Sophia.Platform
{
    public class MoveTransformToTransform : MonoBehaviour
    {
        //--------------------------------------------------------------------------------------
        //  protected variables
        protected bool can_lerp_object = true;
        protected bool finished_lerp = false;
        protected Transform moving_object = null;
        protected Transform object_to_move_to = null;

        //--------------------------------------------------------------------------------------
        // private variables
        private Vector3 current_velocity = Vector3.zero;

        //--------------------------------------------------------------------------------------
        private void smoothDampToObject(float moveTime)
        {
            var movement = Vector3.SmoothDamp(moving_object.position, object_to_move_to.position, ref current_velocity, moveTime);
            moving_object.transform.position = movement;
        }

        //--------------------------------------------------------------------------------------
        private IEnumerator moveTransforms(float threshold, float moveTime)
        {
            while (Vector3.Distance(moving_object.position, object_to_move_to.position) > threshold)
            {
                yield return new WaitForEndOfFrame();
                smoothDampToObject(moveTime);
            }
            finished_lerp = true;
        }

        //--------------------------------------------------------------------------------------
        /// <summary>
        /// Use this function to automatically move an object to another object.
        /// </summary>
        /// <summary>
        /// To move an object to another object per frame use the function: moveTransform(Transform, Transform, float)
        /// </summary>
        /// <param name="movingObject">The transform of the object to move.</param>
        /// <param name="objectToMoveTo">The transform of the object to move to.</param>
        /// <param name="threshold">Treshold between the the allowed distance.</param>
        /// <param name="moveTime">How fast should the smooth damp be.</param>
        public void moveTransformAutomatically(Transform movingObject, Transform objectToMoveTo, float threshold, float moveTime)
        {
            if (!can_lerp_object)
                return;

            Debug.Log(string.Format("moving == 0: {0}, object == 0 {1}", movingObject == null, objectToMoveTo == null));
            Debug.Log("automatically called by: " + objectToMoveTo.name);
            moving_object = movingObject;
            object_to_move_to = objectToMoveTo;
            finished_lerp = false;
            can_lerp_object = false;
            StartCoroutine(moveTransforms(threshold, moveTime));
        }

        //--------------------------------------------------------------------------------------
        /// <summary>
        /// Use this function to move an object to another object per frame.
        /// </summary>
        /// <summary>
        /// Using this function means you need to keep calling it per frame to execute a movement.
        /// </summary>
        /// <summary>
        /// To automate this process use the function:  moveTransformAutomatically(Transform, Transform, float, float)
        /// </summary>
        /// <param name="movingObject"></param>
        /// <param name="objectToMoveTo"></param>
        /// <param name="moveTime"></param>
        public void moveTransform(Transform movingObject, Transform objectToMoveTo, float moveTime)
        {
            if (!can_lerp_object)
                return;

            moving_object = movingObject;
            object_to_move_to = objectToMoveTo;
            finished_lerp = false;
            can_lerp_object = false;
            smoothDampToObject(moveTime);
        }
    }
}
