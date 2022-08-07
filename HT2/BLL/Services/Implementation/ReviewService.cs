using BLL.Services.Abstraction;
using DAL.UnitOfWorks;
using Domain.Models;

namespace BLL.Services.Implementation;

public class ReviewService: IReviewService
{
	private readonly UnitOfWork unitOfWork;

	public ReviewService(UnitOfWork unitOfWork)
	{
		this.unitOfWork = unitOfWork;
	}

	public Review GetReviewById(int id)
	{
		return unitOfWork.Reviews.Value.GetById(id);
	}

	public int AddReview(Review review)
	{
		return unitOfWork.Reviews.Value.AddReview(review);
	}
}
