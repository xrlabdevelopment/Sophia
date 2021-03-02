using UnityEngine;

namespace Sophia.Platform.Extension
{
	/// <summary>
	/// Contains useful extension methods for vectors.
	/// </summary>
	public static class VectorExtensions
	{
        //-------------------------------------------------------------------------------------
        /// <summary>
        /// Returns a copy of this vector with the given x-coordinate.
        /// </summary>
        public static Vector2 withX(this Vector2 vector, float x)
		{
			return new Vector2(x, vector.y);
		}

        //-------------------------------------------------------------------------------------
        /// <summary>
        /// Returns a copy of this vector with the given y-coordinate.
        /// </summary>
        public static Vector2 withY(this Vector2 vector, float y)
		{
			return new Vector2(vector.x, y);
		}

        //-------------------------------------------------------------------------------------
        /// <summary>
        /// Returns a copy of this vector with the given x-coordinate.
        /// </summary>
        public static Vector3 withX(this Vector3 vector, float x)
		{
			return new Vector3(x, vector.y, vector.z);
		}

        //-------------------------------------------------------------------------------------
        /// <summary>
        /// Returns a copy of this vector with the given y-coordinate.
        /// </summary>
        public static Vector3 withY(this Vector3 vector, float y)
		{
			return new Vector3(vector.x, y, vector.z);
		}

        //-------------------------------------------------------------------------------------
        /// <summary>
        /// Returns a copy of this vector with the given z-coordinate.
        /// </summary>
        public static Vector3 withZ(this Vector3 vector, float z)
		{
			return new Vector3(vector.x, vector.y, z);
		}

        //-------------------------------------------------------------------------------------
        /// <summary>
        /// Returns a copy of the vector with the x-coordinate incremented
        /// with the given value.
        /// </summary>
        public static Vector2 withIncX(this Vector2 vector, float xInc)
		{
			return new Vector2(vector.x + xInc, vector.y);
		}

        //-------------------------------------------------------------------------------------
        /// <summary>
        /// Returns a copy of the vector with the y-coordinate incremented
        /// with the given value.
        /// </summary>
        public static Vector2 withIncY(this Vector2 vector, float yInc)
		{
			return new Vector2(vector.x, vector.y + yInc);
		}

        //-------------------------------------------------------------------------------------
        /// <summary>
        /// Returns a copy of the vector with the x-coordinate incremented
        /// with the given value.
        /// </summary>
        public static Vector3 withIncX(this Vector3 vector, float xInc)
		{
			return new Vector3(vector.x + xInc, vector.y, vector.z);
		}

        //-------------------------------------------------------------------------------------
        /// <summary>
        /// Returns a copy of the vector with the y-coordinate incremented
        /// with the given value.
        /// </summary>
        public static Vector3 withIncY(this Vector3 vector, float yInc)
		{
			return new Vector3(vector.x, vector.y + yInc, vector.z);
		}

        //-------------------------------------------------------------------------------------
        /// <summary>
        /// Returns a copy of the vector with the z-coordinate incremented
        /// with the given value.
        /// </summary>
        public static Vector3 withIncZ(this Vector3 vector, float zInc)
		{
			return new Vector3(vector.x, vector.y, vector.z + zInc);
		}

        //-------------------------------------------------------------------------------------
        /// <summary>
        /// Converts a 2D vector to a 3D vector using the vector 
        /// for the x and z coordinates, and the given value for the y coordinate.
        /// </summary>
        public static Vector3 to3DXZ(this Vector2 vector, float y)
		{
			return new Vector3(vector.x, y, vector.y);
		}

        //-------------------------------------------------------------------------------------
        /// <summary>
        /// Converts a 2D vector to a 3D vector using the vector 
        /// for the x and z coordinates, and 0 for the y coordinate.
        /// </summary>
        public static Vector3 to3DXZ(this Vector2 vector)
		{
			return vector.to3DXZ(0);
		}

        //-------------------------------------------------------------------------------------
        /// <summary>
        /// Converts a 2D vector to a 3D vector using the vector 
        /// for the x and y coordinates, and the given value for the z coordinate.
        /// </summary>
        public static Vector3 to3DXY(this Vector2 vector, float z)
		{
			return new Vector3(vector.x, vector.y, z);
		}

        //-------------------------------------------------------------------------------------
        /// <summary>
        /// Converts a 2D vector to a 3D vector using the vector 
        /// for the x and y coordinates, and 0 for the z coordinate.
        /// </summary>
        public static Vector3 to3DXY(this Vector2 vector)
		{
			return vector.to3DXY(0);
		}

