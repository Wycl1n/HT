using HT_1.Models.InputModels;
using HT_1.Services.Abstraction;

namespace HT_1.Services.Implementations;

public class CsvParser: Parser
{
	private readonly ILogger _log;

	public CsvParser(ILogger log)
	{
		_log = log;
	}

	public override string FileExtention => ".csv";

	public override List<Transaction> ParseTransactions(
		string path,
		out int parsedLines,
		out int errors)
	{
		using var sr = new StreamReader(path);
		var fileData = sr.ReadToEnd();

		_log.FileRead(path);

		return ParseFileData(fileData, out parsedLines, out errors, separator: ";");
	}
}
