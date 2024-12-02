namespace MYABackend.Responses;
public class BaseResponse
{
    public bool success {get; set;}
    public bool error {get; set;}
    public int code {get; set;}
    public string message {get; set;}

    public BaseResponse(bool success, int code, string message)
    {
        this.success = success;
        this.error = !success;
        this.code = code;
        this.message = message;
    }
}