        //-------------------------------------------------------------------------------------
        /// <summary>
        /// Converts a 2D vector to a 3D vector using the vector 
        /// for the y and z coordinates, and the given value for the x coordinate.
        /// </summary>
        public static Vector3 to3DYZ(this Vector2 vector, float x)
		{
			return new Vector3(x, vector.x, vector.y);
		}

        //-------------------------------------------------------------------------------------
        /// <summary>
        /// Converts a 2D vector to a 3D vector using the vector 
        /// for the y and z coordinates, and 0 for the x coordinate.
        /// </summary>
        public static Vector3 to3DYZ(this Vector2 vector)
		{
			return vector.to3DYZ(0);
		}

        //-------------------------------------------------------------------------------------
        /// <summary>
        /// Converts a 3D vector to a 2D vector taking the x and z coordinates.
        /// </summary>
        public static Vector2 to2DXZ(this Vector3 vector)
		{
			return new Vector2(vector.x, vector.z);
		}

        //-------------------------------------------------------------------------------------
        /// <summary>
        /// Converts a 3D vector to a 2D vector taking the x and y coordinates.
        /// </summary>
        public static Vector2 to2DXY(this Vector3 vector)
		{
			return new Vector2(vector.x, vector.y);
		}

        //-------------------------------------------------------------------------------------
        /// <summary>
        /// Converts a 3D vector to a 2D vector taking the y and z coordinates.
        /// </summary>
        public static Vector2 to2DYZ(this Vector3 vector)
		{
			return new Vector2(vector.y, vector.z);
		}

        //-------------------------------------------------------------------------------------
        /// <summary>
        /// Returns the vector rotated 90 degrees counter-clockwise.
        /// </summary>
        /// <remarks>
        /// 	<para>The returned vector is always perpendicular to the given vector. </para>
        /// 	<para>The perp dot product can be calculated using this: <c>var perpDotPorpduct = Vector2.Dot(v1.Perp(), v2);</c></para>
        /// </remarks>
        /// <param name="vector"></param>
        public static Vector2 perp(this Vector2 vector)
		{
            return new Vector2(-vector.y, vector.x);
        }

        //-------------------------------------------------------------------------------------
        /// <summary>
        /// Equivalent to Vector2.Dot(v1, v2).
        /// </summary>
        /// <param name="vector1">The first operand.</param>
        /// <param name="vector2">The second operand.</param>
        /// <returns>Vector2.</returns>
        public static float dot(this Vector2 vector1, Vector2 vector2)
		{
			return vector1.x * vector2.x + vector1.y * vector2.y;
		}

        //-------------------------------------------------------------------------------------
        /// <summary>
        /// Equivalent to Vector3.Dot(v1, v2).
        /// </summary>
        /// <param name="vector1">The first operand.</param>
        /// <param name="vector2">The second operand.</param>
        /// <returns>Vector3.</returns>
        public static float dot(this Vector3 vector1, Vector3 vector2)
		{
			return vector1.x * vector2.x + vector1.y * vector2.y + vector1.z * vector2.z;
		}

        //-------------------------------------------------------------------------------------
        /// <summary>
        /// Equivalent to Vector4.Dot(v1, v2).
        /// </summary>
        /// <param name="vector1">The first operand.</param>
        /// <param name="vector2">The second operand.</param>
        /// <returns>Vector4.</returns>
        public static float dot(this Vector4 vector1, Vector4 vector2)
		{
			return vector1.x * vector2.x + vector1.y * vector2.y + vector1.z * vector2.z + vector1.w * vector2.w;
		}

        //-------------------------------------------------------------------------------------
        /// <summary>
        /// Returns the projection of this vector onto the given base.
        /// </summary>
        /// <param name="vector"></param>
        /// <param name="baseVector"></param>
        public static Vector2 project(this Vector2 vector, Vector2 baseVector)
		{
            Vector2 direction = baseVector.normalized;
            float magnitude = Vector2.Dot(vector, direction);

			return direction * magnitude;
		}

        //-------------------------------------------------------------------------------------
        /// <summary>
        /// Returns the projection of this vector onto the given base.
        /// </summary>
        /// <param name="vector"></param>
        /// <param name="baseVector"></param>

        public static Vector3 project(this Vector3 vector, Vector3 baseVector)
		{
            Vector3 direction = baseVector.normalized;
            float magnitude = Vector2.Dot(vector, direction);

			return direction * magnitude;
		}

        //-------------------------------------------------------------------------------------
        /// <summary>
        /// Returns the projection of this vector onto the given base.
        /// </summary>
        /// <param name="vector"></param>
        /// <param name="baseVector"></param>
        public static Vector4 project(this Vector4 vector, Vector4 baseVector)
		{
            Vector4 direction = baseVector.normalized;
            float magnitude = Vector2.Dot(vector, direction);

			return direction * magnitude;
		}
    }
}
