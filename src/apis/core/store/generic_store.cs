using Newtonsoft.Json;

public class GenericStore<T> : IGenericStore<T>
{
    public string Context { get; }
    public string Version { get; }

    public string StorageKey => $"{Version}//{Context}";

    public Func<dynamic, object> FromJson => throw new NotImplementedException();

    public Event<StoreCreateEvent<object>> OnCreate { get; }
    public Event<StoreUpdateEvent<object>> OnUpdate { get; }
    public Event<StoreDeleteEvent<object>> OnDelete { get; }
    public Event<StoreSyncEvent> OnSync { get; }


    private bool _initialized = false;

    private readonly Dictionary<string, object> data = new Dictionary<string, object>();

    //private readonly Func<dynamic, object> fromJson;

    public GenericStore(/*IStore<T> storage, */string context, string version)
    {
        //Storage = storage ?? throw new ArgumentNullException(nameof(storage));
        Context = context ?? throw new ArgumentNullException(nameof(context));
        Version = version ?? throw new ArgumentNullException(nameof(version));
        //this.fromJson = fromJson ?? throw new ArgumentNullException(nameof(fromJson));

        OnCreate = new Event<StoreCreateEvent<object>>();
        OnUpdate = new Event<StoreUpdateEvent<object>>();
        OnDelete = new Event<StoreDeleteEvent<object>>();
        OnSync = new Event<StoreSyncEvent>();
    }

    public void Init()
    {
        if (_initialized)
        {
            return;
        }

        _initialized = true;

        Restore();
    }

    public bool Has(string key)
    {
        CheckInitialized();
        return data.ContainsKey(key);
    }

    public T? Get(string key)
    {
        CheckInitialized();

        if (data.ContainsKey(key))
        {
            return JsonConvert.DeserializeObject<T>(JsonConvert.SerializeObject(data[key]));
        }
        return default;
    }

    public List<object> GetAll()
    {
        return new List<object>(data.Values);
    }

    public void Set(string key, object value)
    {
        CheckInitialized();

        if (data.ContainsKey(key))
        {
            OnUpdate.Broadcast(new StoreUpdateEvent<object>(key, value));

        }
        else
        {
            OnCreate.Broadcast(new StoreCreateEvent<object>(key, value));
        }

        data[key] = value;
        //OnSync.Broadcast(new StoreSyncEvent());
    }

    public void Delete(string key)
    {
        CheckInitialized();

        if (!data.ContainsKey(key))
        {
            return;
        }

        OnDelete.Broadcast(new StoreDeleteEvent<object>(key, data[key]));
        data.Remove(key);
    }

    public void Restore()
    {

        if (!Has(Context))
        {
            Set(Context, Version);
            Set(StorageKey, new Dictionary<string, object>());
            return;
        }


        var stored = Get(Context) as Dictionary<string, string>;
        string storedVersion = stored!["version"];
        if (storedVersion != Version)
        {
            Delete($"{storedVersion}//{Context}");
            var config = new Dictionary<string, string> { { "version", Version } };
            string configJson = JsonConvert.SerializeObject(config);
            Set(Context, configJson);
            Set(StorageKey, new Dictionary<string, dynamic>());
            return;
        }

        if (Has(StorageKey))
        {
            Delete(storedVersion);
        }
    }

    protected void CheckInitialized()
    {
        if (!_initialized)
        {
            throw new Exception("NOT_INITIALIZED");
        }
    }

}
