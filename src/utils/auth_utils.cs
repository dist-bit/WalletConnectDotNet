public class AuthUtils
{
    public static string GenerateNonce()
    {
        return DateTime.Now.Ticks.ToString();
    }
}
