using AutoMapper;
using DAL.Models;
using Domain.Models;
using System.Linq;
using WebAPI.Areas.Books.Models;

namespace WebAPI.Infrastructure.AutoMapper;

public class BookProfile: Profile
{
	public BookProfile()
	{
		CreateMap<BookModel, BookInfoModel>()
			.ReverseMap();

		CreateMap<Book, BookListItem>()
			.ReverseMap();

		CreateMap<Book, BookUpdateModel>()
			.ReverseMap();
	}
}
