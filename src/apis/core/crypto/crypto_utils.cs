public class CryptoUtils : ICryptoUtils
{
    private static readonly Random _random = new Random();
    Curve curve = new Curve();

    private const int IV_LENGTH = 12;
    private const int KEY_LENGTH = 32;

    private const int TYPE_LENGTH = 1;

    public CryptoKeyPair GenerateKeyPair()
    {
        var kp = curve.GenerateKeyPair();
        var sk = Utils.topByteArray(kp.PrivateKey);
        var pk = Utils.topByteArray(kp.PublicKey);
        return new CryptoKeyPair(BitConverter.ToString(sk).Replace("-", ""), BitConverter.ToString(pk).Replace("-", ""));
    }

    public byte[] RandomBytes(int length)
    {
        var random = new byte[length];
        _random.NextBytes(random);
        return random;
    }

    public string GenerateRandomBytes32()
    {
        return BitConverter.ToString(RandomBytes(32)).Replace("-", "");
    }

    public string DeriveSymKey(string privKeyA, string pubKeyB)
    {
        var a = Utils.ByteArrayFromHexString(privKeyA);
        var b = Utils.ByteArrayFromHexString(pubKeyB);
        var sharedKey1 = curve.X25519(Utils.ToLongList(a), Utils.ToLongList(b));
        var ikm = Utils.topByteArray(sharedKey1);
        var salt = new byte[0];
        var info = new byte[0];

        Hkdf hkdf = new Hkdf();
        var derivation = hkdf.DeriveKey(salt, ikm, info, KEY_LENGTH);

        return BitConverter.ToString(derivation).Replace("-", "").ToLower();
    }

    public string HashKey(string key)
    {
        return Utils.CalculateSHA256Hash(key);
    }

    public string HashMessage(string message)
    {
        return Utils.CalculateSHA256Hash(message);
    }

    public string Encrypt(string message, string symKey, int? type = null, string? iv = null, string? senderPublicKey = null)
    {
        var decodedType = type ?? EncodeOptions.TYPE_0;

        if (decodedType == EncodeOptions.TYPE_1 && senderPublicKey == null)
        {
            throw new WalletConnectError
            {
                Code = -1,
                Message = "Missing sender public key for type 1 envelope"
            };
        }

        var usedIV = iv != null ? Utils.ByteArrayFromHexString(iv) : RandomBytes(IV_LENGTH);
        var b = Utils.ChaCha20Poly1305EncryptMessage(Utils.ByteArrayFromHexString(symKey), usedIV, message);

        return Serialize(decodedType, b, usedIV, senderPublicKey != null ? Utils.ByteArrayFromHexString(senderPublicKey) : null);
    }

    public string Decrypt(string symKey, string encoded)
    {
        var secretKey = Utils.ByteArrayFromHexString(symKey);
        var encodedData = Deserialize(encoded);
        return Utils.ChaCha20Poly1305DecryptMessage(secretKey, encodedData.Sealed);
    }

    public string Serialize(int type, byte[] sealedBytes, byte[] iv, byte[]? senderPublicKey = null)
    {
        var list = new List<byte> { (byte)type };

        if (type == EncodeOptions.TYPE_1)
        {
            if (senderPublicKey == null)
            {
                throw new WalletConnectError {
                    Code = -1, Message = "Missing sender public key for type 1 envelope"
                };
            }

            list.AddRange(senderPublicKey);
        }

        list.AddRange(iv);
        list.AddRange(sealedBytes);

        return Convert.ToBase64String(list.ToArray());
    }

    public EncodingParams Deserialize(string encoded)
    {
        var bytes = Convert.FromBase64String(encoded);
        var type = bytes[0];

        var index = TYPE_LENGTH;
        byte[] senderPublicKey = null;
        if (type == EncodeOptions.TYPE_1)
        {
            senderPublicKey = bytes.Skip(index).Take(KEY_LENGTH).ToArray();
            index += KEY_LENGTH;
        }
        var iv = bytes.Skip(index).Take(IV_LENGTH).ToArray();
        var ivSealed = bytes.Skip(index).ToArray();
        index += IV_LENGTH;
        var sealedBytes = bytes.Skip(index).ToArray();

        return new EncodingParams(type, sealedBytes, iv, ivSealed, senderPublicKey);
    }

    public EncodingValidation ValidateDecoding(string encoded, string receiverPublicKey = null)
    {
        var deserialized = Deserialize(encoded);
        var senderPublicKey = deserialized.SenderPublicKey != null ? BitConverter.ToString(deserialized.SenderPublicKey).Replace("-", "") : null;
        return ValidateEncoding(deserialized.Type, senderPublicKey, receiverPublicKey);
    }

    public EncodingValidation ValidateEncoding(int? type, string senderPublicKey = null, string receiverPublicKey = null)
    {
        var t = type ?? EncodeOptions.TYPE_0;
        if (t == EncodeOptions.TYPE_1)
        {
            if (senderPublicKey == null)
            {
                throw new WalletConnectError {
                    Code =-1, Message = "Missing sender public key"
                };
            }
            if (receiverPublicKey == null)
            {
                throw new WalletConnectError {
                    Code = -1, Message = "Missing receiver public key"
                };
            }
        }
        return new EncodingValidation(t, senderPublicKey, receiverPublicKey);
    }

    public bool IsTypeOneEnvelope(EncodingValidation result)
    {
        return result.Type == EncodeOptions.TYPE_1 && result.SenderPublicKey != null && result.ReceiverPublicKey != null;
    }
}
