public class WCEncryptionPayload
{
    public string Data { get; }
    public string Hmac { get; }
    public string Iv { get; }

    public WCEncryptionPayload(string data, string hmac, string iv)
    {
        Data = data;
        Hmac = hmac;
        Iv = iv;
    }
}