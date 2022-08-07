namespace HT_1.Services.Abstraction;

public interface ILogger
{
	void Info(string message);

	void Error(string message);

	void UnexpercedFileExtention(string path);

	void Start(string path);
	void ParserFound(string path);
	void FileRead(string path);
	void Parsered(string path);
	void Grouped(string path);
	void Ended(string path);

	void MidnightReportDone();
}
