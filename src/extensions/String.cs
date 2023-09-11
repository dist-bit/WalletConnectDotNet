public static class StringExtensions
{
    private const string HexChars = "0123456789abcdef";

    public static byte[] HexStringToByteArray(this string hex)
    {
        hex = hex.ToLower();
        byte[] result = new byte[hex.Length / 2];

        for (int i = 0; i < hex.Length; i += 2)
        {
            int firstIndex = HexChars.IndexOf(hex[i]);
            int secondIndex = HexChars.IndexOf(hex[i + 1]);

            int octet = (firstIndex << 4) | secondIndex;
            result[i / 2] = (byte)octet;
        }

        return result;
    }
}