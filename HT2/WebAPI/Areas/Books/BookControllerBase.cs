using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Areas.Books
{
	[Route("api/[controller]")]
	[Area("books")]
	[ApiController]
	public class BookControllerBase: BaseController
	{
	}
}
