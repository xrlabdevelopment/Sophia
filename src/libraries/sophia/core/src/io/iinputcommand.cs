namespace Sophia.Core
{
    public interface IInputCommand : ICommand
    {
        IInputManager InputManager { get; }

        bool isTriggered();
    }
}