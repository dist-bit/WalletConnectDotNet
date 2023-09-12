public class FieldElement
{
    public List<long> InnerList;

    public FieldElement()
    {
        InnerList = new List<long>(10);
        for (int index = 0; index < 10; index++)
        {
            InnerList.Add(0);
        }
    }

    public FieldElement(List<long> list)
    {
        InnerList = list;
    }

    public long this[int index]
    {
        get { return InnerList[index]; }
        set { InnerList[index] = value; }
    }

    public int Length => InnerList.Count;
}


public class Curve25519
{
    FieldElement zero = new FieldElement();
    public void FieldElementCopy(FieldElement src, int srcPos, FieldElement dest, int destPos, int length)
    {
        for (int i = 0; i < length; i++)
        {
            dest.InnerList[destPos + i] = src.InnerList[srcPos + i];
        }
    }

    public void FieldElementFullCopy(FieldElement src, FieldElement dest)
    {
        FieldElementCopy(src, 0, dest, 0, dest.Length);
    }



    public void FeZero(FieldElement fe)
    {
        FieldElementFullCopy(zero, fe);
    }

    public void FeOne(FieldElement fe)
    {
        FeZero(fe);
        fe[0] = 1;
    }

    public void FeAdd(FieldElement dst, FieldElement a, FieldElement b)
    {
        dst[0] = a[0] + b[0];
        dst[1] = a[1] + b[1];
        dst[2] = a[2] + b[2];
        dst[3] = a[3] + b[3];
        dst[4] = a[4] + b[4];
        dst[5] = a[5] + b[5];
        dst[6] = a[6] + b[6];
        dst[7] = a[7] + b[7];
        dst[8] = a[8] + b[8];
        dst[9] = a[9] + b[9];
    }

    public void FeSub(FieldElement dst, FieldElement a, FieldElement b)
    {
        dst[0] = a[0] - b[0];
        dst[1] = a[1] - b[1];
        dst[2] = a[2] - b[2];
        dst[3] = a[3] - b[3];
        dst[4] = a[4] - b[4];
        dst[5] = a[5] - b[5];
        dst[6] = a[6] - b[6];
        dst[7] = a[7] - b[7];
        dst[8] = a[8] - b[8];
        dst[9] = a[9] - b[9];
    }

    public void FeCopy(FieldElement dst, FieldElement src)
    {
        FieldElementFullCopy(src, dst);
    }


    public void FeCSwap(FieldElement f, FieldElement g, long b)
    {
        b = (long)-b;
        for (var i = 0; i < f.Length; i++)
        {
            var t = b & (f[i] ^ g[i]);
            f[i] ^= t;
            g[i] ^= t;
        }
    }

    // load3 lee un valor de 24 bits en formato little-endian desde "input".
    public long Load3(List<long> input)
    {
        var r = input[0];
        r |= input[1] << 8;
        r |= input[2] << 16;
        return r;
    }

    public long Load4(List<long> input)
    {

        long r;
        r = input[0];
        r |= input[1] << 8;
        r |= input[2] << 16;
        r |= input[3] << 24;

        return r;
    }

