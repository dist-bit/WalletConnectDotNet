


using System.Text;
using System.Security.Cryptography;

namespace Example
{

    public class Wind : EventArgs
    {
        public string Direction { get; }
        public int Strength { get; }

        public Wind(string direction, int strength)
        {
            Direction = direction;
            Strength = strength;
        }
    }



    public class Program
    {



        static void Main(string[] args)
        {
            var windChanged = new Event<Wind>();
            var wind = new Wind("ENE", 27);
            var curve = new Curve();

            windChanged.Subscribe(handler => Console.WriteLine("event"));


            var aliceKeyPair = curve.GenerateKeyPair();

            var bobKeyPair = curve.GenerateKeyPair();

            var aliceSharedKey = curve.X25519(aliceKeyPair.PrivateKey, bobKeyPair.PublicKey);
            var bobSharedKey = curve.X25519(bobKeyPair.PrivateKey, aliceKeyPair.PublicKey);

            var kp = curve.GenerateKeyPair();
            var sk = Utils.topByteArray(kp.PrivateKey).ToArray();
            var pk = Utils.topByteArray(kp.PublicKey).ToArray();


            var ckp = new CryptoKeyPair(BitConverter.ToString(sk).Replace("-", ""), BitConverter.ToString(pk).Replace("-", ""));


            List<long> longList = new List<long>
        { 227, 70, 61, 68, 18, 147, 159, 110, 87, 203, 109, 121, 108, 148, 142, 93, 165, 181, 21, 7, 15, 131, 181, 119, 183, 215, 165, 146, 159, 135, 230, 83};

            var ikm = Utils.topByteArray(longList);
            var salt = new byte[0];
            var info = new byte[0];
            var L = 32;

            var hkdf = new Hkdf();
            var actualPrk = BitConverter.ToString(hkdf.Extract(salt, ikm));
            var derive = hkdf.DeriveKey(salt, ikm, info, L);

            var key = BitConverter.ToString(derive).Replace("-", "").ToLower();
            var hash = Utils.CalculateSHA256Hash(key);
            var sk1 = Utils.ByteArrayFromHexString(hash);
            //Utils.PrintList(sk1);

            // Console.WriteLine(key);
            List<byte> iv = new List<byte> { 72, 110, 71, 192, 86, 158, 19, 153, 184, 233, 201, 96 };


            var encrypt = Utils.ChaCha20Poly1305EncryptMessage(sk1, iv.ToArray(), "Hola mundo");
            Utils.PrintList(encrypt);
            var decrypt = Utils.ChaCha20Poly1305DecryptMessage(sk1, encrypt);
             Utils.PrintList(decrypt);
        }

    }
}