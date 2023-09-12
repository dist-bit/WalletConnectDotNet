public class KeyPair
{
    public List<long> PrivateKey { get; }
    public List<long> PublicKey { get; }

    public KeyPair(List<long> privateKey, List<long> publicKey)
    {
        PrivateKey = privateKey ?? throw new ArgumentNullException(nameof(privateKey));
        PublicKey = publicKey ?? throw new ArgumentNullException(nameof(publicKey));
    }

    public override int GetHashCode()
    {
        return PublicKey.GetHashCode();
    }

    public override bool Equals(object obj)
    {
        if (obj is KeyPair other)
        {
            return PublicKey.Equals(other.PublicKey) &&
                   PrivateKey.Equals(other.PrivateKey);
        }
        return false;
    }
}