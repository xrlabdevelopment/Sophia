namespace Sophia.Core
{
    public interface IInputManager
    {
        bool isDown(KeyCode code);
        bool isUp(KeyCode code);
        bool isPressed(KeyCode code);
        bool isReleased(KeyCode code);

        float getAxis(string axis);
    }
}