namespace DAL.Repositories.Abstraction.Base;

public interface IRepositoryBase<T>
{
	IQueryable<T> GetAll();
}
