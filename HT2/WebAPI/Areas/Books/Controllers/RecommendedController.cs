using AutoMapper;
using BLL.Services.Abstraction;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace WebAPI.Areas.Books.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class RecommendedController: BaseController
	{
		private readonly Lazy<IBookService> _bookService;
		private readonly Lazy<IMapper> _mapper;

		public RecommendedController(
			Lazy<IBookService> bookService,
			Lazy<IMapper> mapper)
		{
			_bookService = bookService;
			_mapper = mapper;
		}

		[HttpGet]
		public async Task<ActionResult> GetTopTenBooks([FromQuery]string genre)
		{
			var books = await _bookService.Value.GetTopTenBooks(genre);

			return Success(books);
		}
	}
}
