public interface ICrypto
{
     string Name { get; }
    // IGenericStore<string> KeyChain { get; }

     Task Init();

     bool HasKeys(string tag);
     Task<string> GetClientId();
     Task<string> GenerateKeyPair();
     Task<string> GenerateSharedKey(
        string selfPublicKey, string peerPublicKey, string? overrideTopic = null);
     Task<string> SetSymKey(
        string symKey, string? overrideTopic = null);
     Task DeleteKeyPair(string publicKey);
     Task DeleteSymKey(string topic);
     Task<string?> Encode(
        string topic, Dictionary<string, dynamic> payload, EncodeOptions options = null);
     Task<string?> Decode(
        string topic, string encoded, DecodeOptions options = null);
     Task<string> SignJWT(string aud);
     int GetPayloadType(string encoded);

     ICryptoUtils GetUtils();
}