namespace Sophia.Core
{
    public interface ICommand
    {
        void execute(ICommandReceiver receiver);
    }
}