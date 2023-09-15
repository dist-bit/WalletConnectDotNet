public class NamespaceUtils
{
    public static bool IsValidChainId(string value)
    {
        if (value.Contains(":"))
        {
            string[] split = value.Split(':');
            return split.Length == 2;
        }
        return false;
    }

    public static bool IsValidAccount(string value)
    {
        if (value.Contains(":"))
        {
            string[] split = value.Split(':');
            if (split.Length == 3)
            {
                string chainId = $"{split[0]}:{split[1]}";
                return split.Length >= 2 && IsValidChainId(chainId);
            }
        }
        return false;
    }

    public static bool IsValidUrl(string value)
    {
        try
        {
            new Uri(value);
            return true;
        }
        catch (UriFormatException)
        {
            return false;
        }
    }


    public static string GetAccount(string namespaceAccount)
    {
        if (IsValidAccount(namespaceAccount))
        {
            return namespaceAccount.Split(':')[2];
        }
        return namespaceAccount;
    }

    public static string GetChainFromAccount(string account)
    {
        if (IsValidAccount(account))
        {
            string[] parts = account.Split(':');
            string namespaceName = parts[0];
            string reference = parts[1];
            return $"{namespaceName}:{reference}";
        }
        return account;
    }

    public static List<string> GetChainsFromAccounts(List<string> accounts)
    {
        HashSet<string> chains = new HashSet<string>();
        foreach (var account in accounts)
        {
            chains.Add(GetChainFromAccount(account));
        }

        return new List<string>(chains);
    }

    public static string GetNamespaceFromChain(string chainId)
    {
        if (IsValidChainId(chainId))
        {
            return chainId.Split(':')[0];
        }
        return chainId;
    }

    public static List<string> GetNamespacesFromAccounts(List<string> accounts)
    {
        HashSet<string> namespaces = new HashSet<string>();
        foreach (var account in accounts)
        {
            namespaces.Add(GetChainFromAccount(account));
        }

        return new List<string>(namespaces);
    }

    public static List<string> GetChainIdsFromNamespace(string nsOrChainId, Namespace namespaceObj)
    {
        if (IsValidChainId(nsOrChainId))
        {
            return new List<string> { nsOrChainId };
        }

        return GetChainsFromAccounts(namespaceObj.Accounts);
    }

    public static List<string> GetChainIdsFromNamespaces(Dictionary<string, Namespace> namespaces)
    {
        HashSet<string> chainIds = new HashSet<string>();

        foreach (var ns in namespaces)
        {
            chainIds.UnionWith(GetChainIdsFromNamespace(ns.Key, ns.Value));
        }

        return new List<string>(chainIds);
    }

    public static List<string> GetNamespacesMethodsForChainId(string chainId, Dictionary<string, Namespace> namespaces)
    {
        List<string> methods = new List<string>();
        foreach (var nsOrChain in namespaces)
        {
            if (nsOrChain.Key == chainId)
            {
                methods.AddRange(nsOrChain.Value.Methods);
            }
            else
            {
                List<string> chains = GetChainsFromAccounts(nsOrChain.Value.Accounts);
                if (chains.Contains(chainId))
                {
                    methods.AddRange(nsOrChain.Value.Methods);
                }
            }
        }

        return methods;
    }

    public static List<string> GetNamespacesEventsForChain(string chainId, Dictionary<string, Namespace> namespaces)
    {
        List<string> events = new List<string>();
        foreach (var nsOrChain in namespaces)
        {
            if (nsOrChain.Key == chainId)
            {
                events.AddRange(nsOrChain.Value.Events);
            }
            else
            {
                List<string> chains = GetChainsFromAccounts(nsOrChain.Value.Accounts);
                if (chains.Contains(chainId))
                {
                    events.AddRange(nsOrChain.Value.Events);
                }
            }
        }

        return events;
    }


    public static List<string> GetChainsFromRequiredNamespace(string nsOrChainId, RequiredNamespace requiredNamespace)
    {
        List<string> chains = new List<string>();
        if (IsValidChainId(nsOrChainId))
        {
            chains.Add(nsOrChainId);
        }
        else if (requiredNamespace.Chains != null)
        {
            // Suponemos que el espacio de nombres es una cadena
            // Valida el espacio de nombres requerido antes de enviarlo aquí
            chains.AddRange(requiredNamespace.Chains);
        }

        return chains;
    }

    public static List<string> GetChainIdsFromRequiredNamespaces(Dictionary<string, RequiredNamespace> requiredNamespaces)
    {
        HashSet<string> chainIds = new HashSet<string>();

        foreach (var ns in requiredNamespaces)
        {
            chainIds.UnionWith(GetChainsFromRequiredNamespace(ns.Key, ns.Value));
        }

        return new List<string>(chainIds);
    }

    public static Dictionary<string, Namespace> ConstructNamespaces(
        HashSet<string> availableAccounts,
        HashSet<string> availableMethods,
        HashSet<string> availableEvents,
        Dictionary<string, RequiredNamespace> requiredNamespaces,
        Dictionary<string, RequiredNamespace> optionalNamespaces = null)
    {
        Dictionary<string, Namespace> namespaces = ConstructNamespacesFromRequired(
            availableAccounts,
            availableMethods,
            availableEvents,
            requiredNamespaces
        );

        Dictionary<string, Namespace> optional = optionalNamespaces == null
            ? new Dictionary<string, Namespace>()
            : ConstructNamespacesFromRequired(
                availableAccounts,
                availableMethods,
                availableEvents,
                optionalNamespaces
            );

        List<string> keys = new List<string>(optional.Keys);
        for (int i = 0; i < keys.Count; i++)
        {
            string key = keys[i];
            if (namespaces.ContainsKey(key))
            {
                namespaces[key] = new Namespace
                {
                    Accounts = new List<string>(
                        new HashSet<string>(namespaces[key].Accounts)
                        .Union(new HashSet<string>(optional[key].Accounts))
                    ),
                    Methods = new List<string>(
                        new HashSet<string>(namespaces[key].Methods)
                        .Union(new HashSet<string>(optional[key].Methods))
                    ),
                    Events = new List<string>(
                        new HashSet<string>(namespaces[key].Events)
                        .Union(new HashSet<string>(optional[key].Events))
                    )
                };
                optional.Remove(key);
            }
        }

        return new Dictionary<string, Namespace>(namespaces.Concat(optional));
    }

    private static HashSet<string> GetMatching(
        string namespaceOrChainId,
        HashSet<string> available,
        HashSet<string> requested = null,
        bool takeLast = true)
    {
        HashSet<string> matching = new HashSet<string>();
        foreach (var item in available)
        {
            if (item.StartsWith($"{namespaceOrChainId}:"))
            {
                matching.Add(takeLast ? item.Split(':')[^1] : item);
            }
        }

        if (requested != null)
        {
            matching.IntersectWith(requested);
        }

        return matching;
    }

    private static Dictionary<string, Namespace> ConstructNamespacesFromRequired(
        HashSet<string> availableAccounts,
        HashSet<string> availableMethods,
        HashSet<string> availableEvents,
        Dictionary<string, RequiredNamespace> requiredNamespaces)
    {
        Dictionary<string, Namespace> namespaces = new Dictionary<string, Namespace>();

        foreach (var namespaceOrChainId in requiredNamespaces.Keys)
        {
            List<string> accounts = new List<string>();
            List<string> events = new List<string>();
            List<string> methods = new List<string>();
            var namespaceObj = requiredNamespaces[namespaceOrChainId];
            if (IsValidChainId(namespaceOrChainId) ||
                namespaceObj.Chains == null ||
                namespaceObj.Chains.Count == 0)
            {
                accounts.AddRange(
                    GetMatching(namespaceOrChainId, availableAccounts, takeLast: false)
                );
                events.AddRange(
                    GetMatching(namespaceOrChainId, availableEvents, new HashSet<string>(namespaceObj.Events))
                );
                methods.AddRange(
                    GetMatching(namespaceOrChainId, availableMethods, new HashSet<string>(namespaceObj.Methods))
                );
            }
            else
            {
                List<string> chains = namespaceObj.Chains;
                List<HashSet<string>> chainMethodSets = new List<HashSet<string>>();
                List<HashSet<string>> chainEventSets = new List<HashSet<string>>();

                foreach (var chainId in chains)
                {

                    accounts.AddRange(GetMatching(chainId, availableAccounts).Select(e => $"{chainId}:{e}").ToList()
);
                    chainEventSets.Add(
                        GetMatching(chainId, availableEvents, new HashSet<string>(namespaceObj.Events))
                    );
                    chainMethodSets.Add(
                        GetMatching(chainId, availableMethods, new HashSet<string>(namespaceObj.Methods))
                    );
                }


                methods.AddRange(chainMethodSets.Aggregate((v, e) => new HashSet<string>(v.Intersect(e))).ToList());
                events.AddRange(chainMethodSets.Aggregate((v, e) => new HashSet<string>(v.Intersect(e))).ToList());
            }

            namespaces[namespaceOrChainId] = new Namespace
            {
                Accounts = accounts,
                Events = events,
                Methods = methods
            };
        }

        return namespaces;
    }
}

