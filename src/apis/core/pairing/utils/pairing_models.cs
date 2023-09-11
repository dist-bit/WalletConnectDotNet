using Newtonsoft.Json;

public enum ProtocolType
{
    Pair,
    Sign,
    Auth
}

public class PairingInfo
{
    [JsonProperty("topic")]
    public string Topic { get; set; }

    [JsonProperty("expiry")]
    public int Expiry { get; set; }

    [JsonProperty("relay")]
    public Relay Relay { get; set; }

    [JsonProperty("active")]
    public bool Active { get; set; }

    [JsonProperty("peerMetadata")]
    public PairingMetadata? PeerMetadata { get; set; }

    public static PairingInfo? FromJson(string json)
    {
        return JsonConvert.DeserializeObject<PairingInfo>(json);
    }
}


public class PairingMetadata
{
    public string Name { get; }
    public string Description { get; }
    public string Url { get; }
    public List<string> Icons { get; }
    public Redirect? Redirect { get; }

    public PairingMetadata(
        string name,
        string description,
        string url,
        List<string> icons,
        Redirect? redirect)
    {
        Name = name;
        Description = description;
        Url = url;
        Icons = icons;
        Redirect = redirect;
    }

    public static PairingMetadata? FromJson(string json)
    {
        return JsonConvert.DeserializeObject<PairingMetadata>(json);
    }

    public static PairingMetadata Empty() => new PairingMetadata(
        name: string.Empty,
        description: string.Empty,
        url: string.Empty,
        redirect: null,
        icons: new List<string>());
}

public class Redirect
{
    public string? Native { get; set; }
    public string? Universal { get; set; }

    public static Redirect? FromJson(string json)
    {
        return JsonConvert.DeserializeObject<Redirect>(json);
    }
}

public class CreateResponse
{
    public string Topic { get; set; }
    public Uri Uri { get; set; }
    public PairingInfo PairingInfo { get; set; }

    public CreateResponse(string topic, Uri uri, PairingInfo pairingInfo)
    {
        Topic = topic;
        Uri = uri;
        PairingInfo = pairingInfo;
    }

    public override string ToString()
    {
        return $"CreateResponse(topic: {Topic}, uri: {Uri})";
    }
}

public class ExpirationEvent: EventArgs
{
    public string Target { get; set; }
    public int Expiry { get; set; }

    public ExpirationEvent(string target, int expiry)
    {
        Target = target;
        Expiry = expiry;
    }

    public override string ToString()
    {
        return $"ExpirationEvent(target: {Target}, expiry: {Expiry})";
    }
}

public class HistoryEvent: EventArgs
{
    public JsonRpcRecord Record { get; set; }

    public HistoryEvent(JsonRpcRecord record)
    {
        Record = record;
    }

    public override string ToString()
    {
        return $"HistoryEvent(record: {Record})";
    }
}

public class PairingInvalidEvent: EventArgs
{
    public string Message { get; set; }

    public PairingInvalidEvent(string message)
    {
        Message = message;
    }

    public override string ToString()
    {
        return $"PairingInvalidEvent(message: {Message})";
    }
}

public class PairingEvent: EventArgs
{
    public int? Id { get; set; }
    public string? Topic { get; set; }
    public JsonRpcError? Error { get; set; }

    public PairingEvent(int? id, string? topic, JsonRpcError? error)
    {
        Id = id;
        Topic = topic;
        Error = error;
    }

    public override string ToString()
    {
        return $"PairingEvent(id: {Id}, topic: {Topic}, error: {Error})";
    }
}

public class PairingActivateEvent: EventArgs
{
    public string Topic { get; set; }
    public int Expiry { get; set; }

    public PairingActivateEvent(string topic, int expiry)
    {
        Topic = topic;
        Expiry = expiry;
    }

    public override string ToString()
    {
        return $"PairingActivateEvent(topic: {Topic}, expiry: {Expiry})";
    }
}

public class JsonRpcRecord
{
    public int Id { get; set; }
    public string Topic { get; set; }
    public string Method { get; set; }
    public dynamic Params { get; set; }
    public string? ChainId { get; set; }
    public int? Expiry { get; set; }
    public dynamic Response { get; set; }

    public static JsonRpcRecord? FromJson(string json)
    {
        return JsonConvert.DeserializeObject<JsonRpcRecord>(json);
    }

    public override string ToString()
    {
        return $"JsonRpcRecord(id: {Id}, topic: {Topic}, method: {Method}, params: {Params}, chainId: {ChainId}, expiry: {Expiry}, response: {Response})";
    }
}

public class ReceiverPublicKey
{
    public string Topic { get; set; }
    public string PublicKey { get; set; }
    public int Expiry { get; set; }

    public static ReceiverPublicKey? FromJson(string json)
    {
        return JsonConvert.DeserializeObject<ReceiverPublicKey>(json);
    }

    public override string ToString()
    {
        return $"ReceiverPublicKey(topic: {Topic}, publicKey: {PublicKey}, expiry: {Expiry})";
    }
}

public class RegisteredFunction
{
    public string Method { get; set; }
    public Action<string, JsonRpcRequest> Function { get; set; }
    public ProtocolType Type { get; set; }

    public RegisteredFunction(string method, Action<string, JsonRpcRequest> function, ProtocolType type)
    {
        Method = method;
        Function = function;
        Type = type;
    }

    public override string ToString()
    {
        return $"RegisteredFunction(Method: {Method}, Type: {Type})";
    }
}