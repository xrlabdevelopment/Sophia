using UnityEngine;

namespace Sophia.Platform.Attributes
{
	/// <summary>
	/// Used to mark the last field in a MonoBehaviour
    /// This is useful to add a decorator that should be displayed below all fields.
	/// </summary>
	/// <seealso cref="UnityEngine.PropertyAttribute" />
	public class DummyAttribute : PropertyAttribute
	{ }
}
