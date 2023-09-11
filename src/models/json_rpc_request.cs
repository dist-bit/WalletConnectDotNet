using Newtonsoft.Json;

public class JsonRpcRequest
{
    public int Id { get; set; }
    public string JsonRpc { get; set; } = "2.0";

    [JsonProperty("params")]
    public dynamic Parameters { get; set; }
    public string Method { get; set; }

    // Constructor
    public JsonRpcRequest(int id, string method, dynamic parameters)
    {
        Id = id;
        Method = method;
        Parameters = parameters;
    }

    public static JsonRpcRequest? FromJson(string json)
    {
         return JsonConvert.DeserializeObject<JsonRpcRequest>(json);
    }
}