using System;
using UnityEngine;
using UnityEditor;

namespace Sophia.Editor.PropertyDrawers
{
    /// <summary>
    /// Property drawer for the label field attribute.
    /// </summary>
    /// <seealso cref="UnityEditor.PropertyDrawer" />
    [CustomPropertyDrawer(typeof (LabelFieldAttribute))]
	public class LabelFieldPropertyDrawer : PropertyDrawer
    {
        #region Unity Messages

        //-------------------------------------------------------------------------------------
        public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
		{
			return EditorGUI.GetPropertyHeight(property, label, true);
		}

        //-------------------------------------------------------------------------------------
        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
		{
            LabelFieldAttribute name_field_attribute = (LabelFieldAttribute) attribute;
			SerializedProperty name_property = property.FindPropertyRelative(name_field_attribute.LabelField);

			if (name_property != null)
			{
				string name = label.text;

				switch (name_property.propertyType)
				{
					case SerializedPropertyType.Generic:
						break;
					case SerializedPropertyType.Integer:
						name = name_property.intValue.ToString();
						break;
					case SerializedPropertyType.Boolean:
						name = name_property.boolValue.ToString();
						break;
					case SerializedPropertyType.Float:
						name = name_property.floatValue.ToString();
						break;
					case SerializedPropertyType.String:
						name = name_property.stringValue;
						break;
					case SerializedPropertyType.Color:
						name = property.colorValue.ToString();
						break;
					case SerializedPropertyType.ObjectReference:
						name = name_property.objectReferenceValue.name;
						break;
					case SerializedPropertyType.LayerMask:
						break;
					case SerializedPropertyType.Enum:
						name = name_property.enumDisplayNames[name_property.enumValueIndex];
						break;
					case SerializedPropertyType.Vector2:
						name = name_property.vector2Value.ToString();
						break;
					case SerializedPropertyType.Vector3:
						name = name_property.vector3Value.ToString();
						break;
					case SerializedPropertyType.Vector4:
						name = name_property.vector4Value.ToString();
						break;
					case SerializedPropertyType.Rect:
						name = name_property.rectValue.ToString();
						break;
					case SerializedPropertyType.ArraySize:
						break;
					case SerializedPropertyType.Character:
						break;
					case SerializedPropertyType.AnimationCurve:
						break;
					case SerializedPropertyType.Bounds:
						name = name_property.boundsValue.ToString();
						break;
					case SerializedPropertyType.Gradient:
						break;
					case SerializedPropertyType.Quaternion:
						name = name_property.quaternionValue.ToString();
						break;
					default:
						throw new ArgumentOutOfRangeException();
				}

				label.text = name;
			}

			EditorGUI.PropertyField(position, property, label, true);
		}

        #endregion
    }
}
