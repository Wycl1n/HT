using AutoMapper;
using BLL.Services.Abstraction;
using DAL.Models;
using DAL.UnitOfWorks;
using Domain.Models;
using Microsoft.EntityFrameworkCore;
using WebAPI.Infrastructure.Enums;

namespace BLL.Services.Implementation;

public class BookService: IBookService
{
	private readonly UnitOfWork unitOfWork;
	private readonly Lazy<IMapper> _mapper;

	public BookService(
		UnitOfWork unitOfWork,
		Lazy<IMapper> mapper)
	{
		this.unitOfWork = unitOfWork;
		_mapper = mapper;
	}

	public Book GetBookById(int id)
	{
		return unitOfWork.Books.Value.GetById(id);
	}

	public async Task<List<BookListItem>> GetOrderredBooks(OrderBooksByEnum? orderBy = null)
	{
		var task = Task.Factory.StartNew(() =>
		{
			IQueryable<Book> query = unitOfWork.Books.Value.GetAll()
				.Include(x => x.Reviews)
				.Include(x => x.Ratings);

			switch (orderBy)
			{
				case OrderBooksByEnum.Title:
					query = query.OrderBy(x => x.Title);
					break;
				case OrderBooksByEnum.Author:
					query = query.OrderBy(x => x.Author);
					break;
			}

			return query
				.Select(x => new BookListItem()
				{
					BookId = x.BookId,
					Title = x.Title,
					Author = x.Author,
					Cover = x.Cover,
					Rating = x.Ratings.Any() ? x.Ratings.Average(x => x.Score) : 0,
					ReviewsNumber = x.Reviews.Any() ? x.Reviews.Count() : 0
				})
				.ToList();
		});

		return await task;
	}

	public async Task<List<BookListItem>> GetTopTenBooks(string genre)
	{
		var task = Task.Factory.StartNew(() =>
		{
			IQueryable<Book> query = unitOfWork.Books.Value.GetAll()
				.Include(x => x.Reviews)
				.Include(x => x.Ratings);

			if (!string.IsNullOrEmpty(genre))
				query = query.Where(x => x.Genre == genre);

			return query.Where(x => x.Ratings.Average(x => x.Score) > 10)
				.Take(10)
				.Select(x => new BookListItem()
				{
					BookId = x.BookId,
					Title = x.Title,
					Author = x.Author,
					Cover = x.Cover,
					Rating = x.Ratings.Any() ? x.Ratings.Average(x => x.Score) : 0,
					ReviewsNumber = x.Reviews.Any() ? x.Reviews.Count() : 0
				})
				.ToList();
		});

		return await task;
	}

	public BookModel GetBookModelById(int bookId)
	{
		return unitOfWork.Books.Value.GetBookModelById(bookId);
	}

	public void DeleteBook(int bookId)
	{
		unitOfWork.Books.Value.DeleteById(bookId);
	}

	public int CreateOrUpdate(BookUpdateModel book)
	{
		return unitOfWork.Books.Value.CreateOrUpdate(book);
	}
}
