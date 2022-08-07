using Domain.Models;

namespace BLL.Services.Abstraction;

public interface IReviewService
{
	Review GetReviewById(int id);

	int AddReview(Review review);
}
