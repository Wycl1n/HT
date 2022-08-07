using DAL.Repositories.Abstraction;

namespace DAL.UnitOfWorks;

public class UnitOfWork
{
	public readonly Lazy<IBookRepository> Books;
	public readonly Lazy<IRatingRepository> Ratings;
	public readonly Lazy<IReviewRepository> Reviews;

	public UnitOfWork(
		Lazy<IBookRepository> books,
		Lazy<IRatingRepository> ratings,
		Lazy<IReviewRepository> reviews)
	{
		Books = books;
		Ratings = ratings;
		Reviews = reviews;
	}
}
