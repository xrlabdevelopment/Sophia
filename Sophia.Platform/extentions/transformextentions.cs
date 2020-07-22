using System;
using System.Collections.Generic;
using System.Linq;
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
        /// Sets the X position of this transform.
        /// </summary>
        public static void SetX(this Transform transform, float x)
		{
            Vector3 new_position = new Vector3(x, transform.position.y, transform.position.z);
			transform.position = new_position;
		}

        //-------------------------------------------------------------------------------------
        /// <summary>
        /// Sets the Y position of this transform.
        /// </summary>
        public static void SetY(this Transform transform, float y)
		{
            Vector3 new_position = new Vector3(transform.position.x, y, transform.position.z);
			transform.position = new_position;
		}

        //-------------------------------------------------------------------------------------
        /// <summary>
        /// Sets the Z position of this transform.
        /// </summary>
        public static void SetZ(this Transform transform, float z)
		{
			Vector3 new_position = new Vector3(transform.position.x, transform.position.y, z);
			transform.position = new_position;
		}

        //-------------------------------------------------------------------------------------
        /// <summary>
        /// Sets the X and Y position of this transform.
        /// </summary>
        public static void SetXY(this Transform transform, float x, float y)
		{
			Vector3 new_position = new Vector3(x, y, transform.position.z);
			transform.position = new_position;
		}

        //-------------------------------------------------------------------------------------
        /// <summary>
        /// Sets the X and Z position of this transform.
        /// </summary>
        public static void SetXZ(this Transform transform, float x, float z)
		{
			Vector3 new_position = new Vector3(x, transform.position.y, z);
			transform.position = new_position;
		}

        //-------------------------------------------------------------------------------------
        /// <summary>
        /// Sets the Y and Z position of this transform.
        /// </summary>
        public static void SetYZ(this Transform transform, float y, float z)
		{
			Vector3 new_position = new Vector3(transform.position.x, y, z);
			transform.position = new_position;
		}

        //-------------------------------------------------------------------------------------
        /// <summary>
        /// Sets the X, Y and Z position of this transform.
        /// </summary>
        public static void SetXYZ(this Transform transform, float x, float y, float z)
		{
			Vector3 new_position = new Vector3(x, y, z);
			transform.position = new_position;
		}

        //-------------------------------------------------------------------------------------
        /// <summary>
        /// Translates this transform along the X axis.
        /// </summary>
        public static void TranslateX(this Transform transform, float x)
		{
			Vector3 offset = new Vector3(x, 0, 0);
			transform.position += offset;
		}

        //-------------------------------------------------------------------------------------
        /// <summary>
        /// Translates this transform along the Y axis.
        /// </summary>
        public static void TranslateY(this Transform transform, float y)
		{
			Vector3 offset = new Vector3(0, y, 0);
			transform.position += offset;
		}

        //-------------------------------------------------------------------------------------
        /// <summary>
        /// Translates this transform along the Z axis.
        /// </summary>
        public static void TranslateZ(this Transform transform, float z)
		{
			Vector3 offset = new Vector3(0, 0, z);
			transform.position += offset;
		}

        //-------------------------------------------------------------------------------------
        /// <summary>
        /// Translates this transform along the X and Y axes.
        /// </summary>
        public static void TranslateXY(this Transform transform, float x, float y)
		{
			Vector3 offset = new Vector3(x, y, 0);
			transform.position += offset;
		}

        //-------------------------------------------------------------------------------------
        /// <summary>
        /// Translates this transform along the X and Z axes.
        /// </summary>
        public static void TranslateXZ(this Transform transform, float x, float z)
		{
			Vector3 offset = new Vector3(x, 0, z);
			transform.position += offset;
		}

        //-------------------------------------------------------------------------------------
        /// <summary>
        /// Translates this transform along the Y and Z axes.
        /// </summary>
        public static void TranslateYZ(this Transform transform, float y, float z)
		{
			Vector3 offset = new Vector3(0, y, z);
			transform.position += offset;
		}

        //-------------------------------------------------------------------------------------
        /// <summary>
        /// Translates this transform along the X, Y and Z axis.
        /// </summary>
        public static void TranslateXYZ(this Transform transform, float x, float y, float z)
		{
			Vector3 offset = new Vector3(x, y, z);
			transform.position += offset;
		}

        //-------------------------------------------------------------------------------------
        /// <summary>
        /// Sets the local X position of this transform.
        /// </summary>
        public static void SetLocalX(this Transform transform, float x)
		{
			Vector3 new_position = new Vector3(x, transform.localPosition.y, transform.localPosition.z);
			transform.localPosition = new_position;
		}

        //-------------------------------------------------------------------------------------
        /// <summary>
        /// Sets the local Y position of this transform.
        /// </summary>
        public static void SetLocalY(this Transform transform, float y)
		{
			Vector3 new_position = new Vector3(transform.localPosition.x, y, transform.localPosition.z);
			transform.localPosition = new_position;
		}

        //-------------------------------------------------------------------------------------
        /// <summary>
        /// Sets the local Z position of this transform.
        /// </summary>
        public static void SetLocalZ(this Transform transform, float z)
		{
			Vector3 new_position = new Vector3(transform.localPosition.x, transform.localPosition.y, z);
			transform.localPosition = new_position;
		}

        //-------------------------------------------------------------------------------------
        /// <summary>
        /// Sets the local X and Y position of this transform.
        /// </summary>
        public static void SetLocalXY(this Transform transform, float x, float y)
		{
			Vector3 new_position = new Vector3(x, y, transform.localPosition.z);
			transform.localPosition = new_position;
		}

        //-------------------------------------------------------------------------------------
        /// <summary>
        /// Sets the local X and Z position of this transform.
        /// </summary>
        public static void SetLocalXZ(this Transform transform, float x, float z)
		{
			Vector3 new_position = new Vector3(x, transform.localPosition.z, z);
			transform.localPosition = new_position;
		}

        //-------------------------------------------------------------------------------------
        /// <summary>
        /// Sets the local Y and Z position of this transform.
        /// </summary>
        public static void SetLocalYZ(this Transform transform, float y, float z)
		{
			Vector3 new_position = new Vector3(transform.localPosition.x, y, z);
			transform.localPosition = new_position;
		}

        //-------------------------------------------------------------------------------------
        /// <summary>
        /// Sets the local X, Y and Z position of this transform.
        /// </summary>
        public static void SetLocalXYZ(this Transform transform, float x, float y, float z)
		{
			Vector3 new_position = new Vector3(x, y, z);
			transform.localPosition = new_position;
		}

        //-------------------------------------------------------------------------------------
        /// <summary>
        /// Sets the position to 0, 0, 0.
        /// </summary>
        public static void ResetPosition(this Transform transform)
		{
			transform.position = Vector3.zero;
		}

        //-------------------------------------------------------------------------------------
        /// <summary>
        /// Sets the local position to 0, 0, 0.
        /// </summary>
        public static void ResetLocalPosition(this Transform transform)
		{
			transform.localPosition = Vector3.zero;
		}

        //-------------------------------------------------------------------------------------
        /// <summary>
        /// Sets the local X scale of this transform.
        /// </summary>
        public static void SetScaleX(this Transform transform, float x)
		{
			Vector3 new_scale = new Vector3(x, transform.localScale.y, transform.localScale.z);
			transform.localScale = new_scale;
		}

        //-------------------------------------------------------------------------------------
        /// <summary>
        /// Sets the local Y scale of this transform.
        /// </summary>
        public static void SetScaleY(this Transform transform, float y)
		{
			Vector3 new_scale = new Vector3(transform.localScale.x, y, transform.localScale.z);
			transform.localScale = new_scale;
		}

        //-------------------------------------------------------------------------------------
        /// <summary>
        /// Sets the local Z scale of this transform.
        /// </summary>
        public static void SetScaleZ(this Transform transform, float z)
		{
			Vector3 new_scale = new Vector3(transform.localScale.x, transform.localScale.y, z);
			transform.localScale = new_scale;
		}

        //-------------------------------------------------------------------------------------
        /// <summary>
        /// Sets the local X and Y scale of this transform.
        /// </summary>
        public static void SetScaleXY(this Transform transform, float x, float y)
		{
			Vector3 new_scale = new Vector3(x, y, transform.localScale.z);
			transform.localScale = new_scale;
		}

        //-------------------------------------------------------------------------------------
        /// <summary>
        /// Sets the local X and Z scale of this transform.
        /// </summary>
        public static void SetScaleXZ(this Transform transform, float x, float z)
		{
			Vector3 new_scale = new Vector3(x, transform.localScale.y, z);
			transform.localScale = new_scale;
		}

        //-------------------------------------------------------------------------------------
        /// <summary>
        /// Sets the local Y and Z scale of this transform.
        /// </summary>
        public static void SetScaleYZ(this Transform transform, float y, float z)
		{
			Vector3 new_scale = new Vector3(transform.localScale.x, y, z);
			transform.localScale = new_scale;
		}

        //-------------------------------------------------------------------------------------
        /// <summary>
        /// Sets the local X, Y and Z scale of this transform.
        /// </summary>
        public static void SetScaleXYZ(this Transform transform, float x, float y, float z)
		{
			Vector3 new_scale = new Vector3(x, y, z);
			transform.localScale = new_scale;
		}

        //-------------------------------------------------------------------------------------
        /// <summary>
        ///  Scale this transform in the X direction.
        /// </summary>
        public static void ScaleByX(this Transform transform, float x)
		{
			transform.localScale = new Vector3(transform.localScale.x * x, transform.localScale.y, transform.localScale.z);
		}

        //-------------------------------------------------------------------------------------
        /// <summary>
        /// Scale this transform in the Y direction.
        /// </summary>
        public static void ScaleByY(this Transform transform, float y)
		{
			transform.localScale = new Vector3(transform.localScale.x, transform.localScale.y * y, transform.localScale.z);
		}

        //-------------------------------------------------------------------------------------
        /// <summary>
        /// Scale this transform in the Z direction.
        /// </summary>
        public static void ScaleByZ(this Transform transform, float z)
		{
			transform.localScale = new Vector3(transform.localScale.x, transform.localScale.y, transform.localScale.z * z);
		}

        //-------------------------------------------------------------------------------------
        /// <summary>
        /// Scale this transform in the X, Y direction.
        /// </summary>
        public static void ScaleByXY(this Transform transform, float x, float y)
		{
			transform.localScale = new Vector3(transform.localScale.x * x, transform.localScale.y * y, transform.localScale.z);
		}

        //-------------------------------------------------------------------------------------
        /// <summary>
        /// Scale this transform in the X, Z directions.
        /// </summary>
        public static void ScaleByXZ(this Transform transform, float x, float z)
		{
			transform.localScale = new Vector3(transform.localScale.x * x, transform.localScale.y, transform.localScale.z * z);
		}

        //-------------------------------------------------------------------------------------
        /// <summary>
        /// Scale this transform in the Y and Z directions.
        /// </summary>
        public static void ScaleByYZ(this Transform transform, float y, float z)
		{
			transform.localScale = new Vector3(transform.localScale.x, transform.localScale.y * y, transform.localScale.z * z);
		}

        //-------------------------------------------------------------------------------------
        /// <summary>
        /// Scale this transform in the X and Y directions.
        /// </summary>
        public static void ScaleByXY(this Transform transform, float r)
		{
			transform.ScaleByXY(r, r);
		}

        //-------------------------------------------------------------------------------------
        /// <summary>
        /// Scale this transform in the X and Z directions.
        /// </summary>
        public static void ScaleByXZ(this Transform transform, float r)
		{
			transform.ScaleByXZ(r, r);
		}

        //-------------------------------------------------------------------------------------
        /// <summary>
        /// Scale this transform in the Y and Z directions.
        /// </summary>
        public static void ScaleByYZ(this Transform transform, float r)
		{
			transform.ScaleByYZ(r, r);
		}

        //-------------------------------------------------------------------------------------
        /// <summary>
        /// Scale this transform in the X, Y and Z directions.
        /// </summary>
        public static void ScaleByXYZ(this Transform transform, float x, float y, float z)
		{
			transform.localScale = new Vector3(x, y, z);
		}

        //-------------------------------------------------------------------------------------
        /// <summary>
        /// Scale this transform in the X, Y and Z directions.
        /// </summary>
        public static void ScaleByXYZ(this Transform transform, float r)
		{
			transform.ScaleByXYZ(r, r, r);
		}

        //-------------------------------------------------------------------------------------
        /// <summary>
        /// Resets the local scale of this transform in to 1 1 1.
        /// </summary>
        public static void ResetScale(this Transform transform)
		{
			transform.localScale = Vector3.one;
		}

        //-------------------------------------------------------------------------------------
        /// <summary>
        /// Negates the X scale.
        /// </summary>
        public static void FlipX(this Transform transform)
		{
			transform.SetScaleX(-transform.localScale.x);
		}

        //-------------------------------------------------------------------------------------
        /// <summary>
        /// Negates the Y scale.
        /// </summary>
        public static void FlipY(this Transform transform)
		{
			transform.SetScaleY(-transform.localScale.y);
		}

        //-------------------------------------------------------------------------------------
        /// <summary>
        /// Negates the Z scale.
        /// </summary>
        public static void FlipZ(this Transform transform)
		{
			transform.SetScaleZ(-transform.localScale.z);
		}

        //-------------------------------------------------------------------------------------
        /// <summary>
        /// Negates the X and Y scale.
        /// </summary>
        public static void FlipXY(this Transform transform)
		{
			transform.SetScaleXY(-transform.localScale.x, -transform.localScale.y);
		}

        //-------------------------------------------------------------------------------------
        /// <summary>
        /// Negates the X and Z scale.
        /// </summary>
        public static void FlipXZ(this Transform transform)
		{
			transform.SetScaleXZ(-transform.localScale.x, -transform.localScale.z);
		}

        //-------------------------------------------------------------------------------------
        /// <summary>
        /// Negates the Y and Z scale.
        /// </summary>
        public static void FlipYZ(this Transform transform)
		{
			transform.SetScaleYZ(-transform.localScale.y, -transform.localScale.z);
		}

        //-------------------------------------------------------------------------------------
        /// <summary>
        /// Negates the X, Y and Z scale.
        /// </summary>
        public static void FlipXYZ(this Transform transform)
		{
			transform.SetScaleXYZ(-transform.localScale.z, -transform.localScale.y, -transform.localScale.z);
		}

        //-------------------------------------------------------------------------------------
        /// <summary>
        /// Sets all scale values to the absolute values.
        /// </summary>
        public static void FlipPostive(this Transform transform)
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
        /// Rotates the transform around the X axis.
        /// </summary>
        public static void RotateAroundX(this Transform transform, float angle)
		{
			Vector3 rotation = new Vector3(angle, 0, 0);
			transform.Rotate(rotation);
		}

        //-------------------------------------------------------------------------------------
        /// <summary>
        /// Rotates the transform around the Y axis.
        /// </summary>
        public static void RotateAroundY(this Transform transform, float angle)
		{
			Vector3 rotation = new Vector3(0, angle, 0);
			transform.Rotate(rotation);
		}

        //-------------------------------------------------------------------------------------
        /// <summary>
        /// Rotates the transform around the Z axis.
        /// </summary>
        public static void RotateAroundZ(this Transform transform, float angle)
		{
			Vector3 rotation = new Vector3(0, 0, angle);
			transform.Rotate(rotation);
		}

        //-------------------------------------------------------------------------------------
        /// <summary>
        /// Sets the X rotation.
        /// </summary>
        public static void SetRotationX(this Transform transform, float angle)
		{
			transform.eulerAngles = new Vector3(angle, 0, 0);
		}

        //-------------------------------------------------------------------------------------
        /// <summary>
        /// Sets the Y rotation.
        /// </summary>
        public static void SetRotationY(this Transform transform, float angle)
		{
			transform.eulerAngles = new Vector3(0, angle, 0);
		}

        //-------------------------------------------------------------------------------------
        /// <summary>
        /// Sets the Z rotation.
        /// </summary>
        public static void SetRotationZ(this Transform transform, float angle)
		{
			transform.eulerAngles = new Vector3(0, 0, angle);
		}

        //-------------------------------------------------------------------------------------
        /// <summary>
        /// Sets the local X rotation.
        /// </summary>
        public static void SetLocalRotationX(this Transform transform, float angle)
		{
			transform.localRotation = Quaternion.Euler(new Vector3(angle, 0, 0));
		}

        //-------------------------------------------------------------------------------------
        /// <summary>
        /// Sets the local Y rotation.
        /// </summary>
        public static void SetLocalRotationY(this Transform transform, float angle)
		{
			transform.localRotation = Quaternion.Euler(new Vector3(0, angle, 0));
		}

        //-------------------------------------------------------------------------------------
        /// <summary>
        /// Sets the local Z rotation.
        /// </summary>
        public static void SetLocalRotationZ(this Transform transform, float angle)
		{
			transform.localRotation = Quaternion.Euler(new Vector3(0, 0, angle));
		}

        //-------------------------------------------------------------------------------------
        /// <summary>
        /// Resets the rotation to 0, 0, 0.
        /// </summary>
        public static void ResetRotation(this Transform transform)
		{
			transform.rotation = Quaternion.identity;
		}

        //-------------------------------------------------------------------------------------
        /// <summary>
        /// Resets the local rotation to 0, 0, 0.
        /// </summary>
        public static void ResetLocalRotation(this Transform transform)
		{
			transform.localRotation = Quaternion.identity;
		}

        //-------------------------------------------------------------------------------------
        /// <summary>
        /// Resets the ;local position, local rotation, and local scale.
        /// </summary>
        public static void ResetLocal(this Transform transform)
		{
			transform.ResetLocalRotation();
			transform.ResetLocalPosition();
			transform.ResetScale();

		}

        //-------------------------------------------------------------------------------------
        /// <summary>
        /// Resets the position, rotation, and local scale.
        /// </summary>
        public static void Reset(this Transform transform)
		{
			transform.ResetRotation();
			transform.ResetPosition();
			transform.ResetScale();
		}

        //-------------------------------------------------------------------------------------
        /// <summary>
        /// Retrieve all children from this transform and destroy them.
        /// </summary>
        /// <param name="transform"></param>
        public static void DestroyChildren(this Transform transform)
		{
            List<Transform> children = transform.GetChildren();

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
        public static void DestroyChildrenImmediate(this Transform transform)
		{
            List<Transform> children = transform.GetChildren();

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
        public static void DestroyChildrenUniversal(this Transform transform)
		{
			if (Application.isPlaying)
			{
				transform.DestroyChildren();
			}
			else
			{
				transform.DestroyChildrenImmediate();
			}
		}

        //-------------------------------------------------------------------------------------
        /// <summary>
        /// Retrieve all children of this transform
        /// </summary>
        /// <param name="transform"></param>
        /// <returns></returns>
        public static List<Transform> GetChildren(this Transform transform)
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
