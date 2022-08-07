using AutoMapper;
using BLL;
using BLL.Services.Abstraction;
using DAL.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using System.Threading.Tasks;
using WebAPI.Areas.Books.Models;
using WebAPI.Infrastructure.Enums;

namespace WebAPI.Areas.Books.Controllers
{
	[Route("api/[area]")]
	[ApiController]
	public class BookController: BookControllerBase
	{
		private readonly Lazy<IBookService> _bookService;
		private readonly Lazy<IMapper> _mapper;

		public BookController(
			Lazy<IBookService> bookService,
			Lazy<IMapper> mapper)
		{
			_bookService = bookService;
			_mapper = mapper;
		}

		[HttpGet]
		public async Task<ActionResult> GetAllBooks([FromRoute] OrderBooksByEnum? orderBy = default)
		{
			var books = await _bookService.Value.GetOrderredBooks(orderBy);

			return Success(books);
		}

		[HttpGet("{id}")]
		public ActionResult GetBookModelById([FromRoute] int id)
		{
			var model = _bookService.Value.GetBookModelById(id);

			return Success(_mapper.Value.Map<BookInfoModel>(model));
		}

		[HttpDelete("{id}")]
		public ActionResult DeleteBook(int id, [FromQuery] string secret)
		{
			if (Config.SecretKey != secret)
				return Forbid();

			_bookService.Value.DeleteBook(id);
			return Success();
		}

		[HttpPost("save")]
		public ActionResult CreateOrUpdateBook([FromBody] BookUpdateModel bookModel)
		{
			var id = _bookService.Value.CreateOrUpdate(bookModel);
			return Success(id);
		}
	}
}
