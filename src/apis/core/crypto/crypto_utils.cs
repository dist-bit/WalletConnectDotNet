using System.Text;


/*public class CryptoUtils : ICryptoUtils
{
    private static readonly Random _random = new Random();
    Curve curve = new Curve();

    private const int IV_LENGTH = 12;
    private const int KEY_LENGTH = 32;

    private const int TYPE_LENGTH = 1;

    public byte[] ByteArrayFromHexString(string hex)
    {
        hex = hex.Replace(" ", "");
        int numberChars = hex.Length;
        byte[] bytes = new byte[numberChars / 2];
        for (int i = 0; i < numberChars; i += 2)
        {
            bytes[i / 2] = Convert.ToByte(hex.Substring(i, 2), 16);
        }
        return bytes;
    }

    public CryptoKeyPair GenerateKeyPair()
    {
        var kp = curve.GenerateKeyPair();

        var sk = Utils.transformLongListToByteList(kp.PrivateKey).ToArray();
        var pk = Utils.transformLongListToByteList(kp.PublicKey).ToArray();
     

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

    public async Task<string> DeriveSymKey(string privKeyA, string pubKeyB)
    {
        var sharedKey1 = curve.X25519(
            ByteArrayFromHexString(privKeyA),
            ByteArrayFromHexString(pubKeyB)
        );

        var outBytes = new byte[KEY_LENGTH];

        var hkdf = new Hkdf(HkdfParameters.DefaultHmacAlgorithm);
        hkdf.Init(new KeyParameter(sharedKey1));
        hkdf.GenerateBytes(outBytes, 0, KEY_LENGTH);

        return BitConverter.ToString(outBytes).Replace("-", "");
    }

    public string HashKey(string key)
    {
        var sha256 = new Sha256Digest();
        var input = Encoding.UTF8.GetBytes(key);
        var output = new byte[sha256.GetDigestSize()];
        sha256.BlockUpdate(input, 0, input.Length);
        sha256.DoFinal(output, 0);
        return BitConverter.ToString(output).Replace("-", "");
    }

    public string HashMessage(string message)
    {
        var sha256 = new Sha256Digest();
        var input = Encoding.UTF8.GetBytes(message);
        var output = new byte[sha256.GetDigestSize()];
        sha256.BlockUpdate(input, 0, input.Length);
        sha256.DoFinal(output, 0);
        return BitConverter.ToString(output).Replace("-", "");
    }

    public async Task<string> Encrypt(string message, string symKey, int? type = null, string iv = null, string senderPublicKey = null)
    {
        var decodedType = type ?? EncodeOptions.TYPE_0;

        if (decodedType == EncodeOptions.TYPE_1 && senderPublicKey == null)
        {
            throw new WalletConnectError(-1, "Missing sender public key for type 1 envelope");
        }

        var usedIV = iv != null ? ByteArrayFromHexString(iv) : RandomBytes(IV_LENGTH);

        var chacha = new ChaCha20Poly1305Aead();
        var b = chacha.Encrypt(Encoding.UTF8.GetBytes(message), ByteArrayFromHexString(symKey), usedIV);

        return Serialize(decodedType, b, usedIV, senderPublicKey != null ? ByteArrayFromHexString(senderPublicKey) : null);
    }

    public async Task<string> Decrypt(string symKey, string encoded)
    {
        var chacha = new ChaCha20Poly1305Aead();
        var secretKey = ByteArrayFromHexString(symKey);
        var encodedData = Deserialize(encoded);
        var b = new SecretBox(encodedData.Sealed, secretKey, encodedData.IvSealed, 0);

        return Encoding.UTF8.GetString(chacha.Decrypt(b, secretKey));
    }

    public string Serialize(int type, byte[] sealedBytes, byte[] iv, byte[] senderPublicKey = null)
    {
        var list = new List<byte> { (byte)type };

        if (type == EncodeOptions.TYPE_1)
        {
            if (senderPublicKey == null)
            {
                throw new WalletConnectError(-1, "Missing sender public key for type 1 envelope");
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
                throw new WalletConnectError(-1, "Missing sender public key");
            }
            if (receiverPublicKey == null)
            {
                throw new WalletConnectError(-1, "Missing receiver public key");
            }
        }
        return new EncodingValidation(t, senderPublicKey, receiverPublicKey);
    }

    public bool IsTypeOneEnvelope(EncodingValidation result)
    {
        return result.Type == EncodeOptions.TYPE_1 && result.SenderPublicKey != null && result.ReceiverPublicKey != null;
    }

    private byte[] ByteArrayFromHexString(string hexString)
    {
        if (hexString.Length % 2 != 0)
        {
            throw new ArgumentException("La cadena hex debe tener una longitud par.");
        }
        var result = new byte[hexString.Length / 2];
        for (var i = 0; i < result.Length; i++)
        {
            result[i] = Convert.ToByte(hexString.Substring(i * 2, 2), 16);
        }
        return result;
    }
}
*/