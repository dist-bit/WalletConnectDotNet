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
