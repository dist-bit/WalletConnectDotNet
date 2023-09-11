using System.Text;

public static class ByteArrayExtensions
{
    private static readonly char[] HexChars = "0123456789abcdef".ToCharArray();

    public static string ToHex(this byte[] byteArray)
    {
        var result = new StringBuilder(byteArray.Length * 2);

        foreach (var octet in byteArray)
        {
            var firstIndex = (octet & 0xF0) >> 4;
            var secondIndex = octet & 0x0F;
            result.Append(HexChars[firstIndex]);
            result.Append(HexChars[secondIndex]);
        }

        return result.ToString();
    }
}
