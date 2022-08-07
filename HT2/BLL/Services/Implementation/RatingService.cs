using BLL.Services.Abstraction;
using DAL.UnitOfWorks;
using Domain.Models;

namespace BLL.Services.Implementation;

public class RatingService: IRatingService
{
	private readonly UnitOfWork unitOfWork;

	public RatingService(UnitOfWork unitOfWork)
	{
		this.unitOfWork = unitOfWork;
	}

	public Rating GetRatingById(int id)
	{
		return unitOfWork.Ratings.Value.GetById(id);
	}

	public int AddRate(Rating rateModel)
	{
		return unitOfWork.Ratings.Value.AddRate(rateModel);
	}
}
