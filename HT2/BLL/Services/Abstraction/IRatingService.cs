using Domain.Models;

namespace BLL.Services.Abstraction;

public interface IRatingService
{
	Rating GetRatingById(int id);

	int AddRate(Rating rateModel);
}
