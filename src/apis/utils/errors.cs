public static class Errors
{
    public const string INVALID_METHOD = "INVALID_METHOD";
    public const string INVALID_EVENT = "INVALID_EVENT";
    public const string INVALID_UPDATE_REQUEST = "INVALID_UPDATE_REQUEST";
    public const string INVALID_EXTEND_REQUEST = "INVALID_EXTEND_REQUEST";
    public const string INVALID_SESSION_SETTLE_REQUEST = "INVALID_SESSION_SETTLE_REQUEST";
    public const string UNAUTHORIZED_METHOD = "UNAUTHORIZED_METHOD";
    public const string UNAUTHORIZED_EVENT = "UNAUTHORIZED_EVENT";
    public const string UNAUTHORIZED_UPDATE_REQUEST = "UNAUTHORIZED_UPDATE_REQUEST";
    public const string UNAUTHORIZED_EXTEND_REQUEST = "UNAUTHORIZED_EXTEND_REQUEST";
    public const string USER_REJECTED_SIGN = "USER_REJECTED_SIGN";
    public const string USER_REJECTED = "USER_REJECTED";
    public const string USER_REJECTED_CHAINS = "USER_REJECTED_CHAINS";
    public const string USER_REJECTED_METHODS = "USER_REJECTED_METHODS";
    public const string USER_REJECTED_EVENTS = "USER_REJECTED_EVENTS";
    public const string UNSUPPORTED_CHAINS = "UNSUPPORTED_CHAINS";
    public const string UNSUPPORTED_METHODS = "UNSUPPORTED_METHODS";
    public const string UNSUPPORTED_EVENTS = "UNSUPPORTED_EVENTS";
    public const string UNSUPPORTED_ACCOUNTS = "UNSUPPORTED_ACCOUNTS";
    public const string UNSUPPORTED_NAMESPACE_KEY = "UNSUPPORTED_NAMESPACE_KEY";
    public const string USER_DISCONNECTED = "USER_DISCONNECTED";
    public const string SESSION_SETTLEMENT_FAILED = "SESSION_SETTLEMENT_FAILED";
    public const string NO_SESSION_FOR_TOPIC = "NO_SESSION_FOR_TOPIC";
    public const string REQUEST_EXPIRED_SESSION = "SESSION_REQUEST_EXPIRED";
    public const string WC_METHOD_UNSUPPORTED = "WC_METHOD_UNSUPPORTED";

    // AUTH
    public const string MALFORMED_RESPONSE_PARAMS = "MALFORMED_RESPONSE_PARAMS";
    public const string MALFORMED_REQUEST_PARAMS = "MALFORMED_REQUEST_PARAMS";
    public const string MESSAGE_COMPROMISED = "MESSAGE_COMPROMISED";
    public const string SIGNATURE_VERIFICATION_FAILED = "SIGNATURE_VERIFICATION_FAILED";
    public const string REQUEST_EXPIRED_AUTH = "AUTH_REQUEST_EXPIRED";
    public const string MISSING_ISSUER_AUTH = "AUTH_MISSING_ISSUER";
    public const string USER_REJECTED_AUTH = "AUTH_USER_REJECTED";
    public const string USER_DISCONNECTED_AUTH = "AUTH_USER_DISCONNECTED";

    public static readonly Dictionary<string, Dictionary<string, object>> SDK_ERRORS = new Dictionary<string, Dictionary<string, object>>
    {
        ["INVALID_METHOD"] = new Dictionary<string, object>
        {
            ["message"] = "Invalid method.",
            ["code"] = 1001
        },
        ["INVALID_EVENT"] = new Dictionary<string, object>
        {
            ["message"] = "Invalid event.",
            ["code"] = 1002
        },
        // Resto de las entradas...
    };

    public const string NOT_INITIALIZED = "NOT_INITIALIZED";
    public const string NO_MATCHING_KEY = "NO_MATCHING_KEY";
    public const string RESTORE_WILL_OVERRIDE = "RESTORE_WILL_OVERRIDE";
    public const string RESUBSCRIBED = "RESUBSCRIBED";
    public const string MISSING_OR_INVALID = "MISSING_OR_INVALID";
    public const string EXPIRED = "EXPIRED";
    public const string UNKNOWN_TYPE = "UNKNOWN_TYPE";
    public const string MISMATCHED_TOPIC = "MISMATCHED_TOPIC";
    public const string NON_CONFORMING_NAMESPACES = "NON_CONFORMING_NAMESPACES";

    public static readonly Dictionary<string, Dictionary<string, object>> INTERNAL_ERRORS = new Dictionary<string, Dictionary<string, object>>
    {
        ["NOT_INITIALIZED"] = new Dictionary<string, object>
        {
            ["message"] = "Not initialized.",
            ["code"] = 1
        },
        ["NO_MATCHING_KEY"] = new Dictionary<string, object>
        {
            ["message"] = "No matching key.",
            ["code"] = 2
        },
        // Resto de las entradas...
    };

    public static WalletConnectError GetInternalError(string key, string context = "")
    {
        if (INTERNAL_ERRORS.ContainsKey(key))
        {
            return new WalletConnectError
            {
                Code = (int)INTERNAL_ERRORS[key]["code"],
                Message = !string.IsNullOrEmpty(context) ? $"{INTERNAL_ERRORS[key]["message"]} {context}" : (string)INTERNAL_ERRORS[key]["message"]
            };
        }
        return new WalletConnectError { Code = -1, Message = "UNKNOWN INTERNAL ERROR" };
    }

    public static WalletConnectError GetSdkError(string key, string context = "")
    {
        if (SDK_ERRORS.ContainsKey(key))
        {
            return new WalletConnectError
            {
                Code = (int)SDK_ERRORS[key]["code"],
                Message = !string.IsNullOrEmpty(context) ? $"{SDK_ERRORS[key]["message"]} {context}" : (string)SDK_ERRORS[key]["message"]
            };
        }
        return new WalletConnectError { Code = -1, Message = "UNKNOWN SDK ERROR" };
    }
}

public static class WebSocketErrors
{
    public const int PROJECT_ID_NOT_FOUND = 401;
    public const int INVALID_PROJECT_ID = 403;
    public const int TOO_MANY_REQUESTS = 1013;
    public const string INVALID_PROJECT_ID_OR_JWT = "Invalid project ID or JWT Token";
    public const string INVALID_PROJECT_ID_MESSAGE = "Invalid project id. Please check your project id.";
    public const string PROJECT_ID_NOT_FOUND_MESSAGE = "Project id not found.";
    public const string TOO_MANY_REQUESTS_MESSAGE = "Too many requests. Please try again later.";

    public const int SERVER_TERMINATING = 1001;
    public const int CLIENT_STALE = 4008;
    public const int LOAD_REBALANCING = 4010;
}