    public void FeFromBytes(FieldElement dst, List<long> src)
    {


        var h0 = Load4(src.Skip(0).ToList());
        var h1 = Load3(src.Skip(4).ToList()) << 6;

        var h2 = Load3(src.Skip(7).ToList()) << 5;
        var h3 = Load3(src.Skip(10).ToList()) << 3;
        var h4 = Load3(src.Skip(13).ToList()) << 2;
        var h5 = Load4(src.Skip(16).ToList());
        var h6 = Load3(src.Skip(20).ToList()) << 7;
        var h7 = Load3(src.Skip(23).ToList()) << 5;
        var h8 = Load3(src.Skip(26).ToList()) << 4;
        var h9 = (Load3(src.Skip(29).ToList()) & 0x7fffff) << 2;


        long[] carry = new long[10];
        carry[9] = (h9 + (1 << 24)) >> 25;

        h0 += carry[9] * 19;
        h9 -= carry[9] << 25;
        
        carry[1] = (h1 + (1 << 24)) >> 25;
        h2 += carry[1];
        h1 -= carry[1] << 25;
        carry[3] = (h3 + (1 << 24)) >> 25;
        h4 += carry[3];
        h3 -= carry[3] << 25;
        carry[5] = (h5 + (1 << 24)) >> 25;
        h6 += carry[5];
        h5 -= carry[5] << 25;
        carry[7] = (h7 + (1 << 24)) >> 25;
        h8 += carry[7];
        h7 -= carry[7] << 25;

        carry[0] = (h0 + (1 << 25)) >> 26;
        h1 += carry[0];
        h0 -= carry[0] << 26;
        carry[2] = (h2 + (1 << 25)) >> 26;
        h3 += carry[2];
        h2 -= carry[2] << 26;
        carry[4] = (h4 + (1 << 25)) >> 26;
        h5 += carry[4];
        h4 -= carry[4] << 26;
        carry[6] = (h6 + (1 << 25)) >> 26;
        h7 += carry[6];
        h6 -= carry[6] << 26;
        carry[8] = (h8 + (1 << 25)) >> 26;
        h9 += carry[8];
        h8 -= carry[8] << 26;


        dst[0] = h0;
        dst[1] = h1;
        dst[2] = h2;
        dst[3] = h3;
        dst[4] = h4;
        dst[5] = h5;
        dst[6] = h6;
        dst[7] = h7;
        dst[8] = h8;
        dst[9] = h9;


    }


    public void FeToBytes(List<long> s, FieldElement h)
    {
        var carry = new long[10];

        var q = (19 * h[9] + (1 << 24)) >> 25;
        q = (h[0] + q) >> 26;
        q = (h[1] + q) >> 25;
        q = (h[2] + q) >> 26;
        q = (h[3] + q) >> 25;
        q = (h[4] + q) >> 26;
        q = (h[5] + q) >> 25;
        q = (h[6] + q) >> 26;
        q = (h[7] + q) >> 25;
        q = (h[8] + q) >> 26;
        q = (h[9] + q) >> 25;

        h[0] += 19 * q;

        carry[0] = h[0] >> 26;
        h[1] += carry[0];
        h[0] -= carry[0] << 26;
        carry[1] = h[1] >> 25;
        h[2] += carry[1];
        h[1] -= carry[1] << 25;
        carry[2] = h[2] >> 26;
        h[3] += carry[2];
        h[2] -= carry[2] << 26;
        carry[3] = h[3] >> 25;
        h[4] += carry[3];
        h[3] -= carry[3] << 25;
        carry[4] = h[4] >> 26;
        h[5] += carry[4];
        h[4] -= carry[4] << 26;
        carry[5] = h[5] >> 25;
        h[6] += carry[5];
        h[5] -= carry[5] << 25;
        carry[6] = h[6] >> 26;
        h[7] += carry[6];
        h[6] -= carry[6] << 26;
        carry[7] = h[7] >> 25;
        h[8] += carry[7];
        h[7] -= carry[7] << 25;
        carry[8] = h[8] >> 26;
        h[9] += carry[8];
        h[8] -= carry[8] << 26;
        carry[9] = h[9] >> 25;
        h[9] -= carry[9] << 25;

        s[0] = h[0] >> 0;
        s[1] = h[0] >> 8;
        s[2] = h[0] >> 16;
        s[3] = (h[0] >> 24) | (h[1] << 2);
        s[4] = h[1] >> 6;
        s[5] = h[1] >> 14;
        s[6] = (h[1] >> 22) | (h[2] << 3);
        s[7] = h[2] >> 5;
        s[8] = h[2] >> 13;
        s[9] = (h[2] >> 21) | (h[3] << 5);
        s[10] = h[3] >> 3;
        s[11] = h[3] >> 11;
        s[12] = (h[3] >> 19) | (h[4] << 6);
        s[13] = h[4] >> 2;
        s[14] = h[4] >> 10;
        s[15] = h[4] >> 18;
        s[16] = h[5] >> 0;
        s[17] = h[5] >> 8;
        s[18] = h[5] >> 16;
        s[19] = (h[5] >> 24) | (h[6] << 1);
        s[20] = h[6] >> 7;
        s[21] = h[6] >> 15;
        s[22] = (h[6] >> 23) | (h[7] << 3);
        s[23] = h[7] >> 5;
        s[24] = h[7] >> 13;
        s[25] = (h[7] >> 21) | (h[8] << 4);
        s[26] = h[8] >> 4;
        s[27] = h[8] >> 12;
        s[28] = (h[8] >> 20) | (h[9] << 6);
        s[29] = h[9] >> 2;
        s[30] = h[9] >> 10;
        s[31] = h[9] >> 18;
    }

