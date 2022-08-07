namespace HT_1.Services.Templates;

public static class OutputTemplates
{
	public static string MetaLogTemplate =
		"parsed_files: {0}\n" +
		"parsed_lines: {1}\n" +
		"found_errors: {2}\n" +
		"invalid_files: [{3}]\n";

	public static string UnexpercedFileExtention =
		"Unexperced File Extention: {0} ({1})";

	public static string Start =
		"Start: {0}";

	public static string ParserFound =
		"Parser Found: {0}";

	public static string FileRead =
		"File Read: {0}";

	public static string Parsered =
		"Parsered: {0}";

	public static string Grouped =
		"Grouped: {0}";

	public static string Ended =
		"Ended: {0}";

	public static string MidnightReportDone =
		"Midnight Report Done";

	public static string TimeFormat =
		" | Time: {0}";
}
