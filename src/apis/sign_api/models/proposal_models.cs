using Newtonsoft.Json;

public class RequiredNamespace
{
    public List<string>? chains { get; set; }
    public List<string> methods { get; set; }
    public List<string> events { get; set; }

    public RequiredNamespace(List<string>? chains, List<string> methods, List<string> events)
    {
        this.chains = chains;
        this.methods = methods;
        this.events = events;
    }

    public static RequiredNamespace FromJson(Dictionary<string, dynamic> json)
    {
        List<string>? chains = json.ContainsKey("chains") ? json["chains"] : null;
        List<string> methods = json["methods"];
        List<string> events = json["events"];

        return new RequiredNamespace(chains, methods, events);
    }
}

public class SessionProposal
{
    public int Id { get; set; }

    [JsonProperty("params")]
    public ProposalData Parameters { get; set; }

    // Constructor
    public SessionProposal(int id, ProposalData @params)
    {
        Id = id;
        Parameters = @params;
    }

    public static SessionProposal? FromJson(string json)
    {
         return JsonConvert.DeserializeObject<SessionProposal>(json);
    }
}

public class ProposalData
{
    public int id { get; set; }
    public int expiry { get; set; }
    public List<Relay> relays { get; set; }
    public ConnectionMetadata proposer { get; set; }
    public Dictionary<string, RequiredNamespace> requiredNamespaces { get; set; }
    public Dictionary<string, RequiredNamespace> optionalNamespaces { get; set; }
    public string pairingTopic { get; set; }
    public Dictionary<string, string>? sessionProperties { get; set; }
    public Dictionary<string, Namespace>? generatedNamespaces { get; set; }

    public ProposalData(
        int id,
        int expiry,
        List<Relay> relays,
        ConnectionMetadata proposer,
        Dictionary<string, RequiredNamespace> requiredNamespaces,
        Dictionary<string, RequiredNamespace> optionalNamespaces,
        string pairingTopic,
        Dictionary<string, string>? sessionProperties,
        Dictionary<string, Namespace>? generatedNamespaces)
    {
        this.id = id;
        this.expiry = expiry;
        this.relays = relays;
        this.proposer = proposer;
        this.requiredNamespaces = requiredNamespaces;
        this.optionalNamespaces = optionalNamespaces;
        this.pairingTopic = pairingTopic;
        this.sessionProperties = sessionProperties;
        this.generatedNamespaces = generatedNamespaces;
    }

    public static ProposalData? FromJson(string json)
    {
         return JsonConvert.DeserializeObject<ProposalData>(json);
    }
}