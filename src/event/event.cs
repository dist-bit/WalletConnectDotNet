public delegate void EventHandler<T>(T args) where T : EventArgs;

public class Event<T> where T : EventArgs
{
    private List<EventHandler<T>> _handlers = new List<EventHandler<T>>();

    public void Subscribe(EventHandler<T> handler)
    {
        _handlers.Add(handler);
    }

    public void Add(EventHandler<T> handler)
    {
        Subscribe(handler);
    }

    public void SubscribeStream(Action<T> action)
    {
        _handlers.Add(args => action(args));
    }

    public void Unsubscribe(EventHandler<T> handler)
    {
        _handlers.Remove(handler);
    }

    public void Remove(EventHandler<T> handler)
    {
        Unsubscribe(handler);
    }

    public void UnsubscribeAll()
    {
        _handlers.Clear();
    }

    public int SubscriberCount
    {
        get { return _handlers.Count; }
    }

    public void Broadcast(T args)
    {
        foreach (var handler in _handlers)
        {
            handler(args);
        }
    }
}