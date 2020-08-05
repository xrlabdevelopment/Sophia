namespace Sophia.Core.Patterns
{
    public interface ICommand
    {
        bool execute(ICommandReceiver receiver);
    }
}
