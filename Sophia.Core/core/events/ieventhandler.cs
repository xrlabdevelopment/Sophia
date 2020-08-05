namespace Sophia.Core.Events
{
    /// <summary>
    /// Interface of an event handler
    /// </summary>
    public interface IEventHandler
        {
            /// <summary>
            /// If the event that is fired is equal to the event category of the handler
            /// The event will be passed along, otherwise this handler will be skipped
            /// </summary>
            int EventCategory { get; }

            /// <summary>
            /// Function to be called when we process an event
            /// </summary>
            /// <param name="evt">The event being processed</param>
            /// <returns>True if the event was handled, false if it wasn't handled</returns>
            bool handleEvent(IEvent evt);
        }
    }
