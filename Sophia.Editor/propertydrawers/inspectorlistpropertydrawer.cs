using Sophia.Platform.Extension;
using UnityEditor;
using UnityEditorInternal;
using UnityEngine;

namespace Sophia.Editor.PropertyDrawers
{
    /// <summary>
    /// A property drawer for type InspectorList.
    /// </summary>
    [CustomPropertyDrawer(typeof (InspectorList), true)]
	public class InspectorListPropertyDrawer : PropertyDrawer
	{
        //-------------------------------------------------------------------------------------
        // Fields
        private ReorderableList reorderable_list;
		private float last_height = 0;

        #region Unity Messages

        //-------------------------------------------------------------------------------------
        public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
		{
			SerializedProperty list = property.FindPropertyRelative("values");
            if (list == null)
			{
				return 0;
			}

			initList(list, property);

            if (reorderable_list != null)
			{
				return reorderable_list.GetHeight();
			}

			return last_height;
		}

        //-------------------------------------------------------------------------------------
        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
		{
            SerializedProperty list = property.FindPropertyRelative("values");
            if (list == null)
			{
				return;
			}

			int indent_level = EditorGUI.indentLevel;

            initList(list, property);

            if (list.arraySize > 0)
            {
                reorderable_list.elementHeight = EditorGUI.GetPropertyHeight(list.GetArrayElementAtIndex(0));
            }

			if(position.height <= 0)
			{
				return;
			}

			last_height = reorderable_list.GetHeight();

			reorderable_list.DoList(position);
			
			EditorGUI.indentLevel = indent_level;
		}

        #endregion

        //-------------------------------------------------------------------------------------
        private void initList(SerializedProperty list, SerializedProperty property)
		{
			if (reorderable_list == null)
			{
				reorderable_list = new ReorderableList(property.serializedObject, list, true, true, true, true)
				{
					drawElementCallback = (rect, index, isActive, isFocused) =>
						{
                            SerializedProperty label_property = list.GetArrayElementAtIndex(index);
                            SerializedProperty potential_property = null;

							int max_check = 0;
							while (label_property.Next(true) && max_check++ < 3)
							{
								if (label_property.propertyType == SerializedPropertyType.String)
								{
									//TODO: this is always true
                                    if (label_property.name == "name" || potential_property == null)
									{
										potential_property = label_property;
										break;
									}
								}
							}

                            GUIContent item_label = potential_property == null
                                ? new GUIContent("Element: " + index)
                                : new GUIContent(label_property.stringValue);

							EditorGUI.PropertyField(rect, list.GetArrayElementAtIndex(index), item_label, true);
						},
					drawHeaderCallback = rect =>
						{
							EditorGUI.indentLevel++;
							EditorGUI.LabelField(rect, property.displayName);
						},
				};
			}
		}
	}
}
