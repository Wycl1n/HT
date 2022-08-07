using HT_1.Services.Abstraction;
using HT_1.Services.Templates;

namespace HT_1.Services.Implementations;

public class ConsoleLogger: ILogger
{
	public void Info(string message)
	{
		Console.ForegroundColor = ConsoleColor.White;
		Console.WriteLine();
		Console.WriteLine(message);
	}

	public void Error(string message)
	{
		Console.ForegroundColor = ConsoleColor.Red;
		Console.WriteLine();
		Console.WriteLine(message);
	}

	public void UnexpercedFileExtention(string path)
	{
		Error(string.Format(OutputTemplates.UnexpercedFileExtention, path.Substring(path.LastIndexOf('.')), path) + AddTime());
	}

	public void Start(string path)
	{
		Info(string.Format(OutputTemplates.Start, path) + AddTime());
	}

	public void ParserFound(string path)
	{
		Info(string.Format(OutputTemplates.ParserFound, path) + AddTime());
	}

	public void FileRead(string path)
	{
		Info(string.Format(OutputTemplates.FileRead, path) + AddTime());
	}

	public void Parsered(string path)
	{
		Info(string.Format(OutputTemplates.Parsered, path) + AddTime());
	}

	public void Grouped(string path)
	{
		Info(string.Format(OutputTemplates.Grouped, path) + AddTime());
	}

	public void Ended(string path)
	{
		Info(string.Format(OutputTemplates.Ended, path) + AddTime());
	}

	public void MidnightReportDone()
	{
		Info(OutputTemplates.MidnightReportDone + AddTime());
	}

	private string AddTime()
	{
		return string.Format(OutputTemplates.TimeFormat, DateTime.Now.ToLongTimeString());
	}
}
