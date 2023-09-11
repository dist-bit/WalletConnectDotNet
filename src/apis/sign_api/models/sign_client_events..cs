public class SessionProposalEvent : EventArgs
{
    public int Id { get; }
    public ProposalData Params { get; }

    public SessionProposalEvent(int id, ProposalData parameters)
    {
        Id = id;
        Params = parameters;
    }

    public SessionProposalEvent(SessionProposal proposal)
    {
        Id = proposal.Id;
        Params = proposal.Parameters;
    }

    public override string ToString()
    {
        return $"SessionProposalEvent(id: {Id}, parameters: {Params})";
    }
}

public class SessionProposalErrorEvent : EventArgs
{
    public int Id { get; }
    public Dictionary<string, RequiredNamespace> RequiredNamespaces { get; }
    public Dictionary<string, Namespace> Namespaces { get; }
    public WalletConnectError Error { get; }

    public SessionProposalErrorEvent(
        int id,
        Dictionary<string, RequiredNamespace> requiredNamespaces,
        Dictionary<string, Namespace> namespaces,
        WalletConnectError error)
    {
        Id = id;
        RequiredNamespaces = requiredNamespaces;
        Namespaces = namespaces;
        Error = error;
    }

    public override string ToString()
    {
        return $"SessionProposalErrorEvent(id: {Id}, requiredNamespaces: {RequiredNamespaces}, namespaces: {Namespaces}, error: {Error})";
    }
}

public class SessionConnect : EventArgs
{
    public SessionData Session { get; }

    public SessionConnect(SessionData session)
    {
        Session = session;
    }

    public override string ToString()
    {
        return $"SessionConnect(session: {Session})";
    }
}

public class SessionUpdate : EventArgs
{
    public int Id { get; }
    public string Topic { get; }
    public Dictionary<string, Namespace> Namespaces { get; }

    public SessionUpdate(int id, string topic, Dictionary<string, Namespace> namespaces)
    {
        Id = id;
        Topic = topic;
        Namespaces = namespaces;
    }

    public override string ToString()
    {
        return $"SessionUpdate(id: {Id}, topic: {Topic}, namespaces: {Namespaces})";
    }
}

public class SessionExtend : EventArgs
{
    public int Id { get; }
    public string Topic { get; }

    public SessionExtend(int id, string topic)
    {
        Id = id;
        Topic = topic;
    }

    public override string ToString()
    {
        return $"SessionExtend(id: {Id}, topic: {Topic})";
    }
}

public class SessionPing : EventArgs
{
    public int Id { get; }
    public string Topic { get; }

    public SessionPing(int id, string topic)
    {
        Id = id;
        Topic = topic;
    }

    public override string ToString()
    {
        return $"SessionPing(id: {Id}, topic: {Topic})";
    }
}

public class SessionDelete : EventArgs
{
    public string Topic { get; }
    public int? Id { get; }

    public SessionDelete(string topic, int? id = null)
    {
        Topic = topic;
        Id = id;
    }

    public override string ToString()
    {
        return $"SessionDelete(topic: {Topic}, id: {Id})";
    }
}

public class SessionExpire : EventArgs
{
    public string Topic { get; }

    public SessionExpire(string topic)
    {
        Topic = topic;
    }

    public override string ToString()
    {
        return $"SessionExpire(topic: {Topic})";
    }
}

public class SessionRequestEvent : EventArgs
{
    public int Id { get; }
    public string Topic { get; }
    public string Method { get; }
    public string ChainId { get; }
    public dynamic Params { get; }

    public SessionRequestEvent(
        int id,
        string topic,
        string method,
        string chainId,
        dynamic @params)
    {
        Id = id;
        Topic = topic;
        Method = method;
        ChainId = chainId;
        Params = @params;
    }

    public static SessionRequestEvent FromSessionRequest(SessionRequest request)
    {
        return new SessionRequestEvent(
            request.Id,
            request.Topic,
            request.Method,
            request.ChainId,
            request.Params
        );
    }

    public override string ToString()
    {
        return $"SessionRequestEvent(id: {Id}, topic: {Topic}, method: {Method}, chainId: {ChainId}, params: {Params})";
    }
}

public class SessionEvent : EventArgs
{
    public int Id { get; }
    public string Topic { get; }
    public string Name { get; }
    public string ChainId { get; }
    public dynamic Data { get; }

    public SessionEvent(
        int id,
        string topic,
        string name,
        string chainId,
        dynamic data)
    {
        Id = id;
        Topic = topic;
        Name = name;
        ChainId = chainId;
        Data = data;
    }

    public override string ToString()
    {
        return $"SessionEvent(id: {Id}, topic: {Topic}, name: {Name}, chainId: {ChainId}, data: {Data})";
    }
}

public class ProposalExpire : EventArgs
{
    public int Id { get; }

    public ProposalExpire(int id)
    {
        Id = id;
    }

    public override string ToString()
    {
        return $"ProposalExpire(id: {Id})";
    }
}