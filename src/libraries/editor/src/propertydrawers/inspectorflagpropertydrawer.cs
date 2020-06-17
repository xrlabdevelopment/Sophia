using Sophia;
using UnityEditor;
using UnityEngine;

namespace Sophia.Editor.PropertyDrawers
{
	/// <summary>
	/// A property drawer for fields marked with the InspectorFlags Attribute.
	/// </summary>
	[CustomPropertyDrawer(typeof(InspectorFlagsAttribute))]
	public class InspectorFlagsPropertyDrawer : PropertyDrawer
	{
        #region Unity Messages

        //-------------------------------------------------------------------------------------
        public override void OnGUI(Rect position, SerializedProperty prop, GUIContent label)
		{
			EditorGUI.showMixedValue = prop.hasMultipleDifferentValues;
			EditorGUI.BeginChangeCheck();

			int new_value = EditorGUI.MaskField(position, label, prop.intValue, prop.enumNames);

			if (EditorGUI.EndChangeCheck())
			{
				prop.intValue = new_value;
			}
		}

        #endregion
    }
}
