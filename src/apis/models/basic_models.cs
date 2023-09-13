using Newtonsoft.Json;

public class WalletConnectError : Exception
{
    public int Code { get; }
    public string Message { get; }
    public string? Data { get; }

    public WalletConnectError(int code, string message, string? data)
    {
        Code = code;
        Message = message;
        Data = data;
    }

    public WalletConnectError(int code, string message)
    {
        Code = code;
        Message = message;
    }


    public static WalletConnectError FromJson(Dictionary<string, dynamic> json)
    {
        int code = json["code"];
        string message = json["message"];
        string? data = json.ContainsKey("data") ? json["data"] : null;

        return new WalletConnectError(code, message, data);
    }
}

public class RpcOptions
{
    public int Ttl { get; }
    public bool Prompt { get; }
    public int Tag { get; }

    public RpcOptions(int ttl, bool prompt, int tag)
    {
        Ttl = ttl;
        Prompt = prompt;
        Tag = tag;
    }
}

public class ConnectionMetadata
{
    public string PublicKey { get; }
    public PairingMetadata Metadata { get; }

    public ConnectionMetadata(string publicKey, PairingMetadata metadata)
    {
        PublicKey = publicKey;
        Metadata = metadata;
    }

     public static ConnectionMetadata? FromJson(string json)
    {
        return JsonConvert.DeserializeObject<ConnectionMetadata>(json);
    }
}