namespace HT_1.Services.Abstraction;

public interface IEtlService
{
	public Task OnAddNewFile(string path);

	public void MidnightReport();
}
