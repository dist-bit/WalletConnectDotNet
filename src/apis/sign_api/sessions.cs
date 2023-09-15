
public class Sessions : GenericStore<SessionData>, ISessions
{
    public Sessions(string context, string version)
        : base(context, version)
    {
    }

    public void Update(string topic, int? expiry = null, Dictionary<string, Namespace>? namespaces = null)
    {
        CheckInitialized();

        var info = Get(topic);
        if (info == null)
        {
            return;
        }

        if (expiry.HasValue)
        {
            info.Expiry = expiry.Value;
        }
        if (namespaces != null)
        {
            info.Namespaces = namespaces;
        }

        Set(topic, info);
    }
}
