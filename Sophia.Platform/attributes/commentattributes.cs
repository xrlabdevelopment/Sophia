using System;
using UnityEngine;

namespace Sophia.Platform.Attributes
{
	/// <summary>
	/// Used to mark a field to add a comment above the field in the inspector.
	/// </summary>
	/// <seealso cref="UnityEngine.PropertyAttribute" />
	[AttributeUsage(AttributeTargets.Field)]
	public class CommentAttribute : PropertyAttribute
	{
        //-------------------------------------------------------------------------------------
        // Fields
        public GUIContent Content { get; private set; }

        //-------------------------------------------------------------------------------------
        public CommentAttribute(string comment, string tooltip = "")
		{
            Content = string.IsNullOrEmpty(tooltip)
                ? new GUIContent(comment)
                : new GUIContent(comment + " [?]", tooltip);
		}
	}
}
