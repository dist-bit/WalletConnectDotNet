public static class MethodConstants
{
    public const string WC_PAIRING_PING = "wc_pairingPing";
    public const string WC_PAIRING_DELETE = "wc_pairingDelete";
    public const string UNREGISTERED_METHOD = "unregistered_method";

    public const string WC_SESSION_PROPOSE = "wc_sessionPropose";
    public const string WC_SESSION_SETTLE = "wc_sessionSettle";
    public const string WC_SESSION_UPDATE = "wc_sessionUpdate";
    public const string WC_SESSION_EXTEND = "wc_sessionExtend";
    public const string WC_SESSION_REQUEST = "wc_sessionRequest";
    public const string WC_SESSION_EVENT = "wc_sessionEvent";
    public const string WC_SESSION_DELETE = "wc_sessionDelete";
    public const string WC_SESSION_PING = "wc_sessionPing";

    public const string WC_AUTH_REQUEST = "wc_authRequest";

    public static readonly Dictionary<string, Dictionary<string, RpcOptions>> RPC_OPTS = new Dictionary<string, Dictionary<string, RpcOptions>>
    {
        {
            WC_PAIRING_PING, new Dictionary<string, RpcOptions>
            {
                {
                    "req", new RpcOptions
                    {
                        Ttl = WalletConnectConstants.ONE_DAY,
                        Prompt = false,
                        Tag = 1000
                    }
                },
                {
                    "res", new RpcOptions
                    {
                        Ttl = WalletConnectConstants.ONE_DAY,
                        Prompt = false,
                        Tag = 1001
                    }
                }
            }
        },
        {
            WC_PAIRING_DELETE, new Dictionary<string, RpcOptions>
            {
                {
                    "req", new RpcOptions
                    {
                        Ttl = WalletConnectConstants.THIRTY_SECONDS,
                        Prompt = false,
                        Tag = 1002
                    }
                },
                {
                    "res", new RpcOptions
                    {
                        Ttl = WalletConnectConstants.THIRTY_SECONDS,
                        Prompt = false,
                        Tag = 1003
                    }
                }
            }
        },
        {
            UNREGISTERED_METHOD, new Dictionary<string, RpcOptions>
            {
                {
                    "req", new RpcOptions
                    {
                        Ttl = WalletConnectConstants.ONE_DAY,
                        Prompt = false,
                        Tag = 0
                    }
                },
                {
                    "res", new RpcOptions
                    {
                        Ttl = WalletConnectConstants.ONE_DAY,
                        Prompt = false,
                        Tag = 0
                    }
                }
            }
        },
        {
            WC_SESSION_PROPOSE, new Dictionary<string, RpcOptions>
            {
                {
                    "req", new RpcOptions
                    {
                        Ttl = WalletConnectConstants.FIVE_MINUTES,
                        Prompt = true,
                        Tag = 1100
                    }
                },
                {
                    "res", new RpcOptions
                    {
                        Ttl = WalletConnectConstants.FIVE_MINUTES,
                        Prompt = false,
                        Tag = 1101
                    }
                }
            }
        },
        {
            WC_SESSION_SETTLE, new Dictionary<string, RpcOptions>
            {
                {
                    "req", new RpcOptions
                    {
                        Ttl = WalletConnectConstants.FIVE_MINUTES,
                        Prompt = false,
                        Tag = 1102
                    }
                },
                {
                    "res", new RpcOptions
                    {
                        Ttl = WalletConnectConstants.FIVE_MINUTES,
                        Prompt = false,
                        Tag = 1103
                    }
                }
            }
        },
        {
            WC_SESSION_UPDATE, new Dictionary<string, RpcOptions>
            {
                {
                    "req", new RpcOptions
                    {
                        Ttl = WalletConnectConstants.ONE_DAY,
                        Prompt = false,
                        Tag = 1104
                    }
                },
                {
                    "res", new RpcOptions
                    {
                        Ttl = WalletConnectConstants.ONE_DAY,
                        Prompt = false,
                        Tag = 1105
                    }
                }
            }
        },
        {
            WC_SESSION_EXTEND, new Dictionary<string, RpcOptions>
            {
                {
                    "req", new RpcOptions
                    {
                        Ttl = WalletConnectConstants.ONE_DAY,
                        Prompt = false,
                        Tag = 1106
                    }
                },
                {
                    "res", new RpcOptions
                    {
                        Ttl = WalletConnectConstants.ONE_DAY,
                        Prompt = false,
                        Tag = 1107
                    }
                }
            }
        },
        {
            WC_SESSION_REQUEST, new Dictionary<string, RpcOptions>
            {
                {
                    "req", new RpcOptions
                    {
                        Ttl = WalletConnectConstants.FIVE_MINUTES,
                        Prompt = true,
                        Tag = 1108
                    }
                },
                {
                    "res", new RpcOptions
                    {
                        Ttl = WalletConnectConstants.FIVE_MINUTES,
                        Prompt = false,
                        Tag = 1109
                    }
                }
            }
        },
        {
            WC_SESSION_EVENT, new Dictionary<string, RpcOptions>
            {
                {
                    "req", new RpcOptions
                    {
                        Ttl = WalletConnectConstants.FIVE_MINUTES,
                        Prompt = true,
                        Tag = 1110
                    }
                },
                {
                    "res", new RpcOptions
                    {
                        Ttl = WalletConnectConstants.FIVE_MINUTES,
                        Prompt = false,
                        Tag = 1111
                    }
                }
            }
        },
        {
            WC_SESSION_DELETE, new Dictionary<string, RpcOptions>
            {
                {
                    "req", new RpcOptions
                    {
                        Ttl = WalletConnectConstants.ONE_DAY,
                        Prompt = false,
                        Tag = 1112
                    }
                },
                {
                    "res", new RpcOptions
                    {
                        Ttl = WalletConnectConstants.ONE_DAY,
                        Prompt = false,
                        Tag = 1113
                    }
                }
            }
        },
        {
            WC_SESSION_PING, new Dictionary<string, RpcOptions>
            {
                {
                    "req", new RpcOptions
                    {
                        Ttl = WalletConnectConstants.THIRTY_SECONDS,
                        Prompt = false,
                        Tag = 1114
                    }
                },
                {
                    "res", new RpcOptions
                    {
                        Ttl = WalletConnectConstants.THIRTY_SECONDS,
                        Prompt = false,
                        Tag = 1115
                    }
                }
            }
        },
        {
            WC_AUTH_REQUEST, new Dictionary<string, RpcOptions>
            {
                {
                    "req", new RpcOptions
                    {
                        Ttl = WalletConnectConstants.ONE_DAY,
                        Prompt = true,
                        Tag = 3000
                    }
                },
                {
                    "res", new RpcOptions
                    {
                        Ttl = WalletConnectConstants.ONE_DAY,
                        Prompt = false,
                        Tag = 3001
                    }
                }
            }
        }
    };
}


