namespace Sophia.Core.Input
{
    public enum MultiFunctionalCommand
    {
        YES,
        NO
    }

    public interface IInputCommand : ICommand
    {
        IInputManager InputManager { get; }
        bool IsMultifuntional { get; set; }

        bool isTriggered();
    }
}
