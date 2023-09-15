using Newtonsoft.Json;

public class WalletConnectError : Exception
{
    public int Code { get; set; }
    public string Message { get; set; }
    public string? Data { get; set; }

    public static WalletConnectError FromJson(Dictionary<string, dynamic> json)
    {
        int code = json["code"];
        string message = json["message"];
        string? data = json.ContainsKey("data") ? json["data"] : null;

        return new WalletConnectError  {
            Code = code, Message = message, Data = data
        };
    }
}

public class RpcOptions
{
    public int Ttl { get; set; }
    public bool Prompt { get; set; }
    public int Tag { get; set; }

 
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