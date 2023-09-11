using System.Security.Cryptography;


public static class WCCipher
{
    private static readonly string CIPHER_ALGORITHM = "AES/CBC/PKCS7Padding";
    private static readonly string MAC_ALGORITHM = "HmacSHA256";

    public static WCEncryptionPayload Encrypt(byte[] data, byte[] key)
    {
        byte[] iv = RandomBytes(16);
        using (Aes aesAlg = Aes.Create())
        {
            aesAlg.Key = key;
            aesAlg.IV = iv;
            aesAlg.Mode = CipherMode.CBC;
            aesAlg.Padding = PaddingMode.PKCS7;

            using (ICryptoTransform encryptor = aesAlg.CreateEncryptor())
            using (MemoryStream msEncrypt = new MemoryStream())
            {
                using (CryptoStream csEncrypt = new CryptoStream(msEncrypt, encryptor, CryptoStreamMode.Write))
                {
                    csEncrypt.Write(data, 0, data.Length);
                    csEncrypt.FlushFinalBlock();
                }

                byte[] encryptedData = msEncrypt.ToArray();
                string hmac = ComputeHmac(encryptedData, iv, key);

                return new WCEncryptionPayload(encryptedData.ToHex(), hmac, iv.ToHex());
            }
        }
    }

    public static byte[] Decrypt(WCEncryptionPayload payload, byte[] key)
    {
        byte[] data = payload.Data.HexStringToByteArray();
        byte[] iv = payload.Iv.HexStringToByteArray();
        string computedHmac = ComputeHmac(data, iv, key);

        if (!computedHmac.Equals(payload.Hmac, StringComparison.OrdinalIgnoreCase))
        {
            throw new InvalidHmacException();
        }

        using (Aes aesAlg = Aes.Create())
        {
            aesAlg.Key = key;
            aesAlg.IV = iv;
            aesAlg.Mode = CipherMode.CBC;
            aesAlg.Padding = PaddingMode.PKCS7;

            using (ICryptoTransform decryptor = aesAlg.CreateDecryptor())
            using (MemoryStream msDecrypt = new MemoryStream(data))
            using (CryptoStream csDecrypt = new CryptoStream(msDecrypt, decryptor, CryptoStreamMode.Read))
            using (MemoryStream msDecrypted = new MemoryStream())
            {
                csDecrypt.CopyTo(msDecrypted);
                return msDecrypted.ToArray();
            }
        }
    }

    private static string ComputeHmac(byte[] data, byte[] iv, byte[] key)
    {
        using (HMACSHA256 hmacAlg = new HMACSHA256(key))
        {
            byte[] payload = new byte[data.Length + iv.Length];
            Buffer.BlockCopy(data, 0, payload, 0, data.Length);
            Buffer.BlockCopy(iv, 0, payload, data.Length, iv.Length);

            byte[] hmacBytes = hmacAlg.ComputeHash(payload);
            return hmacBytes.ToHex();
        }
    }

    private static byte[] RandomBytes(int size)
    {
        using (RandomNumberGenerator rng = RandomNumberGenerator.Create())
        {
            byte[] bytes = new byte[size];
            rng.GetBytes(bytes);
            return bytes;
        }
    }
}