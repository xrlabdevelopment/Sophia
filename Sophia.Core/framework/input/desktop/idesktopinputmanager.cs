namespace Sophia.Core.Gameplay.Input
{
	public interface IDesktopInputManager
	{
		bool isDown(KeyCode code);
		bool isUp(KeyCode code);
		bool isPressed(KeyCode code);
		bool isReleased(KeyCode code);
	}
}
