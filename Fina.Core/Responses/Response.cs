using System.Text.Json.Serialization;

namespace Fina.Core.Responses;

public class Response<T>(T? data, int code = Configuration.DefaultStatusCode, string? message = null)
{

    [JsonConstructor]
    public Response() : this(default(T?), Configuration.DefaultStatusCode, null)
    {
    }

    public T? Data { get; set; } = data;
    public string? Message { get; set; } = message;

    [JsonIgnore]
    public bool IsSuccess => code is >= 200 and <= 299;
}
 