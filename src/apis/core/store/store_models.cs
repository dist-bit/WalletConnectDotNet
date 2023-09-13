public class StoreCreateEvent<T> : EventArgs
{
    public string Key { get; }
    public T Value { get; }

    public StoreCreateEvent(string key, T value)
    {
        Key = key;
        Value = value;
    }
}

public class StoreUpdateEvent<T> : EventArgs
{
    public string Key { get; }
    public T Value { get; }

    public StoreUpdateEvent(string key, T value)
    {
        Key = key;
        Value = value;
    }
}

public class StoreDeleteEvent<T> : EventArgs
{
    public string Key { get; }
    public T Value { get; }

    public StoreDeleteEvent(string key, T value)
    {
        Key = key;
        Value = value;
    }
}

public class StoreSyncEvent : EventArgs
{
    public StoreSyncEvent()
    {
    }
}