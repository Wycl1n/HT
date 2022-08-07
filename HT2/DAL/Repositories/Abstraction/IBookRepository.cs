using DAL.Models;
using DAL.Repositories.Abstraction.Base;
using Domain.Models;

namespace DAL.Repositories.Abstraction;

public interface IBookRepository: IRepositoryBase<Book>
{
	Book GetById(int id);

	BookModel GetBookModelById(int bookId);

	void DeleteById(int bookId);

	int CreateOrUpdate(BookUpdateModel book);
}
