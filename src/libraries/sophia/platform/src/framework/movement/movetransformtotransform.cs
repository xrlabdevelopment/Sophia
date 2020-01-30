using System.Collections;
using Sophia.Core;
using UnityEngine;

namespace Sophia.Platform
{
    public class MoveTransformToTransform : MonoBehaviour
    {
        //--------------------------------------------------------------------------------------
        // private variables
        private Vector3 current_velocity = Vector3.zero;

        //--------------------------------------------------------------------------------------
        private void smoothDampToObject(Transform movingObject, Transform objectToMoveTo, float moveTime)
        {
            Vector3.SmoothDamp(movingObject.position, objectToMoveTo.position, ref current_velocity, moveTime);
        }

        //--------------------------------------------------------------------------------------
        private IEnumerator moveTransforms(Transform movingObject, Transform objectToMoveTo, float threshold, float moveTime)
        {
            while (Vector3.Distance(movingObject.position, objectToMoveTo.position) > threshold)
            {
                yield return new WaitForEndOfFrame();
                smoothDampToObject(movingObject, objectToMoveTo, moveTime);
            }
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
            StartCoroutine(moveTransforms(movingObject, objectToMoveTo, threshold, moveTime));
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
            smoothDampToObject(movingObject, objectToMoveTo, moveTime);
        }
    }
}
