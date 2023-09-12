/*public interface ISignEngineCommon
{
    public abstract Event<SessionConnect> onSessionConnect { get; }
    public abstract Event<SessionDelete> onSessionDelete { get; }
    public abstract Event<SessionExpire> onSessionExpire { get; }
    public abstract Event<SessionPing> onSessionPing { get; }
    public abstract Event<SessionProposalEvent> onProposalExpire { get; }

    public abstract ICore core { get; }
    public abstract PairingMetadata metadata { get; }
    public abstract IGenericStore<ProposalData> proposals { get; }
    public abstract ISessions sessions { get; }
    public abstract IGenericStore<SessionRequest> pendingRequests { get; }

    public abstract Task Init();
    public abstract Task DisconnectSession(string topic, WalletConnectError reason);
    public abstract Dictionary<string, SessionData> GetActiveSessions();
    public abstract Dictionary<string, SessionData> GetSessionsForPairing(string pairingTopic);
    public abstract Dictionary<string, ProposalData> GetPendingSessionProposals();

    public abstract IPairingStore pairings { get; }
} */