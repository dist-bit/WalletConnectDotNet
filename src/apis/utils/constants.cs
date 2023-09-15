public static class WalletConnectConstants
{
    public const string SDK_VERSION = "2.1.4";

    public const string CORE_PROTOCOL = "wc";
    public const int CORE_VERSION = 2;
    public const string CORE_CONTEXT = "core";

    public const string DEFAULT_RELAY_URL = "wss://relay.walletconnect.com";
    public const string FALLBACK_RELAY_URL = "wss://relay.walletconnect.org";

    public static string CORE_STORAGE_PREFIX = $"{CORE_PROTOCOL}@{CORE_VERSION}:{CORE_CONTEXT}";

    public const int THIRTY_SECONDS = 30;
    public const int ONE_MINUTE = 60;
    public const int FIVE_MINUTES = ONE_MINUTE * 5;
    public const int ONE_HOUR = ONE_MINUTE * 60;
    public const int ONE_DAY = ONE_MINUTE * 24 * 60;
    public const int SEVEN_DAYS = ONE_DAY * 7;
    public const int THIRTY_DAYS = ONE_DAY * 30;

    public const string RELAYER_DEFAULT_PROTOCOL = "irn";

    public const string DEFAULT_PUSH_URL = "https://echo.walletconnect.com";
}

public static class StoreVersions
{
    // Core
    public const string CONTEXT_KEYCHAIN = "keychain";
    public const string VERSION_KEYCHAIN = "0.3";
    public const string CONTEXT_JSON_RPC_HISTORY = "jsonRpcHistory";
    public const string VERSION_JSON_RPC_HISTORY = "1.0";
    public const string CONTEXT_PAIRINGS = "pairings";
    public const string VERSION_PAIRINGS = "1.0";
    public const string CONTEXT_EXPIRER = "expirer";
    public const string VERSION_EXPIRER = "0.3";
    public const string CONTEXT_MESSAGE_TRACKER = "messageTracker";
    public const string VERSION_MESSAGE_TRACKER = "1.0";
    public const string CONTEXT_TOPIC_MAP = "topicMap";
    public const string VERSION_TOPIC_MAP = "1.0";
    public const string CONTEXT_TOPIC_TO_RECEIVER_PUBLIC_KEY = "topicToReceiverPublicKey";
    public const string VERSION_TOPIC_TO_RECEIVER_PUBLIC_KEY = "1.1";

    // Sign
    public const string CONTEXT_PROPOSALS = "proposals";
    public const string VERSION_PROPOSALS = "1.1";
    public const string CONTEXT_SESSIONS = "sessions";
    public const string VERSION_SESSIONS = "1.1";
    public const string CONTEXT_PENDING_REQUESTS = "pendingRequests";
    public const string VERSION_PENDING_REQUESTS = "1.0";

    // Auth
    public const string CONTEXT_AUTH_KEYS = "authKeys";
    public const string VERSION_AUTH_KEYS = "2.0";
    public const string CONTEXT_PAIRING_TOPICS = "authPairingTopics";
    public const string VERSION_PAIRING_TOPICS = "2.0";
    public const string CONTEXT_AUTH_REQUESTS = "authRequests";
    public const string VERSION_AUTH_REQUESTS = "2.0";
    public const string CONTEXT_COMPLETE_REQUESTS = "completeRequests";
    public const string VERSION_COMPLETE_REQUESTS = "2.1";
}
