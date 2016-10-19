
namespace GameEngine.GameEngineComponents
{
    /// <summary>
    /// Event queue service.
    /// </summary>
    /// <remarks>
    /// This is a fire and forget event queue. Implementations are not required
    /// to report back the result of the event.
    /// </remarks>
    public interface IEventQueue : IGameComponent
    {
        /// <summary>
        /// Add a new event to the event queue
        /// </summary>
        /// <param name="newEvent">Event</param>
        void AddEvent(IEvent newEvent);
    }
}
