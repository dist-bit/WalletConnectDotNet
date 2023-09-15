public class WalletConnectUtils
{
    public static bool IsExpired(int expiry)
    {
        return DateTime.UtcNow.CompareTo(DateTimeOffset.FromUnixTimeMilliseconds(ToMilliseconds(expiry)).UtcDateTime) >= 0;
    }

    public static int ToMilliseconds(int seconds)
    {
        return seconds * 1000;
    }

    public static int CalculateExpiry(int offset)
    {
        return (int)DateTime.UtcNow.Subtract(new DateTime(1970, 1, 1)).TotalSeconds + offset;
    }

    public static string GetOS()
    {
        return $"{Environment.OSVersion.Platform}-{Environment.OSVersion.Version}";
    }

    public static string GetId()
    {
        if (Environment.OSVersion.Platform == PlatformID.Win32NT)
        {
            return "windows";
        }
        else if (Environment.OSVersion.Platform == PlatformID.Unix)
        {
            return "linux";
        }
        else if (Environment.OSVersion.Platform == PlatformID.MacOSX)
        {
            return "macos";
        }
        else if (Environment.OSVersion.Platform == PlatformID.Xbox)
        {
            return "xbox";
        }
        /* else if (Environment.OSVersion.Platform == PlatformID.Android)
         {
             return "android";
         }
         else if (Environment.OSVersion.Platform == PlatformID.iOS)
         {
             return "ios";
         } */
        else
        {
            return "unknown";
        }
    }

    public static string FormatUA(string protocol, int version, string sdkVersion)
    {
        string os = GetOS();
        string id = GetId();
        return $"{protocol}-{version} Flutter-{sdkVersion} {os} {id}";
    }

    public static string FormatRelayRpcUrl(string protocol, int version, string relayUrl, string sdkVersion, string auth, string? projectId)
    {
        Uri uri = new Uri(relayUrl);
        System.Collections.Specialized.NameValueCollection queryParams = System.Web.HttpUtility.ParseQueryString(uri.Query);

        string ua = FormatUA(protocol, version, sdkVersion);

        Dictionary<string, string> relayParams = new Dictionary<string, string>
    {
        { "auth", auth }
    };

        if (!string.IsNullOrEmpty(projectId))
        {
            relayParams.Add("projectId", projectId);
        }

        relayParams.Add("ua", ua);

        foreach (var param in relayParams)
        {
            queryParams[param.Key] = param.Value;
        }

        Dictionary<string, string> queryParamsDictionary = ConvertNameValueCollectionToDictionary(queryParams);

        UriBuilder uriBuilder = new UriBuilder(uri)
        {
            Query = string.Join("&", queryParamsDictionary.Select(kv => $"{kv.Key}={kv.Value}"))
        };

        return uriBuilder.ToString();
    }

    public static URIParseResult ParseUri(Uri uri)
    {
        string protocol = uri.Scheme;
        string path = uri.AbsolutePath;
        string[] splitParams = path.Split('@');

        if (splitParams.Length == 1)
        {
            throw new WalletConnectError {
                Code = 0, Message = "Invalid URI: Missing @",
            };
        }

        string[] methods = (uri.GetComponents(UriComponents.Query, UriFormat.Unescaped).Split('&')
            .FirstOrDefault(p => p.StartsWith("methods="))?
            .Substring(8) ?? "")
            .Replace("[", "")
            .Replace("]", "")
            .Replace("\"", "")
            .Split(',');

        if (methods.Length == 1 && string.IsNullOrEmpty(methods[0]))
        {
            methods = new string[0];
        }

        URIVersion? version = splitParams[1] switch
        {
            "1" => URIVersion.V1,
            "2" => URIVersion.V2,
            _ => null
        };

        URIV1ParsedData? v1Data = null;
        URIV2ParsedData? v2Data = null;

        if (version == URIVersion.V1)
        {
            v1Data = new URIV1ParsedData(uri.GetComponents(UriComponents.Query, UriFormat.Unescaped).Split('&')
                .FirstOrDefault(p => p.StartsWith("key="))?
                .Substring(4) ?? "",
                uri.GetComponents(UriComponents.Query, UriFormat.Unescaped).Split('&')
                .FirstOrDefault(p => p.StartsWith("bridge="))?
                .Substring(7) ?? "");
        }
        else
        {
            v2Data = new URIV2ParsedData(uri.GetComponents(UriComponents.Query, UriFormat.Unescaped).Split('&')
                .FirstOrDefault(p => p.StartsWith("symKey="))?
                .Substring(7) ?? "",
                new Relay(uri.GetComponents(UriComponents.Query, UriFormat.Unescaped).Split('&')
                    .FirstOrDefault(p => p.StartsWith("relay-protocol="))?
                    .Substring(14) ?? "",
                    uri.GetComponents(UriComponents.Query, UriFormat.Unescaped).Split('&')
                    .FirstOrDefault(p => p.StartsWith("relay-data="))?
                    .Substring(10) ?? null),
                methods);
        }

        URIParseResult ret = new URIParseResult(protocol, version, splitParams[0], v1Data, v2Data);
        return ret;
    }

    public static Dictionary<string, string> FormatRelayParams(Relay relay, string delimiter = "-")
    {
        Dictionary<string, string> paramsDict = new Dictionary<string, string>
        {
            { $"relay{delimiter}protocol", relay.Protocol }
        };

        if (!string.IsNullOrEmpty(relay.Data))
        {
            paramsDict.Add($"relay{delimiter}data", relay.Data);
        }

        return paramsDict;
    }

    public static Uri FormatUri(string protocol, string version, string topic, string symKey, Relay relay, List<List<string>> methods)
    {
        Dictionary<string, string> paramsDict = FormatRelayParams(relay);
        paramsDict.Add("symKey", symKey);

        if (methods != null && methods.Count > 0)
        {
            paramsDict.Add("methods", "[" + string.Join(",", methods.Select(m => $"[{string.Join(",", m)}]")) + "]");
        }
        else
        {
            paramsDict.Add("methods", "[]");
        }

        UriBuilder uriBuilder = new UriBuilder()
        {
            Scheme = protocol,
            Path = $"{topic}@{version}",
            Query = string.Join("&", paramsDict.Select(kv => $"{kv.Key}={kv.Value}"))
        };

        return uriBuilder.Uri;
    }

    public static Dictionary<string, T> ConvertMapTo<T>(Dictionary<string, dynamic> inMap)
    {
        return inMap.ToDictionary(entry => entry.Key, entry => (T)entry.Value);
    }

    public static Dictionary<string, string> ConvertNameValueCollectionToDictionary(System.Collections.Specialized.NameValueCollection nameValueCollection)
    {
        Dictionary<string, string> dictionary = new Dictionary<string, string>();

        foreach (string key in nameValueCollection.AllKeys)
        {
            dictionary[key] = nameValueCollection[key];
        }

        return dictionary;
    }
}

public enum URIVersion
{
    V1,
    V2
}

public class URIV1ParsedData
{
    public string Key { get; }
    public string Bridge { get; }
    public URIV1ParsedData(string key, string bridge)
    {
        Key = key;
        Bridge = bridge;
    }
}

public class URIV2ParsedData
{
    public string SymKey { get; }
    public Relay Relay { get; }
    public string[] Methods { get; }
    public URIV2ParsedData(string symKey, Relay relay, string[] methods)
    {
        SymKey = symKey;
        Relay = relay;
        Methods = methods;
    }
}


public class URIParseResult
{
    public string Protocol { get; }
    public URIVersion? Version { get; }
    public string Topic { get; }
    public URIV1ParsedData? V1Data { get; }
    public URIV2ParsedData? V2Data { get; }
    public URIParseResult(string protocol, URIVersion? version, string topic, URIV1ParsedData? v1Data, URIV2ParsedData? v2Data)
    {
        Protocol = protocol;
        Version = version;
        Topic = topic;
        V1Data = v1Data;
        V2Data = v2Data;
    }

}
