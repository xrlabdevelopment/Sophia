using Sophia;
using UnityEditor;
using UnityEngine;

namespace Sophia.Editor.PropertyDrawers
{
	/// <summary>
	/// Property drawer for fields marked with the WarnIfNull.
	/// </summary>
	/// <seealso cref="UnityEditor.PropertyDrawer" />
	[CustomPropertyDrawer(typeof(WarningIfNullAttribute))]
	public class WarningIfNullPropertyDrawer : PropertyDrawer
	{
        #region Unity Messages

        //-------------------------------------------------------------------------------------
        public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
		{
            if (property.objectReferenceValue != null)
			{
				return base.GetPropertyHeight(property, label);
			}

            WarningIfNullAttribute warning_attribute = (WarningIfNullAttribute)attribute;

            GUIContent gui_content = new GUIContent(warning_attribute.WarningMessage);

			bool old_word_wrap = EditorStyles.miniLabel.wordWrap;

			EditorStyles.miniLabel.wordWrap = true;

			float height = EditorStyles.miniLabel.CalcHeight(gui_content, Screen.width - 19) + EditorGUI.GetPropertyHeight(property, label, true);

			EditorStyles.miniLabel.wordWrap = old_word_wrap;

			return height;

		}

        //-------------------------------------------------------------------------------------
        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
		{
            if (property.objectReferenceValue != null)
			{
				EditorGUI.PropertyField(position, property);
				return;
			}

            WarningIfNullAttribute warning_attribute = (WarningIfNullAttribute)attribute;

            GUIContent gui_content = new GUIContent(warning_attribute.WarningMessage);

            bool old_word_wrap = EditorStyles.miniLabel.wordWrap;

			EditorStyles.miniLabel.wordWrap = true;		

			Color color = GUI.color;
			Color content_color = GUI.contentColor;
			Color background_color = GUI.backgroundColor;

			if (EditorGUIUtility.isProSkin)
			{			
				GUI.color = Color.yellow;
			}
			else
			{
				EditorGUI.DrawRect(position, Color.yellow);

				GUI.contentColor = Color.black;
				GUI.backgroundColor = Color.yellow;
			}

			float graph_height = EditorGUI.GetPropertyHeight(property, label, true); ;
			float label_height = EditorStyles.miniLabel.CalcHeight(gui_content, Screen.width - 19);

			position.height = label_height;

			EditorGUI.LabelField(position, warning_attribute.WarningMessage, EditorStyles.miniLabel	);
					
			position.y += label_height;
			position.height = graph_height;

			EditorGUI.PropertyField(position, property);
			EditorStyles.miniLabel.wordWrap = old_word_wrap;

			if (EditorGUIUtility.isProSkin)
			{
				GUI.color = color;
			}
			else
			{
				GUI.contentColor = content_color;
				GUI.backgroundColor = background_color;
			}
		}

        #endregion
    }
}
