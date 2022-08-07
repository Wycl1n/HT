using DAL.DbContexts;
using DAL.Repositories.Abstraction;
using Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace DAL.Repositories.Implementation;

public class ReviewRepository: IReviewRepository
{
	private readonly DbContextBase _dbContext;
	private readonly DbSet<Review> _reviews;

	public ReviewRepository(DbContextBase dbContext)
	{
		_dbContext = dbContext;
		_reviews = dbContext.Set<Review>();
	}

	public Review GetById(int id)
	{
		return _reviews.FirstOrDefault(x => x.ReviewId == id);
	}

	public IQueryable<Review> GetAll()
	{
		return _reviews.AsQueryable<Review>();
	}

	public int AddReview(Review review)
	{
		_reviews.Add(review);
		_dbContext.SaveChanges();
		return review.ReviewId;
	}
}
