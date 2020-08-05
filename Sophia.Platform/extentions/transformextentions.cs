using System.Collections.Generic;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Sophia.Extensions
{
    /// <summary>
    /// Contains useful extensions for Select.
    /// </summary>
    public static class TransformExtensions
	{
        //-------------------------------------------------------------------------------------
        /// <summary>
        /// sets the X position of this transform.
        /// </summary>
        public static void setX(this Transform transform, float x)
		{
            Vector3 new_position = new Vector3(x, transform.position.y, transform.position.z);
			transform.position = new_position;
		}

        //-------------------------------------------------------------------------------------
        /// <summary>
        /// sets the Y position of this transform.
        /// </summary>
        public static void setY(this Transform transform, float y)
		{
            Vector3 new_position = new Vector3(transform.position.x, y, transform.position.z);
			transform.position = new_position;
		}

        //-------------------------------------------------------------------------------------
        /// <summary>
        /// sets the Z position of this transform.
        /// </summary>
        public static void setZ(this Transform transform, float z)
		{
			Vector3 new_position = new Vector3(transform.position.x, transform.position.y, z);
			transform.position = new_position;
		}

        //-------------------------------------------------------------------------------------
        /// <summary>
        /// sets the X and Y position of this transform.
        /// </summary>
        public static void setXY(this Transform transform, float x, float y)
		{
			Vector3 new_position = new Vector3(x, y, transform.position.z);
			transform.position = new_position;
		}

        //-------------------------------------------------------------------------------------
        /// <summary>
        /// sets the X and Z position of this transform.
        /// </summary>
        public static void setXZ(this Transform transform, float x, float z)
		{
			Vector3 new_position = new Vector3(x, transform.position.y, z);
			transform.position = new_position;
		}

        //-------------------------------------------------------------------------------------
        /// <summary>
        /// sets the Y and Z position of this transform.
        /// </summary>
        public static void setYZ(this Transform transform, float y, float z)
		{
			Vector3 new_position = new Vector3(transform.position.x, y, z);
			transform.position = new_position;
		}

        //-------------------------------------------------------------------------------------
        /// <summary>
        /// sets the X, Y and Z position of this transform.
        /// </summary>
        public static void setXYZ(this Transform transform, float x, float y, float z)
		{
			Vector3 new_position = new Vector3(x, y, z);
			transform.position = new_position;
		}

        //-------------------------------------------------------------------------------------
        /// <summary>
        /// Translates this transform along the X axis.
        /// </summary>
        public static void translateX(this Transform transform, float x)
		{
			Vector3 offset = new Vector3(x, 0, 0);
			transform.position += offset;
		}

        //-------------------------------------------------------------------------------------
        /// <summary>
        /// Translates this transform along the Y axis.
        /// </summary>
        public static void translateY(this Transform transform, float y)
		{
			Vector3 offset = new Vector3(0, y, 0);
			transform.position += offset;
		}

        //-------------------------------------------------------------------------------------
        /// <summary>
        /// Translates this transform along the Z axis.
        /// </summary>
        public static void translateZ(this Transform transform, float z)
		{
			Vector3 offset = new Vector3(0, 0, z);
			transform.position += offset;
		}

        //-------------------------------------------------------------------------------------
        /// <summary>
        /// Translates this transform along the X and Y axes.
        /// </summary>
        public static void translateXY(this Transform transform, float x, float y)
		{
			Vector3 offset = new Vector3(x, y, 0);
			transform.position += offset;
		}

        //-------------------------------------------------------------------------------------
        /// <summary>
        /// Translates this transform along the X and Z axes.
        /// </summary>
        public static void translateXZ(this Transform transform, float x, float z)
		{
			Vector3 offset = new Vector3(x, 0, z);
			transform.position += offset;
		}

        //-------------------------------------------------------------------------------------
        /// <summary>
        /// Translates this transform along the Y and Z axes.
        /// </summary>
        public static void translateYZ(this Transform transform, float y, float z)
		{
			Vector3 offset = new Vector3(0, y, z);
			transform.position += offset;
		}

        //-------------------------------------------------------------------------------------
        /// <summary>
        /// Translates this transform along the X, Y and Z axis.
        /// </summary>
        public static void translateXYZ(this Transform transform, float x, float y, float z)
		{
			Vector3 offset = new Vector3(x, y, z);
			transform.position += offset;
		}

        //-------------------------------------------------------------------------------------
        /// <summary>
        /// sets the local X position of this transform.
        /// </summary>
        public static void setLocalX(this Transform transform, float x)
		{
			Vector3 new_position = new Vector3(x, transform.localPosition.y, transform.localPosition.z);
			transform.localPosition = new_position;
		}

        //-------------------------------------------------------------------------------------
        /// <summary>
        /// sets the local Y position of this transform.
        /// </summary>
        public static void setLocalY(this Transform transform, float y)
		{
			Vector3 new_position = new Vector3(transform.localPosition.x, y, transform.localPosition.z);
			transform.localPosition = new_position;
		}

        //-------------------------------------------------------------------------------------
        /// <summary>
        /// sets the local Z position of this transform.
        /// </summary>
        public static void setLocalZ(this Transform transform, float z)
		{
			Vector3 new_position = new Vector3(transform.localPosition.x, transform.localPosition.y, z);
			transform.localPosition = new_position;
		}

        //-------------------------------------------------------------------------------------
        /// <summary>
        /// sets the local X and Y position of this transform.
        /// </summary>
        public static void setLocalXY(this Transform transform, float x, float y)
		{
			Vector3 new_position = new Vector3(x, y, transform.localPosition.z);
			transform.localPosition = new_position;
		}

        //-------------------------------------------------------------------------------------
        /// <summary>
        /// sets the local X and Z position of this transform.
        /// </summary>
        public static void setLocalXZ(this Transform transform, float x, float z)
		{
			Vector3 new_position = new Vector3(x, transform.localPosition.z, z);
			transform.localPosition = new_position;
		}

        //-------------------------------------------------------------------------------------
        /// <summary>
        /// sets the local Y and Z position of this transform.
        /// </summary>
        public static void setLocalYZ(this Transform transform, float y, float z)
		{
			Vector3 new_position = new Vector3(transform.localPosition.x, y, z);
			transform.localPosition = new_position;
		}

        //-------------------------------------------------------------------------------------
        /// <summary>
        /// sets the local X, Y and Z position of this transform.
        /// </summary>
        public static void setLocalXYZ(this Transform transform, float x, float y, float z)
		{
			Vector3 new_position = new Vector3(x, y, z);
			transform.localPosition = new_position;
		}

        //-------------------------------------------------------------------------------------
        /// <summary>
        /// sets the position to 0, 0, 0.
        /// </summary>
        public static void resetPosition(this Transform transform)
		{
			transform.position = Vector3.zero;
		}

        //-------------------------------------------------------------------------------------
        /// <summary>
        /// sets the local position to 0, 0, 0.
        /// </summary>
        public static void resetLocalPosition(this Transform transform)
		{
			transform.localPosition = Vector3.zero;
		}

        //-------------------------------------------------------------------------------------
        /// <summary>
        /// sets the local X scale of this transform.
        /// </summary>
        public static void setScaleX(this Transform transform, float x)
		{
			Vector3 new_scale = new Vector3(x, transform.localScale.y, transform.localScale.z);
			transform.localScale = new_scale;
		}

        //-------------------------------------------------------------------------------------
        /// <summary>
        /// sets the local Y scale of this transform.
        /// </summary>
        public static void setScaleY(this Transform transform, float y)
		{
			Vector3 new_scale = new Vector3(transform.localScale.x, y, transform.localScale.z);
			transform.localScale = new_scale;
		}

        //-------------------------------------------------------------------------------------
        /// <summary>
        /// sets the local Z scale of this transform.
        /// </summary>
        public static void setScaleZ(this Transform transform, float z)
		{
			Vector3 new_scale = new Vector3(transform.localScale.x, transform.localScale.y, z);
			transform.localScale = new_scale;
		}

        //-------------------------------------------------------------------------------------
        /// <summary>
        /// sets the local X and Y scale of this transform.
        /// </summary>
        public static void setScaleXY(this Transform transform, float x, float y)
		{
			Vector3 new_scale = new Vector3(x, y, transform.localScale.z);
			transform.localScale = new_scale;
		}

        //-------------------------------------------------------------------------------------
        /// <summary>
        /// sets the local X and Z scale of this transform.
        /// </summary>
        public static void setScaleXZ(this Transform transform, float x, float z)
		{
			Vector3 new_scale = new Vector3(x, transform.localScale.y, z);
			transform.localScale = new_scale;
		}

        //-------------------------------------------------------------------------------------
        /// <summary>
        /// sets the local Y and Z scale of this transform.
        /// </summary>
        public static void setScaleYZ(this Transform transform, float y, float z)
		{
			Vector3 new_scale = new Vector3(transform.localScale.x, y, z);
			transform.localScale = new_scale;
		}

        //-------------------------------------------------------------------------------------
        /// <summary>
        /// sets the local X, Y and Z scale of this transform.
        /// </summary>
        public static void setScaleXYZ(this Transform transform, float x, float y, float z)
		{
			Vector3 new_scale = new Vector3(x, y, z);
			transform.localScale = new_scale;
		}

        //-------------------------------------------------------------------------------------
        /// <summary>
        ///  Scale this transform in the X direction.
        /// </summary>
        public static void scaleByX(this Transform transform, float x)
		{
			transform.localScale = new Vector3(transform.localScale.x * x, transform.localScale.y, transform.localScale.z);
		}

        //-------------------------------------------------------------------------------------
        /// <summary>
        /// Scale this transform in the Y direction.
        /// </summary>
        public static void scaleByY(this Transform transform, float y)
		{
			transform.localScale = new Vector3(transform.localScale.x, transform.localScale.y * y, transform.localScale.z);
		}

        //-------------------------------------------------------------------------------------
        /// <summary>
        /// Scale this transform in the Z direction.
        /// </summary>
        public static void scaleByZ(this Transform transform, float z)
		{
			transform.localScale = new Vector3(transform.localScale.x, transform.localScale.y, transform.localScale.z * z);
		}

        //-------------------------------------------------------------------------------------
        /// <summary>
        /// Scale this transform in the X, Y direction.
        /// </summary>
        public static void scaleByXY(this Transform transform, float x, float y)
		{
			transform.localScale = new Vector3(transform.localScale.x * x, transform.localScale.y * y, transform.localScale.z);
		}

        //-------------------------------------------------------------------------------------
        /// <summary>
        /// Scale this transform in the X, Z directions.
        /// </summary>
        public static void scaleByXZ(this Transform transform, float x, float z)
		{
			transform.localScale = new Vector3(transform.localScale.x * x, transform.localScale.y, transform.localScale.z * z);
		}

        //-------------------------------------------------------------------------------------
        /// <summary>
        /// Scale this transform in the Y and Z directions.
        /// </summary>
        public static void scaleByYZ(this Transform transform, float y, float z)
		{
			transform.localScale = new Vector3(transform.localScale.x, transform.localScale.y * y, transform.localScale.z * z);
		}

        //-------------------------------------------------------------------------------------
        /// <summary>
        /// Scale this transform in the X and Y directions.
        /// </summary>
        public static void scaleByXY(this Transform transform, float r)
		{
			transform.scaleByXY(r, r);
		}

        //-------------------------------------------------------------------------------------
        /// <summary>
        /// Scale this transform in the X and Z directions.
        /// </summary>
        public static void scaleByXZ(this Transform transform, float r)
		{
			transform.scaleByXZ(r, r);
		}

        //-------------------------------------------------------------------------------------
        /// <summary>
        /// Scale this transform in the Y and Z directions.
        /// </summary>
        public static void scaleByYZ(this Transform transform, float r)
		{
			transform.scaleByYZ(r, r);
		}

        //-------------------------------------------------------------------------------------
        /// <summary>
        /// Scale this transform in the X, Y and Z directions.
        /// </summary>
        public static void scaleByXYZ(this Transform transform, float x, float y, float z)
		{
			transform.localScale = new Vector3(x, y, z);
		}

        //-------------------------------------------------------------------------------------
        /// <summary>
        /// Scale this transform in the X, Y and Z directions.
        /// </summary>
        public static void scaleByXYZ(this Transform transform, float r)
		{
			transform.scaleByXYZ(r, r, r);
		}

        //-------------------------------------------------------------------------------------
        /// <summary>
        /// resets the local scale of this transform in to 1 1 1.
        /// </summary>
        public static void resetScale(this Transform transform)
		{
			transform.localScale = Vector3.one;
		}

        //-------------------------------------------------------------------------------------
        /// <summary>
        /// Negates the X scale.
        /// </summary>
        public static void flipX(this Transform transform)
		{
			transform.setScaleX(-transform.localScale.x);
		}

        //-------------------------------------------------------------------------------------
        /// <summary>
        /// Negates the Y scale.
        /// </summary>
        public static void flipY(this Transform transform)
		{
			transform.setScaleY(-transform.localScale.y);
		}

        //-------------------------------------------------------------------------------------
        /// <summary>
        /// Negates the Z scale.
        /// </summary>
        public static void flipZ(this Transform transform)
		{
			transform.setScaleZ(-transform.localScale.z);
		}

        //-------------------------------------------------------------------------------------
        /// <summary>
        /// Negates the X and Y scale.
        /// </summary>
        public static void flipXY(this Transform transform)
		{
			transform.setScaleXY(-transform.localScale.x, -transform.localScale.y);
		}

        //-------------------------------------------------------------------------------------
        /// <summary>
        /// Negates the X and Z scale.
        /// </summary>
        public static void flipXZ(this Transform transform)
		{
			transform.setScaleXZ(-transform.localScale.x, -transform.localScale.z);
		}

        //-------------------------------------------------------------------------------------
        /// <summary>
        /// Negates the Y and Z scale.
        /// </summary>
        public static void flipYZ(this Transform transform)
		{
			transform.setScaleYZ(-transform.localScale.y, -transform.localScale.z);
		}

        //-------------------------------------------------------------------------------------
        /// <summary>
        /// Negates the X, Y and Z scale.
        /// </summary>
        public static void flipXYZ(this Transform transform)
		{
			transform.setScaleXYZ(-transform.localScale.z, -transform.localScale.y, -transform.localScale.z);
		}

        //-------------------------------------------------------------------------------------
        /// <summary>
        /// sets all scale values to the absolute values.
        /// </summary>
        public static void flipPostive(this Transform transform)
		{
			transform.localScale = new Vector3
                (
				    Mathf.Abs(transform.localScale.x),
				    Mathf.Abs(transform.localScale.y),
				    Mathf.Abs(transform.localScale.z)
                );
		}

        //-------------------------------------------------------------------------------------
        /// <summary>
        /// rotates the transform around the X axis.
        /// </summary>
        public static void rotateAroundX(this Transform transform, float angle)
		{
			Vector3 rotation = new Vector3(angle, 0, 0);
			transform.Rotate(rotation);
		}

        //-------------------------------------------------------------------------------------
        /// <summary>
        /// rotates the transform around the Y axis.
        /// </summary>
        public static void rotateAroundY(this Transform transform, float angle)
		{
			Vector3 rotation = new Vector3(0, angle, 0);
			transform.Rotate(rotation);
		}

        //-------------------------------------------------------------------------------------
        /// <summary>
        /// rotates the transform around the Z axis.
        /// </summary>
        public static void rotateAroundZ(this Transform transform, float angle)
		{
			Vector3 rotation = new Vector3(0, 0, angle);
			transform.Rotate(rotation);
		}

        //-------------------------------------------------------------------------------------
        /// <summary>
        /// sets the X rotation.
        /// </summary>
        public static void setRotationX(this Transform transform, float angle)
		{
			transform.eulerAngles = new Vector3(angle, 0, 0);
		}

        //-------------------------------------------------------------------------------------
        /// <summary>
        /// sets the Y rotation.
        /// </summary>
        public static void setRotationY(this Transform transform, float angle)
		{
			transform.eulerAngles = new Vector3(0, angle, 0);
		}

        //-------------------------------------------------------------------------------------
        /// <summary>
        /// sets the Z rotation.
        /// </summary>
        public static void setRotationZ(this Transform transform, float angle)
		{
			transform.eulerAngles = new Vector3(0, 0, angle);
		}

        //-------------------------------------------------------------------------------------
        /// <summary>
        /// sets the local X rotation.
        /// </summary>
        public static void setLocalRotationX(this Transform transform, float angle)
		{
			transform.localRotation = Quaternion.Euler(new Vector3(angle, 0, 0));
		}

        //-------------------------------------------------------------------------------------
        /// <summary>
        /// sets the local Y rotation.
        /// </summary>
        public static void setLocalRotationY(this Transform transform, float angle)
		{
			transform.localRotation = Quaternion.Euler(new Vector3(0, angle, 0));
		}

        //-------------------------------------------------------------------------------------
        /// <summary>
        /// sets the local Z rotation.
        /// </summary>
        public static void setLocalRotationZ(this Transform transform, float angle)
		{
			transform.localRotation = Quaternion.Euler(new Vector3(0, 0, angle));
		}

        //-------------------------------------------------------------------------------------
        /// <summary>
        /// resets the rotation to 0, 0, 0.
        /// </summary>
        public static void resetRotation(this Transform transform)
		{
			transform.rotation = Quaternion.identity;
		}

        //-------------------------------------------------------------------------------------
        /// <summary>
        /// resets the local rotation to 0, 0, 0.
        /// </summary>
        public static void resetLocalRotation(this Transform transform)
		{
			transform.localRotation = Quaternion.identity;
		}

        //-------------------------------------------------------------------------------------
        /// <summary>
        /// resets the ;local position, local rotation, and local scale.
        /// </summary>
        public static void resetLocal(this Transform transform)
		{
			transform.resetLocalRotation();
			transform.resetLocalPosition();
			transform.resetScale();

		}

        //-------------------------------------------------------------------------------------
        /// <summary>
        /// resets the position, rotation, and local scale.
        /// </summary>
        public static void reset(this Transform transform)
		{
			transform.resetRotation();
			transform.resetPosition();
			transform.resetScale();
		}

        //-------------------------------------------------------------------------------------
        /// <summary>
        /// Retrieve all children from this transform and destroy them.
        /// </summary>
        /// <param name="transform"></param>
        public static void destroyChildren(this Transform transform)
		{
            List<Transform> children = transform.getChildren();

            foreach (Transform child in children)
			{
				Object.Destroy(child.gameObject);
			}
		}

        //-------------------------------------------------------------------------------------
        /// <summary>
        /// Retrieve all children from this transform and destroy them.
        /// </summary>
        /// <param name="transform"></param>
        public static void destroyChildrenImmediate(this Transform transform)
		{
            List<Transform> children = transform.getChildren();

			foreach (Transform child in children)
			{
				Object.DestroyImmediate(child.gameObject);
			}
		}

        //-------------------------------------------------------------------------------------
        /// <summary>
        /// Retrieve all children from this transform and destroy them.
        /// </summary>
        /// <param name="transform"></param>
        public static void destroyChildrenUniversal(this Transform transform)
		{
			if (Application.isPlaying)
			{
				transform.destroyChildren();
			}
			else
			{
				transform.destroyChildrenImmediate();
			}
		}

        //-------------------------------------------------------------------------------------
        /// <summary>
        /// Retrieve all children of this transform
        /// </summary>
        /// <param name="transform"></param>
        /// <returns></returns>
        public static List<Transform> getChildren(this Transform transform)
		{
            List<Transform> children = new List<Transform>();

			for (int i = 0; i < transform.childCount; i++)
			{
                Transform child = transform.GetChild(i);
				children.Add(child);
			}

			return children;
		}
	}
}
