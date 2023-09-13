public interface IStore<T>
{
    IDictionary<string, T> Map { get; }
    IList<string> Keys { get; }
    IList<T> Values { get; }
    string StoragePrefix { get; }

    Task InitAsync();
    void Set(string key, object value);
    T? Get<T>(string key) where T : class;

    bool Has(string key);
    List<dynamic> GetAll();
    void UpdateAsync(string key, T value);
    void DeleteAsync(string key);
}