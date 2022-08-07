using DAL.Models;
using Domain.Models;
using WebAPI.Infrastructure.Enums;

namespace BLL.Services.Abstraction;

public interface IBookService
{
	Book GetBookById(int id);

	Task<List<BookListItem>> GetOrderredBooks(OrderBooksByEnum? orderBy = null);

	Task<List<BookListItem>> GetTopTenBooks(string genre);

	BookModel GetBookModelById(int bookId);

	void DeleteBook(int bookId);

	int CreateOrUpdate(BookUpdateModel book);
}
