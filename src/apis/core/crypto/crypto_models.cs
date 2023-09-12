public class CryptoKeyPair
{
    public string PrivateKey { get; }
    public string PublicKey { get; }

    public CryptoKeyPair(string privateKey, string publicKey)
    {
        PrivateKey = privateKey;
        PublicKey = publicKey;
    }

    public byte[] GetPrivateKeyBytes()
    {
        return ByteArrayFromHexString(PrivateKey);
    }

    public byte[] GetPublicKeyBytes()
    {
        return ByteArrayFromHexString(PublicKey);
    }

    private static byte[] ByteArrayFromHexString(string hexString)
    {
        if (hexString.Length % 2 != 0)
            throw new ArgumentException("La cadena hex debe tener una longitud par.");
        byte[] result = new byte[hexString.Length / 2];
        for (int i = 0; i < result.Length; i++)
        {
            result[i] = Convert.ToByte(hexString.Substring(i * 2, 2), 16);
        }
        return result;
    }
}

public class EncryptParams
{
    public string Message { get; }
    public string SymKey { get; }
    public int? Type { get; }
    public string? Iv { get; }
    public string? SenderPublicKey { get; }

    public EncryptParams(string message, string symKey, int? type = null, string iv = null, string senderPublicKey = null)
    {
        Message = message;
        SymKey = symKey;
        Type = type;
        Iv = iv;
        SenderPublicKey = senderPublicKey;
    }
}

public class EncodingParams
{
    public int Type { get; }
    public byte[] Sealed { get; }
    public byte[] Iv { get; }
    public byte[] IvSealed { get; }
    public byte[]? SenderPublicKey { get; }

    public EncodingParams(int type, byte[] sealedBytes, byte[] iv, byte[] ivSealed, byte[]? senderPublicKey = null)
    {
        Type = type;
        Sealed = sealedBytes;
        Iv = iv;
        IvSealed = ivSealed;
        SenderPublicKey = senderPublicKey;
    }
}

public class EncodingValidation
{
    public int Type { get; }
    public string? SenderPublicKey { get; }
    public string? ReceiverPublicKey { get; }

    public EncodingValidation(int type, string? senderPublicKey = null, string? receiverPublicKey = null)
    {
        Type = type;
        SenderPublicKey = senderPublicKey;
        ReceiverPublicKey = receiverPublicKey;
    }
}

public class EncodeOptions
{
    public const int TYPE_0 = 0;
    public const int TYPE_1 = 1;

    public int? Type { get; }
    public string? SenderPublicKey { get; }
    public string? ReceiverPublicKey { get; }

    public EncodeOptions(int? type = null, string? senderPublicKey = null, string? receiverPublicKey = null)
    {
        Type = type;
        SenderPublicKey = senderPublicKey;
        ReceiverPublicKey = receiverPublicKey;
    }
}

public class DecodeOptions
{
    public string? ReceiverPublicKey { get; }

    public DecodeOptions(string? receiverPublicKey = null)
    {
        ReceiverPublicKey = receiverPublicKey;
    }
}