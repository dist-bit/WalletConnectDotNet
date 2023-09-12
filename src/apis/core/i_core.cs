/*public abstract class ICore
{
    public string Protocol { get; } = "wc";
    public string Version { get; } = "2";

    public abstract string RelayUrl { get; }
    public abstract string ProjectId { get; }
    public abstract string PushUrl { get; }

    // public abstract IHeartBeat Heartbeat { get; }
    public abstract ICrypto Crypto { get; }
    public abstract IRelayClient RelayClient { get; }
    public abstract IStore<Dictionary<string, dynamic>> Storage { get; }
    // public abstract IJsonRpcHistory History { get; }
    public abstract IExpirer Expirer { get; }
    public abstract IPairing Pairing { get; }
    public abstract IEcho Echo { get; }
    public abstract Logger Logger { get; }

    public abstract Task Start();
} */