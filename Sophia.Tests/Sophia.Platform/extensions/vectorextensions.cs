using Sophia.Platform.Extension;

using NUnit.Framework;

using UnityEngine;

namespace Sophia.Tests.Platform
{
    [TestFixture]
    public class testsuit_VectorExtensions
    {
        //--------------------------------------------------------------------------------------
        private bool nearlyEqual(float a, float b, float epsilon)
        {
            const double MIN_NORMAL = 2.2250738585072014E-308d;

            float absA = Mathf.Abs(a);
            float absB = Mathf.Abs(b);
            float diff = Mathf.Abs(a - b);

            if (a == b)
            { // shortcut, handles infinities
                return true;
            }
            else if (a == 0 || b == 0 || (absA + absB < MIN_NORMAL))
            {
                // a or b is zero or both are extremely close to it
                // relative error is less meaningful here
                return diff < (epsilon * MIN_NORMAL);
            }
            else
            { // use relative error
                return diff / Mathf.Min((absA + absB), float.MaxValue) < epsilon;
            }
        }

        //--------------------------------------------------------------------------------------
        [TestCase]
        public void test_VectorExtensions_withX_Vector2()
        {
            Vector2 v = Vector2.zero;
            Vector2 new_v = v.withX(5.0f);
            Vector2 expected_v = new Vector2(5.0f, v.y);

            Assert.AreEqual(new_v, expected_v);
        }
        //--------------------------------------------------------------------------------------
        [TestCase]
        public void test_VectorExtensions_withY_Vector2()
        {
            Vector2 v = Vector2.zero;
            Vector2 new_v = v.withY(5.0f);
            Vector2 expected_v = new Vector2(v.x, 5.0f);

            Assert.AreEqual(new_v, expected_v);
        }
        //--------------------------------------------------------------------------------------
        [TestCase]
        public void test_VectorExtensions_withX_Vector3()
        {
            Vector3 v = Vector3.zero;
            Vector3 new_v = v.withX(5.0f);
            Vector3 expected_v = new Vector3(5.0f, v.y, v.z);

            Assert.AreEqual(new_v, expected_v);
        }
        //--------------------------------------------------------------------------------------
        [TestCase]
        public void test_VectorExtensions_withY_Vector3()
        {
            Vector3 v = Vector3.zero;
            Vector3 new_v = v.withY(5.0f);
            Vector3 expected_v = new Vector3(v.x, 5.0f, v.z);

            Assert.AreEqual(new_v, expected_v);
        }
        //--------------------------------------------------------------------------------------
        [TestCase]
        public void test_VectorExtensions_withZ_Vector3()
        {
            Vector3 v = Vector3.zero;
            Vector3 new_v = v.withZ(5.0f);
            Vector3 expected_v = new Vector3(v.x, v.y, 5.0f);

            Assert.AreEqual(new_v, expected_v);
        }
        //--------------------------------------------------------------------------------------
        [TestCase]
        public void test_VectorExtensions_withIncX_Vector2()
        {
            Vector2 v = Vector2.zero;
            Vector2 new_v = v.withIncX(5.0f);
            Vector2 expected_v = new Vector2(v.x + 5.0f, v.y);

            Assert.AreEqual(new_v, expected_v);
        }
        //--------------------------------------------------------------------------------------
        [TestCase]
        public void test_VectorExtensions_withIncY_Vector2()
        {
            Vector2 v = Vector2.zero;
            Vector2 new_v = v.withIncY(5.0f);
            Vector2 expected_v = new Vector2(v.x, v.y + 5.0f);

            Assert.AreEqual(new_v, expected_v);
        }
        //--------------------------------------------------------------------------------------
        [TestCase]
        public void test_VectorExtensions_withIncX_Vector3()
        {
            Vector3 v = Vector3.zero;
            Vector3 new_v = v.withIncX(5.0f);
            Vector3 expected_v = new Vector3(v.x + 5.0f, v.y, v.z);

            Assert.AreEqual(new_v, expected_v);
        }
        //--------------------------------------------------------------------------------------
        [TestCase]
        public void test_VectorExtensions_withIncY_Vector3()
        {
            Vector3 v = Vector3.zero;
            Vector3 new_v = v.withIncY(5.0f);
            Vector3 expected_v = new Vector3(v.x, v.y + 5.0f, v.z);

            Assert.AreEqual(new_v, expected_v);
        }
        //--------------------------------------------------------------------------------------
        [TestCase]
        public void test_VectorExtensions_withIncZ_Vector3()
        {
            Vector3 v = Vector3.zero;
            Vector3 new_v = v.withIncZ(5.0f);
            Vector3 expected_v = new Vector3(v.x, v.y, v.z + 5.0f);

            Assert.AreEqual(new_v, expected_v);
        }
        //--------------------------------------------------------------------------------------
        [TestCase]
        public void test_VectorExtensions_to3DXZ()
        {
            Vector2 v = Vector2.zero;
            Vector3 new_v = v.to3DXZ(5.0f);
            Vector3 expected_v = new Vector3(v.x, 5.0f, v.y);

            Assert.AreEqual(new_v, expected_v);

            v = Vector2.zero;
            new_v = v.to3DXZ();
            expected_v = new Vector3(v.x, 0.0f, v.y);

            Assert.AreEqual(new_v, expected_v);
        }
        //--------------------------------------------------------------------------------------
        [TestCase]
        public void test_VectorExtensions_to3DXY()
        {
            Vector2 v = Vector2.zero;
            Vector3 new_v = v.to3DXY(5.0f);
            Vector3 expected_v = new Vector3(v.x, v.y, 5.0f);

            Assert.AreEqual(new_v, expected_v);

            v = Vector2.zero;
            new_v = v.to3DXY();
            expected_v = new Vector3(v.x, v.y, 0.0f);

            Assert.AreEqual(new_v, expected_v);
        }
        //--------------------------------------------------------------------------------------
        [TestCase]
        public void test_VectorExtensions_to3DYZ()
        {
            Vector2 v = Vector2.zero;
            Vector3 new_v = v.to3DYZ(5.0f);
            Vector3 expected_v = new Vector3(5.0f, v.x, v.y);

            Assert.AreEqual(new_v, expected_v);

            v = Vector2.zero;
            new_v = v.to3DYZ();
            expected_v = new Vector3(0.0f, v.x, v.y);

            Assert.AreEqual(new_v, expected_v);
        }