    public void FeMul(FieldElement h, FieldElement f, FieldElement g)
    {
        var f0 = f[0];
        var f1 = f[1];
        var f2 = f[2];
        var f3 = f[3];
        var f4 = f[4];
        var f5 = f[5];
        var f6 = f[6];
        var f7 = f[7];
        var f8 = f[8];
        var f9 = f[9];
        var g0 = g[0];
        var g1 = g[1];
        var g2 = g[2];
        var g3 = g[3];
        var g4 = g[4];
        var g5 = g[5];
        var g6 = g[6];
        var g7 = g[7];
        var g8 = g[8];
        var g9 = g[9];
        var g1_19 = 19 * g[1]; // 1.4*2^29
        var g2_19 = 19 * g[2]; // 1.4*2^30; still ok
        var g3_19 = 19 * g[3];
        var g4_19 = 19 * g[4];
        var g5_19 = 19 * g[5];
        var g6_19 = 19 * g[6];
        var g7_19 = 19 * g[7];
        var g8_19 = 19 * g[8];
        var g9_19 = 19 * g[9];
        var f1_2 = 2 * f[1];
        var f3_2 = 2 * f[3];
        var f5_2 = 2 * f[5];
        var f7_2 = 2 * f[7];
        var f9_2 = 2 * f[9];
        var f0g0 = f0 * g0;
        var f0g1 = f0 * g1;
        var f0g2 = f0 * g2;
        var f0g3 = f0 * g3;
        var f0g4 = f0 * g4;
        var f0g5 = f0 * g5;
        var f0g6 = f0 * g6;
        var f0g7 = f0 * g7;
        var f0g8 = f0 * g8;
        var f0g9 = f0 * g9;
        var f1g0 = f1 * g0;
        var f1g1_2 = f1_2 * g1;
        var f1g2 = f1 * g2;
        var f1g3_2 = f1_2 * g3;
        var f1g4 = f1 * g4;
        var f1g5_2 = f1_2 * g5;
        var f1g6 = f1 * g6;
        var f1g7_2 = f1_2 * g7;
        var f1g8 = f1 * g8;
        var f1g9_38 = f1_2 * g9_19;
        var f2g0 = f2 * g0;
        var f2g1 = f2 * g1;
        var f2g2 = f2 * g2;
        var f2g3 = f2 * g3;
        var f2g4 = f2 * g4;
        var f2g5 = f2 * g5;
        var f2g6 = f2 * g6;
        var f2g7 = f2 * g7;
        var f2g8_19 = f2 * g8_19;
        var f2g9_19 = f2 * g9_19;
        var f3g0 = f3 * g0;
        var f3g1_2 = f3_2 * g1;
        var f3g2 = f3 * g2;
        var f3g3_2 = f3_2 * g3;
        var f3g4 = f3 * g4;
        var f3g5_2 = f3_2 * g5;
        var f3g6 = f3 * g6;
        var f3g7_38 = f3_2 * g7_19;
        var f3g8_19 = f3 * g8_19;
        var f3g9_38 = f3_2 * g9_19;
        var f4g0 = f4 * g0;
        var f4g1 = f4 * g1;
        var f4g2 = f4 * g2;
        var f4g3 = f4 * g3;
        var f4g4 = f4 * g4;
        var f4g5 = f4 * g5;
        var f4g6_19 = f4 * g6_19;
        var f4g7_19 = f4 * g7_19;
        var f4g8_19 = f4 * g8_19;
        var f4g9_19 = f4 * g9_19;
        var f5g0 = f5 * g0;
        var f5g1_2 = f5_2 * g1;
        var f5g2 = f5 * g2;
        var f5g3_2 = f5_2 * g3;
        var f5g4 = f5 * g4;
        var f5g5_38 = f5_2 * g5_19;
        var f5g6_19 = f5 * g6_19;
        var f5g7_38 = f5_2 * g7_19;
        var f5g8_19 = f5 * g8_19;
        var f5g9_38 = f5_2 * g9_19;
        var f6g0 = f6 * g0;
        var f6g1 = f6 * g1;
        var f6g2 = f6 * g2;
        var f6g3 = f6 * g3;
        var f6g4_19 = f6 * g4_19;
        var f6g5_19 = f6 * g5_19;
        var f6g6_19 = f6 * g6_19;
        var f6g7_19 = f6 * g7_19;
        var f6g8_19 = f6 * g8_19;
        var f6g9_19 = f6 * g9_19;
        var f7g0 = f7 * g0;
        var f7g1_2 = f7_2 * g1;
        var f7g2 = f7 * g2;
        var f7g3_38 = f7_2 * g3_19;
        var f7g4_19 = f7 * g4_19;
        var f7g5_38 = f7_2 * g5_19;
        var f7g6_19 = f7 * g6_19;
        var f7g7_38 = f7_2 * g7_19;
        var f7g8_19 = f7 * g8_19;
        var f7g9_38 = f7_2 * g9_19;
        var f8g0 = f8 * g0;
        var f8g1 = f8 * g1;
        var f8g2_19 = f8 * g2_19;
        var f8g3_19 = f8 * g3_19;
        var f8g4_19 = f8 * g4_19;
        var f8g5_19 = f8 * g5_19;
        var f8g6_19 = f8 * g6_19;
        var f8g7_19 = f8 * g7_19;
        var f8g8_19 = f8 * g8_19;
        var f8g9_19 = f8 * g9_19;
        var f9g0 = f9 * g0;
        var f9g1_38 = f9_2 * g1_19;
        var f9g2_19 = f9 * g2_19;
        var f9g3_38 = f9_2 * g3_19;
        var f9g4_19 = f9 * g4_19;
        var f9g5_38 = f9_2 * g5_19;
        var f9g6_19 = f9 * g6_19;
        var f9g7_38 = f9_2 * g7_19;
        var f9g8_19 = f9 * g8_19;
        var f9g9_38 = f9_2 * g9_19;
        var h0 = f0g0 +
            f1g9_38 +
            f2g8_19 +
            f3g7_38 +
            f4g6_19 +
            f5g5_38 +
            f6g4_19 +
            f7g3_38 +
            f8g2_19 +
            f9g1_38;
        var h1 = f0g1 +
            f1g0 +
            f2g9_19 +
            f3g8_19 +
            f4g7_19 +
            f5g6_19 +
            f6g5_19 +
            f7g4_19 +
            f8g3_19 +
            f9g2_19;
        var h2 = f0g2 +
            f1g1_2 +
            f2g0 +
            f3g9_38 +
            f4g8_19 +
            f5g7_38 +
            f6g6_19 +
            f7g5_38 +
            f8g4_19 +
            f9g3_38;
        var h3 = f0g3 +
            f1g2 +
            f2g1 +
            f3g0 +
            f4g9_19 +
            f5g8_19 +
            f6g7_19 +
            f7g6_19 +
            f8g5_19 +
            f9g4_19;
        var h4 = f0g4 +
            f1g3_2 +
            f2g2 +
            f3g1_2 +
            f4g0 +
            f5g9_38 +
            f6g8_19 +
            f7g7_38 +
            f8g6_19 +
            f9g5_38;
        var h5 = f0g5 +
            f1g4 +
            f2g3 +
            f3g2 +
            f4g1 +
            f5g0 +
            f6g9_19 +
            f7g8_19 +
            f8g7_19 +
            f9g6_19;
        var h6 = f0g6 +
            f1g5_2 +
            f2g4 +
            f3g3_2 +
            f4g2 +
            f5g1_2 +
            f6g0 +
            f7g9_38 +
            f8g8_19 +
            f9g7_38;
        var h7 =
            f0g7 + f1g6 + f2g5 + f3g4 + f4g3 + f5g2 + f6g1 + f7g0 + f8g9_19 + f9g8_19;
        var h8 = f0g8 +
            f1g7_2 +
            f2g6 +
            f3g5_2 +
            f4g4 +
            f5g3_2 +
            f6g2 +
            f7g1_2 +
            f8g0 +
            f9g9_38;
        var h9 = f0g9 + f1g8 + f2g7 + f3g6 + f4g5 + f5g4 + f6g3 + f7g2 + f8g1 + f9g0;

        var carry = new long[10];

        carry[0] = (h0 + (1 << 25)) >> 26;
        h1 += carry[0];
        h0 -= carry[0] << 26;
        carry[4] = (h4 + (1 << 25)) >> 26;
        h5 += carry[4];
        h4 -= carry[4] << 26;

        carry[1] = (h1 + (1 << 24)) >> 25;
        h2 += carry[1];
        h1 -= carry[1] << 25;
        carry[5] = (h5 + (1 << 24)) >> 25;
        h6 += carry[5];
        h5 -= carry[5] << 25;

        carry[2] = (h2 + (1 << 25)) >> 26;
        h3 += carry[2];
        h2 -= carry[2] << 26;
        carry[6] = (h6 + (1 << 25)) >> 26;
        h7 += carry[6];
        h6 -= carry[6] << 26;

        carry[3] = (h3 + (1 << 24)) >> 25;
        h4 += carry[3];
        h3 -= carry[3] << 25;
        carry[7] = (h7 + (1 << 24)) >> 25;
        h8 += carry[7];
        h7 -= carry[7] << 25;

        carry[4] = (h4 + (1 << 25)) >> 26;
        h5 += carry[4];
        h4 -= carry[4] << 26;
        carry[8] = (h8 + (1 << 25)) >> 26;
        h9 += carry[8];
        h8 -= carry[8] << 26;

        carry[9] = (h9 + (1 << 24)) >> 25;
        h0 += carry[9] * 19;
        h9 -= carry[9] << 25;

        carry[0] = (h0 + (1 << 25)) >> 26;
        h1 += carry[0];
        h0 -= carry[0] << 26;

        h[0] = h0;
        h[1] = h1;
        h[2] = h2;
        h[3] = h3;
        h[4] = h4;
        h[5] = h5;
        h[6] = h6;
        h[7] = h7;
        h[8] = h8;
        h[9] = h9;
    }

