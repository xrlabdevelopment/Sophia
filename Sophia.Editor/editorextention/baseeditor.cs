using System;
using System.Collections;
using System.Reflection;
using System.Linq;
using System.Collections.Generic;

using UnityEditor;
using UnityEngine;

using Sophia.Platform.Attributes;
using Sophia.Platform.Extension;

namespace Sophia.Editor.Extension
{
    /// <summary>
    /// Class that can be used as a base class for custom editors with extra convenience methods
    /// and properties.
    /// </summary>
    /// <typeparam name="T">The type this is an editor for.</typeparam>
    /// <seealso cref="UnityEditor.Editor" />
    public class BaseEditor<T> : UnityEditor.Editor
		where T : MonoBehaviour
	{
        //--------------------------------------------------------------------------------------
        // Properties
        public T Target
		{
			get { return (T) (object) target; }
		}

		public T[] Targets
		{
			get { return targets.Cast<T>().ToArray(); }
		}

        //--------------------------------------------------------------------------------------
        public bool hasProperty(string propertyName)
        {
            return serializedObject.FindProperty(propertyName) != null;
        }

        //--------------------------------------------------------------------------------------
        /// <summary>
        /// Draws a line as a separator in the inspector.
        /// </summary>
        public void addSplitter()
		{
			BaseEditorHelpers.splitter();
		}

        //--------------------------------------------------------------------------------------
        public static int addCombo(string[] options, int selectedIndex)
		{
			return EditorGUILayout.Popup(selectedIndex, options);
		}
        //--------------------------------------------------------------------------------------
        protected void addField(SerializedProperty prop)
		{
			EditorGUILayout.BeginHorizontal();
			EditorGUILayout.PropertyField(prop, true);
			EditorGUILayout.EndHorizontal();
		}
        //--------------------------------------------------------------------------------------
        protected void addLabel(string title, string text)
		{
			EditorGUILayout.LabelField(title, text);
		}
        //--------------------------------------------------------------------------------------
        protected void addTextAndButton(string text, string buttonLabel, Action buttonAction)
        {
            EditorGUILayout.BeginHorizontal();

            EditorGUILayout.LabelField(text, EditorStyles.boldLabel);

            if (GUILayout.Button(buttonLabel))
            {
                if (buttonAction != null)
                    buttonAction();
            }

            EditorGUILayout.EndHorizontal();
        }
        //--------------------------------------------------------------------------------------
        protected void arrayGUI(SerializedObject obj, SerializedProperty property)
        {
            int size = property.arraySize;
            int new_size = EditorGUILayout.IntField(property.name + " Size", size);

            if (new_size != size)
            {
                property.arraySize = new_size;
            }

            EditorGUI.indentLevel = 3;

            for (int i = 0; i < new_size; i++)
            {
                SerializedProperty prop = property.GetArrayElementAtIndex(i);
                EditorGUILayout.PropertyField(prop);
            }
        }

        //--------------------------------------------------------------------------------------
        /// <summary>
        /// Draws the buttons in the inspector for all method in the target class
        /// that are marked with the InspectorButtonAttribute.
        /// </summary>
        /// <param name="columnCount">The number of columns to draw the buttons in.</param>
        protected void drawInspectorButtons(int columnCount)
		{
			MethodInfo[] methods =  getParentTypes(Target.GetType())
							.SelectMany(type => type.GetMethods(BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.Instance | BindingFlags.Static))
							.Where(m => m.GetCustomAttributes(typeof(InspectorButtonAttribute), false).Length > 0)
							.ToArray();

			EditorGUILayout.BeginHorizontal();

			for (int i = 0; i < methods.Length; i++)
			{
				MethodInfo method = methods[i];

				if (GUILayout.Button(method.Name.splitCamelCase()))
				{
					if (method.ReturnType == typeof(IEnumerator))
					{
						Target.StartCoroutine((IEnumerator)method.Invoke(Target, new object[] { }));
					}
					else
					{
						method.Invoke(Target, new object[] { });
					}
				}

				if (i % columnCount == columnCount - 1)
				{
					EditorGUILayout.EndHorizontal();
					EditorGUILayout.BeginHorizontal();
				}
			}

			EditorGUILayout.EndHorizontal();
		}

        //--------------------------------------------------------------------------------------
        private static IEnumerable<Type> getParentTypes(Type type)
		{
			Type current_base_type = type;

			while (current_base_type != null)
			{
				yield return current_base_type;
                current_base_type = current_base_type.BaseType;
			}
		}
	}
}
