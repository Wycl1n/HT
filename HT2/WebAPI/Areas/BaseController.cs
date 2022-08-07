using Common.Web;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Areas;

[ApiController]
public class BaseController: ControllerBase
{

	internal JsonResult Success() => JsonResultData.Success();
	internal JsonResult Success<T>(T data) => JsonResultData<T>.Success(data);
	internal JsonResult Failure(string message = null, object data = null, ApiErrorTypeEnum errorType = ApiErrorTypeEnum.Unknown) => JsonResultData<object>.Failure(message, data, errorType);
	internal JsonResult Failure(ApiErrorTypeEnum errorType) => JsonResultData<object>.Failure(null, null, errorType);

}
