using System;
using System.Collections.Generic;
using System.Text.Json;
/*public class Crypto : ICrypto
{
    public static readonly string CryptoContext = "crypto";
    public static readonly string CryptoClientSeed = "client_ed25519_seed";
    public static readonly string ClientSeed = "CLIENT_SEED";

    private bool _initialized = false;

    public string Name => CryptoContext;
    public IGenericStore<string> KeyChain { get; }

    private readonly ICryptoUtils _utils;
    private readonly IRelayAuth _relayAuth;

    public Crypto(ICore core, IGenericStore<string> keyChain, CryptoUtils utils = null, RelayAuth relayAuth = null)
    {
        KeyChain = keyChain;
        _utils = utils ?? new CryptoUtils();
        _relayAuth = relayAuth ?? new RelayAuth();
    }

    public async Task Init()
    {
        if (_initialized)
        {
            return;
        }

        await KeyChain.Init();

        _initialized = true;
    }

    public bool HasKeys(string tag)
    {
        _checkInitialized();
        return KeyChain.Has(tag);
    }

    public async Task<string> GetClientId()
    {
        _checkInitialized();

        var seed = await _getClientSeed();
        var keyPair = await _relayAuth.GenerateKeyPair(seed);
        return _relayAuth.EncodeIss(keyPair.PublicKeyBytes);
    }

    public async Task<string> GenerateKeyPair()
    {
        _checkInitialized();

        var keyPair = _utils.GenerateKeyPair();
        return await _setPrivateKey(keyPair);
    }

    public async Task<string> GenerateSharedKey(string selfPublicKey, string peerPublicKey, string overrideTopic = null)
    {
        _checkInitialized();

        var privKey = _getPrivateKey(selfPublicKey);
        var symKey = _utils.DeriveSymKey(privKey, peerPublicKey);
        return SetSymKey(symKey, overrideTopic);
    }

    public string SetSymKey(string symKey, string overrideTopic = null)
    {
        _checkInitialized();

        var topic = overrideTopic ?? _utils.HashKey(symKey);
        KeyChain.Set(topic, symKey);
        return topic;
    }

    public async Task DeleteKeyPair(string publicKey)
    {
        _checkInitialized();
        await KeyChain.Delete(publicKey);
    }

    public async Task DeleteSymKey(string topic)
    {
        _checkInitialized();
        await KeyChain.Delete(topic);
    }

    public async Task<string?> Encode(string topic, Dictionary<string, dynamic> payload, EncodeOptions options = null)
    {
        _checkInitialized();

        EncodingValidation paramsValidation;
        if (options == null)
        {
            paramsValidation = _utils.ValidateEncoding();
        }
        else
        {
            paramsValidation = _utils.ValidateEncoding(
                type: options.Type,
                senderPublicKey: options.SenderPublicKey,
                receiverPublicKey: options.ReceiverPublicKey
            );
        }

        var message = JsonSerializer.Serialize(payload);

        if (_utils.IsTypeOneEnvelope(paramsValidation))
        {
            var selfPublicKey = paramsValidation.SenderPublicKey;
            var peerPublicKey = paramsValidation.ReceiverPublicKey;
            topic = await GenerateSharedKey(selfPublicKey, peerPublicKey);
        }

        var symKey = _getSymKey(topic);
        if (symKey == null)
        {
            return null;
        }

        var result = await _utils.Encrypt(message, symKey, type: paramsValidation.Type, senderPublicKey: paramsValidation.SenderPublicKey);

        return result;
    }

    public async Task<string?> Decode(string topic, string encoded, DecodeOptions options = null)
    {
        _checkInitialized();

        var paramsValidation = _utils.ValidateDecoding(encoded, receiverPublicKey: options?.ReceiverPublicKey);

        if (_utils.IsTypeOneEnvelope(paramsValidation))
        {
            var selfPublicKey = paramsValidation.ReceiverPublicKey;
            var peerPublicKey = paramsValidation.SenderPublicKey;
            topic = await GenerateSharedKey(selfPublicKey, peerPublicKey);
        }
        var symKey = _getSymKey(topic);
        if (symKey == null)
        {
            return null;
        }

        var message = await _utils.Decrypt(symKey, encoded);

        return message;
    }

    public async Task<string> SignJWT(string aud)
    {
        _checkInitialized();

        var seed = await _getClientSeed();
        var keyPair = await _relayAuth.GenerateKeyPair(seed);
        var sub = _utils.GenerateRandomBytes32();
        var jwt = await _relayAuth.SignJWT(sub, aud, WalletConnectConstants.ONE_DAY, keyPair);

        return jwt;
    }

    public int GetPayloadType(string encoded)
    {
        _checkInitialized();

        return _utils.Deserialize(encoded).Type;
    }

    private async Task<string> _setPrivateKey(CryptoKeyPair keyPair)
    {
        await KeyChain.Set(keyPair.PublicKey, keyPair.PrivateKey);
        return keyPair.PublicKey;
    }

    private string? _getPrivateKey(string publicKey)
    {
        return KeyChain.Get(publicKey);
    }

    private string? _getSymKey(string topic)
    {
        return KeyChain.Get(topic);
    }

    private async Task<Byte[]> _getClientSeed()
    {
        var seed = KeyChain.Get(ClientSeed);
        if (seed == null)
        {
            seed = _utils.GenerateRandomBytes32();
            await KeyChain.Set(ClientSeed, seed);
        }

        return HexadecimalStringToByteArray(seed);
    }

    private void _checkInitialized()
    {
        if (!_initialized)
        {
            throw Errors.GetInternalError(Errors.NOT_INITIALIZED);
        }
    }

    public ICryptoUtils GetUtils()
    {
        return _utils;
    }

    private byte[] HexadecimalStringToByteArray(string hex)
    {
        if (hex.Length % 2 != 0)
        {
            throw new ArgumentException("La cadena hexadecimal debe tener una longitud par", nameof(hex));
        }

        byte[] bytes = new byte[hex.Length / 2];
        for (int i = 0; i < hex.Length; i += 2)
        {
            bytes[i / 2] = Convert.ToByte(hex.Substring(i, 2), 16);
        }

        return bytes;
    }
}
*/