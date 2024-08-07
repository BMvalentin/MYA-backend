namespace MYABackend.Responses;
public class DataResponse<T> : BaseResponse
{
    public new T data {get; set;} = default;

    public DataResponse(bool succes, int code, string message, T data = default) : base (succes, code, message)
    {
        this.data = data;
    }
}