        //--------------------------------------------------------------------------------------
        [TestCase]
        public void test_VectorExtensions_to2DXZ()
        {
            Vector3 v = Vector3.one;
            Vector2 new_v = v.to2DXZ();
            Vector2 expected_v = new Vector2(v.x, v.z);

            Assert.AreEqual(new_v, expected_v);
        }
        //--------------------------------------------------------------------------------------
        [TestCase]
        public void test_VectorExtensions_to2DXY()
        {
            Vector3 v = Vector3.one;
            Vector2 new_v = v.to2DXY();
            Vector2 expected_v = new Vector2(v.x, v.y);

            Assert.AreEqual(new_v, expected_v);
        }
        //--------------------------------------------------------------------------------------
        [TestCase]
        public void test_VectorExtensions_to2DYZ()
        {
            Vector3 v = Vector3.one;
            Vector2 new_v = v.to2DYZ();
            Vector2 expected_v = new Vector2(v.y, v.z);

            Assert.AreEqual(new_v, expected_v);
        }

        //--------------------------------------------------------------------------------------
        [TestCase]
        public void test_VectorExtensions_perp_Vector2()
        {
            Vector2 v = new Vector2(0, 1);
            Vector2 new_v = v.perp();
            Vector2 expected_v = new Vector2(-1, 0);

            Assert.AreEqual(new_v, expected_v);
        }
        //--------------------------------------------------------------------------------------
        [TestCase]
        public void test_VectorExtensions_dot_Vector2()
        {
            Vector2 vector1 = new Vector2(1, 2);
            Vector2 vector2 = new Vector2(3, 4);

            float dot = (vector1.x * vector2.x) + (vector1.y * vector2.y);

            Assert.AreEqual(vector1.dot(vector2), dot);
        }
        //--------------------------------------------------------------------------------------
        [TestCase]
        public void test_VectorExtensions_dot_Vector3()
        {
            Vector3 vector1 = new Vector3(1, 2, 3);
            Vector3 vector2 = new Vector3(4, 5, 6);

            float dot = (vector1.x * vector2.x) + (vector1.y * vector2.y) + (vector1.z * vector2.z);

            Assert.AreEqual(vector1.dot(vector2), dot);
        }
        //--------------------------------------------------------------------------------------
        [TestCase]
        public void test_VectorExtensions_dot_Vector4()
        {
            Vector4 vector1 = new Vector4(1, 2, 3, 4);
            Vector4 vector2 = new Vector4(5, 6, 7, 8);

            float dot = (vector1.x * vector2.x) + (vector1.y * vector2.y) + (vector1.z * vector2.z) + (vector1.w * vector2.w);

            Assert.AreEqual(vector1.dot(vector2), dot);
        }

        //--------------------------------------------------------------------------------------
        [TestCase]
        public void test_VectorExtensions_project_Vector2()
        {
            Vector2 v = Vector2.one;
            Vector2 b = Vector2.right;

            Vector2 p = v.project(b);

            Assert.AreEqual(p, new Vector2(1, 0));
        }
        //--------------------------------------------------------------------------------------
        [TestCase]
        public void test_VectorExtensions_project_Vector3()
        {
            Vector3 v = Vector3.one;
            Vector3 b = Vector3.right;

            Vector3 p = v.project(b);

            Assert.AreEqual(p, new Vector3(1, 0, 0));
        }
        //--------------------------------------------------------------------------------------
        [TestCase]
        public void test_VectorExtensions_project_Vector4()
        {
            const float PRECISION = 0.0001f;

            Vector4 v = Vector4.one;
            Vector4 b = new Vector4(1, 0, 0, 1);

            Vector4 p = v.project(b);

            Assert.IsTrue(nearlyEqual(p.x, 0.5f, PRECISION));
            Assert.IsTrue(nearlyEqual(p.y, 0.0f, PRECISION));
            Assert.IsTrue(nearlyEqual(p.z, 0.0f, PRECISION));
            Assert.IsTrue(nearlyEqual(p.w, 0.5f, PRECISION));
        }
    }
}
