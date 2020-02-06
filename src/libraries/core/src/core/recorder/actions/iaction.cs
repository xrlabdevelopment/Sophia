namespace Sophia.Core
{
    public interface IAction
    {
        int ActionType { get; }
        string Name { get; }
    }
}
