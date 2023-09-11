using Newtonsoft.Json;

public class SessionProposalCompleter
{
    public int Id { get; }
    public string SelfPublicKey { get; }
    public string PairingTopic { get; }
    public Dictionary<string, RequiredNamespace> RequiredNamespaces { get; }
    public Dictionary<string, RequiredNamespace> OptionalNamespaces { get; }
    public Dictionary<string, string>? SessionProperties { get; }
    public TaskCompletionSource Completer { get; }

    public SessionProposalCompleter(
        int id,
        string selfPublicKey,
        string pairingTopic,
        Dictionary<string, RequiredNamespace> requiredNamespaces,
        Dictionary<string, RequiredNamespace> optionalNamespaces,
        TaskCompletionSource completer,
        Dictionary<string, string>? sessionProperties = null)
    {
        Id = id;
        SelfPublicKey = selfPublicKey;
        PairingTopic = pairingTopic;
        RequiredNamespaces = requiredNamespaces;
        OptionalNamespaces = optionalNamespaces;
        SessionProperties = sessionProperties;
        Completer = completer;
    }

    public override string ToString()
    {
        return $"SessionProposalCompleter(Id: {Id}, SelfPublicKey: {SelfPublicKey}, PairingTopic: {PairingTopic}, RequiredNamespaces: {RequiredNamespaces}, OptionalNamespaces: {OptionalNamespaces}, SessionProperties: {SessionProperties}, Completer: {Completer})";
    }
}

public class Namespace
{
    [JsonProperty("accounts")]
    public List<string> Accounts { get; set; }

    [JsonProperty("methods")]
    public List<string> Methods { get; set; }

    [JsonProperty("events")]
    public List<string> Events { get; set; }

    public static Namespace? FromJson(string jsonString)
    {
         return JsonConvert.DeserializeObject<Namespace>(jsonString);
    }
}

public class SessionData
{
    [JsonProperty("topic")]
    public string Topic { get; set; }

    [JsonProperty("pairingTopic")]
    public string PairingTopic { get; set; }

    [JsonProperty("relay")]
    public Relay Relay { get; set; }

    [JsonProperty("expiry")]
    public int Expiry { get; set; }

    [JsonProperty("acknowledged")]
    public bool Acknowledged { get; set; }

    [JsonProperty("controller")]
    public string Controller { get; set; }

    [JsonProperty("namespaces")]
    public Dictionary<string, Namespace> Namespaces { get; set; }

    [JsonProperty("requiredNamespaces")]
    public Dictionary<string, RequiredNamespace>? RequiredNamespaces { get; set; }

    [JsonProperty("optionalNamespaces")]
    public Dictionary<string, RequiredNamespace>? OptionalNamespaces { get; set; }

    [JsonProperty("sessionProperties")]
    public Dictionary<string, string>? SessionProperties { get; set; }

    [JsonProperty("self")]
    public ConnectionMetadata Self { get; set; }

    [JsonProperty("peer")]
    public ConnectionMetadata Peer { get; set; }

    public static SessionData? FromJson(string json)
    {
       return JsonConvert.DeserializeObject<SessionData>(json);
    }
}


public class SessionRequest
{
    [JsonProperty("id")]
    public int Id { get; set; }

    [JsonProperty("topic")]
    public string Topic { get; set; }

    [JsonProperty("method")]
    public string Method { get; set; }

    [JsonProperty("chainId")]
    public string ChainId { get; set; }

    [JsonProperty("params")]
    public dynamic Params { get; set; }

    public static SessionRequest? FromJson(string jsonString)
    {
        return JsonConvert.DeserializeObject<SessionRequest>(jsonString);
    }
}