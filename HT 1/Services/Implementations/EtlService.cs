using HT_1.Models;
using HT_1.Models.InputModels;
using HT_1.Models.OutputModels;
using HT_1.Services.Extentions;
using HT_1.Services.Abstraction;
using HT_1.Services.Templates;
using System.Text;
using Newtonsoft.Json;

namespace HT_1.Services.Implementations;

public class EtlService: IEtlService
{
	private Dictionary<string, Lazy<Parser>> _parses { get; }
	private ILogger _log { get; }

	public EtlService(IEnumerable<Parser> parses, ILogger log)
	{
		_parses = parses.Select(x => new { Key = x.FileExtention, Value = new Lazy<Parser>(() =>x) })
			.ToDictionary(x => x.Key, x => x.Value);
		_log = log;

		ClearData();
	}

	private int _parsedFiles { get; set; }
	private int _parsedLines { get; set; }
	private int _foundErrors { get; set; }
	private List<string> _invalidFiles { get; set; } = new();
	private string TodatDateFormat => "MM-dd-yyyy";

	public Task OnAddNewFile(string path)
	{
		if (!_parses.ContainsKey(path.Substring(path.LastIndexOf('.'))))
		{
			_log.UnexpercedFileExtention(path);
			return Task.CompletedTask;
		}

		_log.Start(path);

		var info = ParseFile(path);
		if (info != null)
			CreateReport(info);

		_log.Ended(path);

		return Task.CompletedTask;
	}

	public void MidnightReport()
	{
		GenerateMetaLog();
		ClearData();
	}

	#region Helpers

	private TransactionInfo[] ParseFile(string path)
	{
		var parser = _parses.FirstOrDefault(x => path.EndsWith(x.Key)).Value;

		if (parser == null)
			throw new Exception("Unexpected file extantion");

		_log.ParserFound(path);

		var transactions = parser.Value.ParseTransactions(
			path,
			out var parsedLines,
			out var errors
		);

		_log.Parsered(path);

		if (transactions == null)
		{
			_invalidFiles.Add(path);
			return null;
		}

		_parsedFiles++;
		_parsedLines += parsedLines;
		_foundErrors += errors;

		var infos = transactions
			.GroupBy(x => x.Address.ToAddress().City)
			.Select(x => new TransactionInfo()
				{
					City = x.Key,
					Services = x.GroupBy(y => y.Service)
						.Select(y => new Service
						{
							Name = y.Key,
							Payers = y.Select(z => new Payer()
							{
								FirstName = z.FirstName,
								LastName = z.LastName,
								Payment = z.Payment,
								AccountNumber = z.AccountNumber
							}).ToArray()
						}).ToArray()
				}).ToArray();

		_log.Grouped(path);

		return infos;
	}

	private void CreateReport(TransactionInfo[] info)
	{
		var directoryPath = @$"{Config.OutputPath}\{DateTime.Now.ToString(TodatDateFormat)}";
		var isExist = Directory.Exists(directoryPath);
		if (!isExist)
			Directory.CreateDirectory(directoryPath);

		using var sw = new StreamWriter($@"{directoryPath}\output{_parsedFiles}.json");
		sw.WriteLine(JsonConvert.SerializeObject(info, Formatting.Indented));
	}

	private void GenerateMetaLog()
	{
		var log = string.Format(
			OutputTemplates.MetaLogTemplate,
			_parsedFiles,
			_parsedLines,
			_foundErrors,
			_invalidFiles.JoinString(", ")
		);

		var directoryPath = @$"{Config.OutputPath}\{DateTime.Now.ToString(TodatDateFormat)}";
		var isExist = Directory.Exists(directoryPath);
		if (!isExist)
			Directory.CreateDirectory(directoryPath);

		using var fs = File.CreateText(@$"{directoryPath}\meta.log");
		fs.WriteLine(log);

		_log.MidnightReportDone();
	}

	private void ClearData()
	{
		_parsedFiles = default;
		_parsedLines = default;
		_foundErrors = default;
		_invalidFiles = new();
	}

	#endregion
}
