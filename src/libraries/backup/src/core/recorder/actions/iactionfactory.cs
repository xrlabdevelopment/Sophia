namespace Sophia.Deprecated
{
    public interface IActionFactory
    {
        IAction createAction(int actionType);
        IAction createSerializableAction(int actionType);
    }
}
