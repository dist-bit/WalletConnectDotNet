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

            windChanged.Subscribe(handler => Console.WriteLine("event"));

    
        }
    }
}