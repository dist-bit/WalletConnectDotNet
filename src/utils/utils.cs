using System.Security.Cryptography;

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