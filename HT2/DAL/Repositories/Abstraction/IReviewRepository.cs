using DAL.Repositories.Abstraction.Base;
using Domain.Models;

namespace DAL.Repositories.Abstraction;

public interface IReviewRepository: IRepositoryBase<Review>
{
	Review GetById(int id);

	int AddReview(Review review);
}
