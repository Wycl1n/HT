using HT_1.Models.InputModels;
using HT_1.Services.Abstraction;
using System.Linq;

namespace HT_1.Services.Implementations;

public class TxtParser: Parser
{
	private readonly ILogger _log;

	public TxtParser(ILogger log)
	{
		_log = log;
	}

	public override string FileExtention => ".txt";

	public override List<Transaction> ParseTransactions(
		string path,
		out int parsedLines,
		out int errors)
	{
		using var sr = new StreamReader(path);
		var fileData = sr.ReadToEnd();

		_log.FileRead(path);

		return ParseFileData(fileData, out parsedLines, out errors);
	}
}
