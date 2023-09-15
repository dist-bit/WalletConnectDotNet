public interface ISessions
{
    void Update(string topic, int? expiry = null, Dictionary<string, Namespace>? namespaces = null);
}