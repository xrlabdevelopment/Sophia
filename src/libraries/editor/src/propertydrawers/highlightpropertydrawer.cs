using Sophia;
using UnityEditor;
using UnityEngine;

namespace Sophia.Editor.PropertyDrawers
{
	/// <summary>
	/// A property drawer for fields marked with the Highlight Attribute.
	/// </summary>
	[CustomPropertyDrawer(typeof(HighlightAttribute))]
	public class HighlightPropertyDrawer : PropertyDrawer
	{
        #region Unity Messages

        //-------------------------------------------------------------------------------------
        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
		{
            Color old_color = GUI.color;

			GUI.color = Color.blue;

			EditorGUI.PropertyField(position, property, label);

			GUI.color = old_color;
		}

        #endregion
    }
}
