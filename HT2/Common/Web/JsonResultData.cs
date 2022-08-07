using Microsoft.AspNetCore.Mvc;

namespace Common.Web;

public class JsonResultData<T>
{
    public JsonResultData(T data)
    {
        Data = data;
    }

    public JsonResultData(JsonErrorResult error)
    {
        IsSuccess = false;
        Error = error;
    }

    public bool IsSuccess { get; set; } = true;

    public T Data { get; set; }

    public JsonErrorResult Error { get; set; }

    public static JsonResult Success(T data)
    {
        return new JsonResult(new JsonResultData<T>(data));
    }

    public static JsonResult Success(object data = null)
    {
        return new JsonResult(new JsonResultData<object>(data ?? new { }));
    }

    public static JsonResult Failure(string message = null, object errorData = null, ApiErrorTypeEnum errorType = ApiErrorTypeEnum.Unknown)
    {
        var data = new JsonResultData<JsonErrorResult>(new JsonErrorResult
        {
            Message = message,
            Error = errorData,
            ErrorType = errorType
        });

        return new JsonResult(data);
    }
}

public class JsonResultData : JsonResultData<object>
{
    public JsonResultData(object data) : base(data) { }

    public JsonResultData(JsonErrorResult error) : base(error) { }
}
