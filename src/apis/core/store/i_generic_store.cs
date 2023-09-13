public abstract class IGenericStore<T>
{
    public abstract string Version { get; }
    public abstract string Context { get; }

   // public abstract IStore<T> Storage { get; }
    public abstract string StorageKey { get; }

    //public abstract Func<dynamic, T> FromJson { get; }

    public abstract Event<StoreCreateEvent<object>> OnCreate { get; }
    public abstract Event<StoreUpdateEvent<object>> OnUpdate { get;}
    public abstract Event<StoreDeleteEvent<object>> OnDelete { get;}
    public abstract Event<StoreSyncEvent> OnSync { get;}

    public abstract void Init();
    public abstract bool Has(string key);
    public abstract void Set(string key, object value);
    public abstract T? Get(string key);
    public abstract List<object> GetAll();
    public abstract void Delete(string key);
    public abstract void Restore();

    public abstract void CheckInitialized();
}