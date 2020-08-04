using UnityEngine;
using UnityEditor;

namespace Sophia.Editor
{
    /// <summary>
    /// A property drawer for fields marked with the CommentAttribute. Similar to Header, but useful
    /// for longer descriptions.
    /// </summary>
    [CustomPropertyDrawer(typeof(CommentAttribute), useForChildren: true)]
	public class CommentPropertyDrawer : DecoratorDrawer
	{
        #region Unity Messages

        //-------------------------------------------------------------------------------------
        public override float GetHeight()
		{
            CommentAttribute comment_attribute = (CommentAttribute)attribute;

            return EditorStyles.whiteLabel.CalcHeight(comment_attribute.Content, Screen.width - 19);
		}

        //-------------------------------------------------------------------------------------
        public override void OnGUI(Rect position)
		{
            CommentAttribute comment_attribute = (CommentAttribute)attribute;

            EditorGUI.BeginDisabledGroup(true);

			EditorGUI.LabelField(position, comment_attribute.Content);

			EditorGUI.EndDisabledGroup();
		}

        #endregion
    }
}
