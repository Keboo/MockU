using System.Reflection;

namespace MockU;

internal sealed class EventHandlerCollection

{
    private readonly Dictionary<EventInfo, Delegate> eventHandlers;

    public EventHandlerCollection()
    {
        eventHandlers = new Dictionary<EventInfo, Delegate>();
    }

    public void Add(EventInfo @event, Delegate eventHandler)
    {
        lock (eventHandlers)
        {
            eventHandlers[@event] = Delegate.Combine(TryGet(@event), eventHandler);
        }
    }

    public void Clear()
    {
        lock (eventHandlers)
        {
            eventHandlers.Clear();
        }
    }

    public void Remove(EventInfo @event, Delegate eventHandler)
    {
        lock (eventHandlers)
        {
            eventHandlers[@event] = Delegate.Remove(TryGet(@event), eventHandler);
        }
    }

    public bool TryGet(EventInfo @event, out Delegate handlers)
    {
        lock (eventHandlers)
        {
            return eventHandlers.TryGetValue(@event, out handlers) && handlers != null;

            /* Unmerged change from project 'Moq(netstandard2.0)'
            Before:
                    private Delegate TryGet(EventInfo @event)
            After:
                    Delegate TryGet(EventInfo @event)
            */

            /* Unmerged change from project 'Moq(netstandard2.1)'
            Before:
                    private Delegate TryGet(EventInfo @event)
            After:
                    Delegate TryGet(EventInfo @event)
            */

            /* Unmerged change from project 'Moq(net6.0)'
            Before:
                    private Delegate TryGet(EventInfo @event)
            After:
                    Delegate TryGet(EventInfo @event)
            */
        }
    }

    private Delegate TryGet(EventInfo @event)
    {
        return eventHandlers.TryGetValue(@event, out var handlers) ? handlers : null;
    }
}
