namespace Sophia.Core
{
    public interface ICommand
    {
        bool execute(ICommandReceiver receiver);
    }
}