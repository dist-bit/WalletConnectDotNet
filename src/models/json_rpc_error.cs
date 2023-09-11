using Newtonsoft.Json;

public class JsonRpcError
{
    public int? Code { get; set; }
    public string? Message { get; set; }

    public static JsonRpcError? FromJson(string json)
    {
        return JsonConvert.DeserializeObject<JsonRpcError>(json);
    }

    public static JsonRpcError ServerError(string message)
    {
        return new JsonRpcError { Code = -32000, Message = message };
    }

    public static JsonRpcError InvalidParams(string message)
    {
        return new JsonRpcError { Code = -32602, Message = message };
    }

    public static JsonRpcError InvalidRequest(string message)
    {
        return new JsonRpcError { Code = -32600, Message = message };
    }

    public static JsonRpcError ParseError(string message)
    {
        return new JsonRpcError { Code = -32700, Message = message };
    }

    public static JsonRpcError MethodNotFound(string message)
    {
        return new JsonRpcError { Code = -32601, Message = message };
    }

    public override string ToString()
    {
        return $"JsonRpcError(code: {Code}, message: {Message})";
    }
}