using UnityEngine;
using UnityEditor;
using Sophia.Platform.Utilities;

namespace Sophia.Editor.PropertyDrawers
{
	/// <summary>
	/// Class for drawing a field of type <see cref="Optional"/> in the inspector.
	/// </summary>
	[CustomPropertyDrawer(typeof(Optional), true)]
	public class OptionalPropertyDrawer : PropertyDrawer
	{
        #region Unity Messages

        //-------------------------------------------------------------------------------------
        public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
		{
			SerializedProperty value_property = property.FindPropertyRelative("value");

			return EditorGUI.GetPropertyHeight(value_property, label, true);
		}

        //-------------------------------------------------------------------------------------
        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
		{
            SerializedProperty use_value_property = property.FindPropertyRelative("useValue");
            SerializedProperty value_property = property.FindPropertyRelative("value");
		
			var toggle_rect = new Rect(position.x, position.y, 16, position.size.y);
			var value_rect = new Rect(40, position.y, position.size.x - 30 +14, position.size.y);

			EditorGUI.BeginProperty(toggle_rect, label, property);
            use_value_property.boolValue = EditorGUI.Toggle(toggle_rect, use_value_property.boolValue);

			bool old_enabled = GUI.enabled;

			GUI.enabled = use_value_property.boolValue;
		
			EditorGUIUtility.labelWidth = EditorGUIUtility.labelWidth - 26;
		    EditorGUI.PropertyField(value_rect, value_property, new GUIContent() {text = property.displayName}, true);
			
			GUI.enabled = old_enabled;

			EditorGUI.EndProperty();
		}

        #endregion
    }
}
