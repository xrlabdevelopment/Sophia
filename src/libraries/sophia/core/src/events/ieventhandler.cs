namespace Sophia.Core
{
    public interface IEventHandler
    {
        void handleEvent(IEvent evt);
    }
}
