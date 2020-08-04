using UnityEditor;
using UnityEngine;

namespace Sophia.Editor
{
	/// <summary>
	/// Functions to supplement Unity EditorGUI functions.
	/// </summary>
	public static class BaseEditorHelpers
	{
        //--------------------------------------------------------------------------------------
        // Constants
        public static readonly GUIStyle SplitterStyle;
		public static readonly GUIStyle LineStyle;

		private static readonly Color SplitterColor = EditorGUIUtility.isProSkin
            ? new Color(0.157f, 0.157f, 0.157f)
            : new Color(0.5f, 0.5f, 0.5f);

        //--------------------------------------------------------------------------------------
        static BaseEditorHelpers()
		{
			SplitterStyle = new GUIStyle
			{
				normal = {background = EditorGUIUtility.whiteTexture},
				stretchWidth = true,
				margin = new RectOffset(0, 0, 7, 7)
			};

			LineStyle = new GUIStyle
			{
				normal = { background = EditorGUIUtility.whiteTexture },
				stretchWidth = true,
				margin = new RectOffset(0, 0, 0, 0)
			};
		}

        //--------------------------------------------------------------------------------------
        public static void splitter(Color rgb, float thickness = 1)
		{
			Rect position = GUILayoutUtility.GetRect(GUIContent.none, SplitterStyle, GUILayout.Height(thickness));

			if (Event.current.type == EventType.Repaint)
			{
				Color restoreColor = GUI.color;
				GUI.color = rgb;
				SplitterStyle.Draw(position, false, false, false, false);
				GUI.color = restoreColor;
			}
		}
        //--------------------------------------------------------------------------------------
        public static void splitter(float thickness, GUIStyle splitterStyle)
		{
			Rect position = GUILayoutUtility.GetRect(GUIContent.none, splitterStyle, GUILayout.Height(thickness));

			if (Event.current.type == EventType.Repaint)
			{
				Color restoreColor = GUI.color;
				GUI.color = SplitterColor;
				splitterStyle.Draw(position, false, false, false, false);
				GUI.color = restoreColor;
			}
		}
        //--------------------------------------------------------------------------------------
        public static void splitter(float thickness = 1)
        {
            splitter(thickness, SplitterStyle);
        }
        //--------------------------------------------------------------------------------------
        public static void splitter(Rect position)
        {
            if (Event.current.type == EventType.Repaint)
            {
                Color restoreColor = GUI.color;
                GUI.color = SplitterColor;
                SplitterStyle.Draw(position, false, false, false, false);
                GUI.color = restoreColor;
            }
        }

        //--------------------------------------------------------------------------------------
        public static void verticalLine()
		{
			verticalLine(SplitterColor, 2);
		}
        //--------------------------------------------------------------------------------------
        public static void verticalLine(Color color, float thickness = 1)
		{
			Rect position = GUILayoutUtility.GetRect(
				GUIContent.none,
				LineStyle, 
				GUILayout.Width(thickness),
				GUILayout.ExpandHeight(true));

			if (Event.current.type == EventType.Repaint)
			{
				Color restoreColor = GUI.color;
				GUI.color = color;
				LineStyle.Draw(position, false, false, false, false);
				GUI.color = restoreColor;
			}
		}
        //--------------------------------------------------------------------------------------
        public static void verticalLine(float thickness, GUIStyle splitterStyle)
		{
			Rect position = GUILayoutUtility.GetRect(GUIContent.none, splitterStyle, GUILayout.Width(thickness));

			if (Event.current.type == EventType.Repaint)
			{
				Color restoreColor = GUI.color;
				GUI.color = SplitterColor;
				splitterStyle.Draw(position, false, false, false, false);
				GUI.color = restoreColor;
			}
		}
	}
}
