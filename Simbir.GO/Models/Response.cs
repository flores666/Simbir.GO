namespace Simbir.GO.Models;

public class Response
{
    public int StatusCode { get; set; }
    public object? Result { get; set; }

    public Response(int statusCode, object? result)
    {
        StatusCode = statusCode;
        Result = result;
    }
}