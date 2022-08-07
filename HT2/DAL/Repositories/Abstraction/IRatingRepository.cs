using DAL.Repositories.Abstraction.Base;
using Domain.Models;

namespace DAL.Repositories.Abstraction;

public interface IRatingRepository: IRepositoryBase<Rating>
{
	Rating GetById(int id);

	int AddRate(Rating rateModel);
}
