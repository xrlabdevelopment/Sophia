using UnityEngine;

namespace Sophia.Platform
{
	public interface IMobileInputManager
	{
		int touchCount { get; }

		Touch getTouch(int index);
	}
}