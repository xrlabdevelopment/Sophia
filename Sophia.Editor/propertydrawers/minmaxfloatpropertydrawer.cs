using UnityEditor;
using UnityEngine;

namespace Sophia.Editor.PropertyDrawers
{
    /// <summary>
    /// A property drawer for the MinMaxFloat class.
    /// </summary>
    [CustomPropertyDrawer(typeof (MinMaxFloat))]
	public class MinMaxFloatPropertyDrawer : PropertyDrawer
	{
        #region Unity Messages

        //-------------------------------------------------------------------------------------
        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
		{
			EditorGUI.BeginProperty(position, label, property);

			position = EditorGUI.PrefixLabel(position, GUIUtility.GetControlID(FocusType.Passive), label);

            SerializedProperty min_prop = property.FindPropertyRelative("min");
            SerializedProperty max_prop = property.FindPropertyRelative("max");

            float min_value = min_prop.floatValue;
            float max_value = max_prop.floatValue;

			//TODO: find a way to support other extremes than 0 and 1.
			EditorGUI.MinMaxSlider(position, ref min_value, ref max_value, 0, 1);

			if (GUI.changed)
			{
                min_prop.floatValue = min_value;
                max_prop.floatValue = max_value;
			}

			EditorGUI.EndProperty();
		}

        #endregion
    }
}
