public abstract class ICrypto
{
    public abstract string Name { get; }
    //public abstract IGenericStore<string> KeyChain { get; }

    public abstract Task Init();

    public abstract bool HasKeys(string tag);
    public abstract Task<string> GetClientId();
    public abstract Task<string> GenerateKeyPair();
    public abstract Task<string> GenerateSharedKey(
        string selfPublicKey, string peerPublicKey, string? overrideTopic = null);
    public abstract Task<string> SetSymKey(
        string symKey, string? overrideTopic = null);
    public abstract Task DeleteKeyPair(string publicKey);
    public abstract Task DeleteSymKey(string topic);
    public abstract Task<string?> Encode(
        string topic, Dictionary<string, dynamic> payload, EncodeOptions options = null);
    public abstract Task<string?> Decode(
        string topic, string encoded, DecodeOptions options = null);
    public abstract Task<string> SignJWT(string aud);
    public abstract int GetPayloadType(string encoded);

    public abstract ICryptoUtils GetUtils();
}