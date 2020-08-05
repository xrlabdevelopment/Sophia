using Sophia.Platform.Attributes;
using UnityEditor;
using UnityEngine;

namespace Sophia.Editor.PropertyDrawers
{
    /// <summary>
    /// Draws a property marked with the Dummy attribute (that is, does not draw it).
    /// </summary>
    [CustomPropertyDrawer(typeof(DummyAttribute))]
	public class DummyPorpertyDrawer : PropertyDrawer
	{
        #region Unity Messages

        //-------------------------------------------------------------------------------------
        public override void OnGUI(Rect position, SerializedProperty prop, GUIContent label)
		{
			// Nothing to implement
		}

        //-------------------------------------------------------------------------------------
        public override float GetPropertyHeight(SerializedProperty prop, GUIContent label)
		{
            return 0.0f;
		}

        #endregion
    }
}
