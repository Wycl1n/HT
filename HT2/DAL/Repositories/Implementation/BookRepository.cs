using AutoMapper;
using DAL.DbContexts;
using DAL.Models;
using DAL.Repositories.Abstraction;
using Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace DAL.Repositories.Implementation;

public class BookRepository: IBookRepository
{
	private readonly DbContextBase _dbContext;
	private readonly DbSet<Book> _books;
	private readonly Lazy<IMapper> _mapper;

	public BookRepository(
		DbContextBase dbContext,
		Lazy<IMapper> mapper)
	{
		_dbContext = dbContext;
		_books = dbContext.Set<Book>();
		_mapper = mapper;
	}

	public Book GetById(int id)
	{
		return _books.FirstOrDefault(x => x.BookId == id);
	}

	public IQueryable<Book> GetAll()
	{
		return _books.AsQueryable<Book>();
	}

	public BookModel GetBookModelById(int bookId)
	{
		return _books
			.Select(x => new BookModel()
			{
				BookId = x.BookId,
				Title = x.Title,
				Cover = x.Cover,
				Content = x.Content,
				Author = x.Author,
				Genre = x.Genre,
				Raiting = x.Ratings.Any() ? x.Ratings.Average(x => x.Score) : 0,
				Reviews = x.Reviews.Select(r => new ReviewModel()
				{
					ReviewId = r.ReviewId,
					Message = r.Message,
					Reviewer = r.Reviewer
				}).ToList(),
			})
			.FirstOrDefault(x => x.BookId == bookId);
	}

	public void DeleteById(int bookId)
	{
		var bookToRemove = _books.Find(bookId);

		_books.Remove(bookToRemove);
		_dbContext.SaveChanges();
	}

	public int CreateOrUpdate(BookUpdateModel bookModel)
	{
		var source = _mapper.Value.Map<Book>(bookModel);

		if (bookModel.BookId == null)
		{
			bookModel.BookId = 0;
			_books.Add(source);
			_dbContext.SaveChanges();
			return source.BookId;
		}

		var targetBook = _books.FirstOrDefault(x => x.BookId == bookModel.BookId);

		if (targetBook == null)
			throw new ArgumentException("Invalid bookId");

		UpdateBook(source, targetBook);

		return bookModel.BookId.Value;
	}

	private void UpdateBook(Book source, Book target)
	{
		target.Title = source.Title;
		target.Author = source.Author;
		target.Cover = source.Cover;
		target.Content = source.Content;
		target.Genre = source.Genre;

		_dbContext.SaveChanges();
	}
}
