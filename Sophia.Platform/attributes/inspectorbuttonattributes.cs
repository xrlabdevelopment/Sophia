using System;

namespace Sophia
{
    /// <summary>
    /// Make an inspector button
    /// </summary>
    /// <seealso cref="System.Attribute" />
    [AttributeUsage(System.AttributeTargets.Method)]
	public class InspectorButtonAttribute : Attribute
	{ }
}
