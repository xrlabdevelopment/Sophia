namespace Sophia.Core
{
    public interface IInputManager
    {
        bool isDown();
        bool isUp();
        bool isPressed();
        bool isReleased();
    }
}