    void FeSquare(FieldElement h, FieldElement f)
    {
        var f0 = f[0];
        var f1 = f[1];
        var f2 = f[2];
        var f3 = f[3];
        var f4 = f[4];
        var f5 = f[5];
        var f6 = f[6];
        var f7 = f[7];
        var f8 = f[8];
        var f9 = f[9];

        var f0_2 = 2 * f0;
        var f1_2 = 2 * f1;
        var f2_2 = 2 * f2;
        var f3_2 = 2 * f3;
        var f4_2 = 2 * f4;
        var f5_2 = 2 * f5;
        var f6_2 = 2 * f6;
        var f7_2 = 2 * f7;

        var f5_38 = 38 * f5; // 1.31*2^30
        var f6_19 = 19 * f6; // 1.31*2^30
        var f7_38 = 38 * f7; // 1.31*2^30
        var f8_19 = 19 * f8; // 1.31*2^30
        var f9_38 = 38 * f9; // 1.31*2^30

        var f0f0 = f0 * f0;
        var f0f1_2 = f0_2 * f1;
        var f0f2_2 = f0_2 * f2;
        var f0f3_2 = f0_2 * f3;
        var f0f4_2 = f0_2 * f4;
        var f0f5_2 = f0_2 * f5;
        var f0f6_2 = f0_2 * f6;
        var f0f7_2 = f0_2 * f7;
        var f0f8_2 = f0_2 * f8;
        var f0f9_2 = f0_2 * f9;

        var f1f1_2 = f1_2 * f1;
        var f1f2_2 = f1_2 * f2;
        var f1f3_4 = f1_2 * f3_2;
        var f1f4_2 = f1_2 * f4;
        var f1f5_4 = f1_2 * f5_2;
        var f1f6_2 = f1_2 * f6;
        var f1f7_4 = f1_2 * f7_2;
        var f1f8_2 = f1_2 * f8;
        var f1f9_76 = f1_2 * f9_38;

        var f2f2 = f2 * f2;
        var f2f3_2 = f2_2 * f3;
        var f2f4_2 = f2_2 * f4;
        var f2f5_2 = f2_2 * f5;
        var f2f6_2 = f2_2 * f6;
        var f2f7_2 = f2_2 * f7;
        var f2f8_38 = f2_2 * f8_19;
        var f2f9_38 = f2 * f9_38;

        var f3f3_2 = f3_2 * f3;
        var f3f4_2 = f3_2 * f4;
        var f3f5_4 = f3_2 * f5_2;
        var f3f6_2 = f3_2 * f6;
        var f3f7_76 = f3_2 * f7_38;
        var f3f8_38 = f3_2 * f8_19;
        var f3f9_76 = f3_2 * f9_38;

        var f4f4 = f4 * f4;
        var f4f5_2 = f4_2 * f5;
        var f4f6_38 = f4_2 * f6_19;
        var f4f7_38 = f4 * f7_38;
        var f4f8_38 = f4_2 * f8_19;
        var f4f9_38 = f4 * f9_38;

        var f5f5_38 = f5 * f5_38;
        var f5f6_38 = f5_2 * f6_19;
        var f5f7_76 = f5_2 * f7_38;
        var f5f8_38 = f5_2 * f8_19;
        var f5f9_76 = f5_2 * f9_38;

        var f6f6_19 = f6 * f6_19;
        var f6f7_38 = f6 * f7_38;
        var f6f8_38 = f6_2 * f8_19;
        var f6f9_38 = f6 * f9_38;

        var f7f7_38 = f7 * f7_38;
        var f7f8_38 = f7_2 * f8_19;
        var f7f9_76 = f7_2 * f9_38;

        var f8f8_19 = f8 * f8_19;
        var f8f9_38 = f8 * f9_38;

        var f9f9_38 = f9 * f9_38;

        var h0 = f0f0 + f1f9_76 + f2f8_38 + f3f7_76 + f4f6_38 + f5f5_38;
        var h1 = f0f1_2 + f2f9_38 + f3f8_38 + f4f7_38 + f5f6_38;
        var h2 = f0f2_2 + f1f1_2 + f3f9_76 + f4f8_38 + f5f7_76 + f6f6_19;
        var h3 = f0f3_2 + f1f2_2 + f4f9_38 + f5f8_38 + f6f7_38;
        var h4 = f0f4_2 + f1f3_4 + f2f2 + f5f9_76 + f6f8_38 + f7f7_38;
        var h5 = f0f5_2 + f1f4_2 + f2f3_2 + f6f9_38 + f7f8_38;
        var h6 = f0f6_2 + f1f5_4 + f2f4_2 + f3f3_2 + f7f9_76 + f8f8_19;
        var h7 = f0f7_2 + f1f6_2 + f2f5_2 + f3f4_2 + f8f9_38;
        var h8 = f0f8_2 + f1f7_4 + f2f6_2 + f3f5_4 + f4f4 + f9f9_38;
        var h9 = f0f9_2 + f1f8_2 + f2f7_2 + f3f6_2 + f4f5_2;

        var carry = new List<long>(new long[10]);

        carry[0] = (h0 + (1 << 25)) >> 26;
        h1 += carry[0];
        h0 -= carry[0] << 26;
        carry[4] = (h4 + (1 << 25)) >> 26;
        h5 += carry[4];
        h4 -= carry[4] << 26;

        carry[1] = (h1 + (1 << 24)) >> 25;
        h2 += carry[1];
        h1 -= carry[1] << 25;
        carry[5] = (h5 + (1 << 24)) >> 25;
        h6 += carry[5];
        h5 -= carry[5] << 25;

        carry[2] = (h2 + (1 << 25)) >> 26;
        h3 += carry[2];
        h2 -= carry[2] << 26;
        carry[6] = (h6 + (1 << 25)) >> 26;
        h7 += carry[6];
        h6 -= carry[6] << 26;

        carry[3] = (h3 + (1 << 24)) >> 25;
        h4 += carry[3];
        h3 -= carry[3] << 25;
        carry[7] = (h7 + (1 << 24)) >> 25;
        h8 += carry[7];
        h7 -= carry[7] << 25;

        carry[4] = (h4 + (1 << 25)) >> 26;
        h5 += carry[4];
        h4 -= carry[4] << 26;
        carry[8] = (h8 + (1 << 25)) >> 26;
        h9 += carry[8];
        h8 -= carry[8] << 26;

        carry[9] = (h9 + (1 << 24)) >> 25;
        h0 += carry[9] * 19;
        h9 -= carry[9] << 25;

        carry[0] = (h0 + (1 << 25)) >> 26;
        h1 += carry[0];
        h0 -= carry[0] << 26;

        h[0] = h0;
        h[1] = h1;
        h[2] = h2;
        h[3] = h3;
        h[4] = h4;
        h[5] = h5;
        h[6] = h6;
        h[7] = h7;
        h[8] = h8;
        h[9] = h9;
    }

