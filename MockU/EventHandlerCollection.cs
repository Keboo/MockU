using System.Diagnostics.CodeAnalysis;
using System.Reflection;

namespace MockU;

internal sealed class EventHandlerCollection
{
    private readonly Dictionary<EventInfo, Delegate?> eventHandlers;

    public EventHandlerCollection()
    {
        eventHandlers = new Dictionary<EventInfo, Delegate?>();
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

    public bool TryGet(EventInfo @event, [NotNullWhen(true)] out Delegate? handlers)
    {
        lock (eventHandlers)
        {
            return eventHandlers.TryGetValue(@event, out handlers) && handlers != null;
        }
    }

    private Delegate? TryGet(EventInfo @event)
    {
        return eventHandlers.TryGetValue(@event, out var handlers) ? handlers : null;
    }
}
