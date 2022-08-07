using DAL.DbContexts;
using DAL.Repositories.Abstraction;
using Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace DAL.Repositories.Implementation;

public class RatingRepository: IRatingRepository
{
	private readonly DbContextBase _dbContext;
	private readonly DbSet<Rating> _ratings;

	public RatingRepository(DbContextBase dbContext)
	{
		_dbContext = dbContext;
		_ratings = dbContext.Set<Rating>();
	}

	public Rating GetById(int id)
	{
		return _ratings.FirstOrDefault(x => x.RatingId == id);
	}

	public IQueryable<Rating> GetAll()
	{
		return _ratings.AsQueryable<Rating>();
	}

	public int AddRate(Rating rateModel)
	{
		_ratings.Add(rateModel);
		_dbContext.SaveChanges();
		return rateModel.RatingId;
	}
}
