using System;
using System.Security.Cryptography;
using System.Text;

public class Utils
{

    public static string CalculateSHA256Hash(string input)
    {
        var encData = ByteArrayFromHexString(input);
        using (SHA256 sha256 = SHA256.Create())
        {
            byte[] hashValue = sha256.ComputeHash(encData);
            return BitConverter.ToString(hashValue).Replace("-", "").ToLower();
        }
    }

    public static byte[] ChaCha20Poly1305EncryptMessage(byte[] key, byte[] nonce, string message)
    {

        byte[] ciphertext = new byte[message.Length];
        byte[] tag = new byte[16];
        using (var chacha = new ChaCha20Poly1305(key))
        {
            chacha.Encrypt(nonce, Encoding.UTF8.GetBytes(message), ciphertext, tag, new byte[0]);
            var concatenation = Concatenation(nonce, ciphertext, tag, includeNonce: true, includeMac: true);
            return concatenation;
        }
    }

    public static string ChaCha20Poly1305DecryptMessage(byte[] key, byte[] encryptedMessage)
    {

        using (var chacha = new ChaCha20Poly1305(key))
        {


            byte[] extractedNonce = new byte[12];
            byte[] extractedCiphertext = new byte[encryptedMessage.Length - 28];
            byte[] extractedTag = new byte[16];

            Buffer.BlockCopy(encryptedMessage, 0, extractedNonce, 0, 12);
            Buffer.BlockCopy(encryptedMessage, 12, extractedCiphertext, 0, encryptedMessage.Length - 28);
            Buffer.BlockCopy(encryptedMessage, encryptedMessage.Length - 16, extractedTag, 0, 16);

            byte[] decryptedMessage = new byte[extractedCiphertext.Length];

            chacha.Decrypt(extractedNonce, extractedCiphertext, extractedTag, decryptedMessage, new byte[0]);
            return Encoding.UTF8.GetString(decryptedMessage).Replace(" ", "");
        }
    }

    public static byte[] Concatenation(byte[] nonce, byte[] cipher, byte[] mac, bool includeNonce = true, bool includeMac = true)
    {
        byte[] nonceBytes = nonce;
        byte[] cipherText = cipher;
        byte[] macBytes = mac;
        int n = cipherText.Length;

        if (includeNonce)
        {
            n += nonceBytes.Length;
        }
        if (includeMac)
        {
            n += macBytes.Length;
        }

        byte[] result = new byte[n];
        int i = 0;

        if (includeNonce)
        {
            Buffer.BlockCopy(nonceBytes, 0, result, i, nonceBytes.Length);
            i += nonceBytes.Length;
        }

        Buffer.BlockCopy(cipherText, 0, result, i, cipherText.Length);
        i += cipherText.Length;

        if (includeMac)
        {
            Buffer.BlockCopy(macBytes, 0, result, i, macBytes.Length);
        }

        return result;
    }

    public static byte[] ByteArrayFromHexString(string hex)
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

    public static byte[] topByteArray(List<long> list)
    {
        List<byte> byteList = new List<byte>();
        foreach (long value in list)
        {
            byte byteValue = (byte)(value & 0xFF);
            byteList.Add(byteValue);
        }
        return byteList.ToArray();
    }

    public static List<long> ToLongList(byte[] byteArray)
    {
        List<long> longList = new List<long>();


        foreach (long value in byteArray)
        {

            longList.Add((long)value);
        }

        return longList;
    }

    public static void PrintList<T>(IEnumerable<T> list)
    {
        foreach (var item in list)
        {
            Console.Write(item + " ");
        }

        Console.WriteLine();
    }

    public static void SetRangeInList(List<long> input, List<long> scalar)
    {
        int count = input.Count;

        if (scalar.Count >= count)
        {
            input.Clear();
            input.AddRange(scalar);
        }
        else
        {
            for (int i = scalar.Count; i < count; i++)
            {
                scalar.Add(scalar[i % scalar.Count]);
            }

            input.Clear();
            input.AddRange(scalar.Take(count));
        }
    }


}