    void FeMul121666(FieldElement h, FieldElement f)
    {
        var h0 = f[0] * 121666;
        var h1 = f[1] * 121666;
        var h2 = f[2] * 121666;
        var h3 = f[3] * 121666;
        var h4 = f[4] * 121666;
        var h5 = f[5] * 121666;
        var h6 = f[6] * 121666;
        var h7 = f[7] * 121666;
        var h8 = f[8] * 121666;
        var h9 = f[9] * 121666;

        var carry = new List<long>(new long[10]);

        carry[9] = (h9 + (1 << 24)) >> 25;
        h0 += carry[9] * 19;
        h9 -= carry[9] << 25;
        carry[1] = (h1 + (1 << 24)) >> 25;
        h2 += carry[1];
        h1 -= carry[1] << 25;
        carry[3] = (h3 + (1 << 24)) >> 25;
        h4 += carry[3];
        h3 -= carry[3] << 25;
        carry[5] = (h5 + (1 << 24)) >> 25;
        h6 += carry[5];
        h5 -= carry[5] << 25;
        carry[7] = (h7 + (1 << 24)) >> 25;
        h8 += carry[7];
        h7 -= carry[7] << 25;

        carry[0] = (h0 + (1 << 25)) >> 26;
        h1 += carry[0];
        h0 -= carry[0] << 26;
        carry[2] = (h2 + (1 << 25)) >> 26;
        h3 += carry[2];
        h2 -= carry[2] << 26;
        carry[4] = (h4 + (1 << 25)) >> 26;
        h5 += carry[4];
        h4 -= carry[4] << 26;
        carry[6] = (h6 + (1 << 25)) >> 26;
        h7 += carry[6];
        h6 -= carry[6] << 26;
        carry[8] = (h8 + (1 << 25)) >> 26;
        h9 += carry[8];
        h8 -= carry[8] << 26;

        h[0] = h0;
        h[1] = h1;
        h[2] = h2;
        h[3] = h3;
        h[4] = h4;
        h[5] = h5;
        h[6] = h6;
        h[7] = h7;
        h[8] = h8;
        h[9] = h9;
    }

