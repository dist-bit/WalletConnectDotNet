


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
            Utils.PrintList(aliceSharedKey);
            Console.WriteLine("=====");
            Utils.PrintList(bobSharedKey);



        }
    }
}