using Newtonsoft.Json;

public class Relay
{
    [JsonProperty("protocol")]
    public string Protocol { get; set; }

    [JsonProperty("data")]
    public string? Data { get; set; }

    public static Relay? FromJson(string json)
    {
        return JsonConvert.DeserializeObject<Relay>(json);
    }

    public string ToJson()
    {
        return JsonConvert.SerializeObject(this);
    }
}


public class MessageEvent : EventArgs
{
    public string Topic { get; }
    public string Message { get; }

    public MessageEvent(string topic, string message)
    {
        Topic = topic;
        Message = message;
    }
}

public class ErrorEvent : EventArgs
{
    public dynamic Error { get; }

    public ErrorEvent(dynamic error)
    {
        Error = error;
    }
}


public class SubscriptionEvent : EventArgs
{
    public string Id { get; }

    public SubscriptionEvent(string id)
    {
        Id = id;
    }
}

public class SubscriptionDeletionEvent : SubscriptionEvent
{
    public string Reason { get; }

    public SubscriptionDeletionEvent(string id, string reason)
        : base(id)
    {
        Reason = reason;
    }
}
