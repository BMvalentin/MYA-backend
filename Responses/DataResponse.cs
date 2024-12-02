namespace MYABackend.Responses;
public class DataResponse<T> : BaseResponse
{
    public new T data {get; set;} = default;

    public DataResponse(bool success, int code, string message, T data = default) : base (success, code, message)
    {
        this.data = data;
    }
}