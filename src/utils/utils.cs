public class Utils {

    private static object?astToUnsigned(object number)
{
    Type type = number.GetType();
    unchecked
    {
        if (type == typeof(int)) return (long)(int)number;
        if (type == typeof(long)) return (ulong)(long)number;
        if (type == typeof(short)) return (ushort)(short)number;
        if (type == typeof(sbyte)) return (byte)(sbyte)number;
    }
    return null;
}

     public static void PrintList<T>(IEnumerable<T> list)
    {
        foreach (var item in list)
        {
            Console.Write(item + " ");
        }

         Console.WriteLine();
    }

    public static void SetRangeInList(List<int> input, List<int> scalar)
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