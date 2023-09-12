public interface ICryptoUtils
{
    CryptoKeyPair GenerateKeyPair();
    byte[] RandomBytes(int length);
    string GenerateRandomBytes32();
    string DeriveSymKey(string privKeyA, string pubKeyB);
    string HashKey(string key);
    string HashMessage(string message);
    Task<string> Encrypt(
        string message,
        string symKey,
        int? type = null,
        string iv = null,
        string senderPublicKey = null
    );
    Task<string> Decrypt(string symKey, string encoded);
    string Serialize(int type, byte[] sealedBytes, byte[] iv, byte[] senderPublicKey = null);
    EncodingParams Deserialize(string encoded);
    EncodingValidation ValidateDecoding(string encoded, string receiverPublicKey = null);
    EncodingValidation ValidateEncoding(int? type = null, string senderPublicKey = null, string receiverPublicKey = null);
    bool IsTypeOneEnvelope(EncodingValidation result);
}