    void FeInvert(FieldElement output, FieldElement z)
    {
        var t0 = new FieldElement();
        var t1 = new FieldElement();
        var t2 = new FieldElement();
        var t3 = new FieldElement();
        int i = 0;

        FeSquare(t0, z);

        for (i = 1; i < 1; i++)
        {
            FeSquare(t0, t0);
        }

        FeSquare(t1, t0);

        for (i = 1; i < 2; i++)
        {
            FeSquare(t1, t1);
        }

        FeMul(t1, z, t1);
        FeMul(t0, t0, t1);

        FeSquare(t2, t0);

        for (i = 1; i < 1; i++)
        {
            FeSquare(t2, t2);
        }

        FeMul(t1, t1, t2);
        FeSquare(t2, t1);

        for (i = 1; i < 5; i++)
        {
            FeSquare(t2, t2);
        }

        FeMul(t1, t2, t1);
        FeSquare(t2, t1);

        for (i = 1; i < 10; i++)
        {
            FeSquare(t2, t2);
        }

        FeMul(t2, t2, t1);
        FeSquare(t3, t2);

        for (i = 1; i < 20; i++)
        {
            FeSquare(t3, t3);
        }

        FeMul(t2, t3, t2);
        FeSquare(t2, t2);

        for (i = 1; i < 10; i++)
        {
            FeSquare(t2, t2);
        }

        FeMul(t1, t2, t1);
        FeSquare(t2, t1);

        for (i = 1; i < 50; i++)
        {
            FeSquare(t2, t2);
        }

        FeMul(t2, t2, t1);
        FeSquare(t3, t2);

        for (i = 1; i < 100; i++)
        {
            FeSquare(t3, t3);
        }

        FeMul(t2, t3, t2);
        FeSquare(t2, t2);

        for (i = 1; i < 50; i++)
        {
            FeSquare(t2, t2);
        }

        FeMul(t1, t2, t1);
        FeSquare(t1, t1);

        for (i = 1; i < 5; i++)
        {
            FeSquare(t1, t1);
        }

        FeMul(output, t1, t0);
    }

