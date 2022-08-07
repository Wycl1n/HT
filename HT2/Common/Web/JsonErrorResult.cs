namespace Common.Web;

public class JsonErrorResult
{
    public string Message { get; set; }
    public object Error { get; set; }
    public ApiErrorTypeEnum ErrorType { get; set; }
}
