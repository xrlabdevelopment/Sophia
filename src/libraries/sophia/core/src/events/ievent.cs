namespace Sophia.Core
{
    public interface IEvent
    {
        int EventType { get; }

        IEventSender Sender { get; }
    }
}
