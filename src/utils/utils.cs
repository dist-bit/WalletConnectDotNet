public class Utils
{

    public static List<byte> transformLongListToByteList(List<long> list)
    {
        List<byte> byteList = new List<byte>();
        foreach (long value in list)
        {
            byte byteValue = (byte)(value & 0xFF);
            byteList.Add(byteValue);
        }
        return byteList;
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