    public void ScalarMultGeneric(List<long> output, List<long> input, List<long> _base)
    {
        var e = new List<long>(Enumerable.Repeat((long)0, 32));


        Utils.SetRangeInList(e, input);
        e[0] &= 248;
        e[31] &= 127;
        e[31] |= 64;



        var x1 = new FieldElement();
        var x2 = new FieldElement();
        var z2 = new FieldElement();
        var x3 = new FieldElement();
        var z3 = new FieldElement();
        var tmp0 = new FieldElement();
        var tmp1 = new FieldElement();

        FeFromBytes(x1, _base);
        FeOne(x2);
        FeCopy(x3, x1);
        FeOne(z3);

        long swap = 0;
        for (var pos = 254; pos >= 0; pos--)
        {
            var b = e[pos / 8] >> (pos % 8);
            b &= 1;
            swap ^= b;
            FeCSwap(x2, x3, swap);
            FeCSwap(z2, z3, swap);
            swap = b;

            FeSub(tmp0, x3, z3);
            FeSub(tmp1, x2, z2);
            FeAdd(x2, x2, z2);
            FeAdd(z2, x3, z3);
            FeMul(z3, tmp0, x2);
            FeMul(z2, z2, tmp1);
            FeSquare(tmp0, tmp1);
            FeSquare(tmp1, x2);
            FeAdd(x3, z3, z2);
            FeSub(z2, z3, z2);
            FeMul(x2, tmp1, tmp0);
            FeSub(tmp1, tmp1, tmp0);
            FeSquare(z2, z2);
            FeMul121666(z3, tmp1);
            FeSquare(x3, x3);
            FeAdd(tmp0, tmp0, z3);
            FeMul(z3, x1, z2);
            FeMul(z2, tmp1, tmp0);
        }

        FeCSwap(x2, x3, swap);
        FeCSwap(z2, z3, swap);

        FeInvert(z2, z2);
        FeMul(x2, x2, z2);
        FeToBytes(output, x2);
    }

}