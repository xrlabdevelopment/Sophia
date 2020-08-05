using System;
using UnityEngine;

namespace Sophia.Platform.Attributes
{
    /// <summary>
    /// Used to mark inspectable fields as read-only (that is, making them uneditable, even if they are visible).
    /// </summary>
    /// <seealso cref="PropertyAttribute" />
    [AttributeUsage(AttributeTargets.Field)]
	public class ReadOnlyAttribute : PropertyAttribute
	{ }
}
