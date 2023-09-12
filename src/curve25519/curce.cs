public class Curve
{
    static Curve25519 curve25519 = new Curve25519();

    static List<long> BasePoint = new List<long>
    {
        9, 0, 0, 0, 0, 0, 0, 0,
        0, 0, 0, 0, 0, 0, 0, 0,
        0, 0, 0, 0, 0, 0, 0, 0,
        0, 0, 0, 0, 0, 0, 0, 0
    };

    public KeyPair GenerateKeyPair()
    {
        var privateKey = new List<long>(Enumerable.Repeat((long)0, 32));
        var publicKey = new List<long>(Enumerable.Repeat((long)0, 32));


        var random = new Random();
        for (int i = 0; i < 32; i++)
        {
            privateKey[i] = random.Next(256);
        }

        privateKey[0] &= 248;
        privateKey[31] &= 127;
        privateKey[31] |= 64;


        ScalarBaseMult(publicKey, privateKey);

        List<byte> byteList = new List<byte>();



        List<long> intList = new List<long>();
        foreach (long value in publicKey)
        {
            byte byteValue = (byte)(value & 0xFF);
            intList.Add(byteValue);
        }



        return new KeyPair(privateKey, intList);
    }


    public void ScalarMult(List<long> dst, List<long> scalar, List<long> point)
    {

        curve25519.ScalarMultGeneric(dst, scalar, point);
    }

    public void ScalarBaseMult(List<long> dst, List<long> scalar)
    {
        curve25519.ScalarMultGeneric(dst, scalar, BasePoint);
    }


    void CheckBasepoint()
    {
        int[] expectedValues =
        {
        0x09, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00,
        0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00,
        0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00,
        0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00
    };

        if (BasePoint.Count != expectedValues.Length)
        {
            throw new ArgumentException("La longitud de basePoint no coincide con la longitud esperada.");
        }

        for (int i = 0; i < BasePoint.Count; i++)
        {
            if (BasePoint[i] != expectedValues[i])
            {
                throw new ArgumentException($"El valor en la posición {i} no coincide con el valor esperado.");
            }
        }
        Console.WriteLine("basePoint contiene los valores esperados.");
    }


    public List<long> X25519(List<long> scalar, List<long> point)
    {
        List<long> dst = new List<long>(Enumerable.Repeat((long)0, 32));
        return X25519Internal(dst, scalar, point);
    }

    private List<long> X25519Internal(List<long> dst, List<long> scalar, List<long> point)
    {
        List<long> input = new List<long>(Enumerable.Repeat((long)0, 32));

        if (scalar.Count != 32)
        {
            throw new ArgumentException($"Bad scalar length: {scalar.Count}, expected 32");
        }

        if (point.Count != 32)
        {
            throw new ArgumentException($"Bad point length: {point.Count}, expected 32");
        }

        //scalar.CopyTo(input.ToArray(), 0);

        Utils.SetRangeInList(input, scalar);

        if (Enumerable.SequenceEqual(point, BasePoint))
        {
            CheckBasepoint();
            ScalarBaseMult(dst, input);
        }
        else
        {
            List<long> baseValue = new List<long>(Enumerable.Repeat((long)0, 32));
            List<long> zero = new List<long>(Enumerable.Repeat((long)0, 32));


            Utils.SetRangeInList(baseValue, point);
            ScalarMult(dst, input, baseValue);
            if (Enumerable.SequenceEqual(dst, zero))
            {
                throw new ArgumentException("Bad input point: low order point");
            }
        }

        return new List<long>(dst);
    }
}
