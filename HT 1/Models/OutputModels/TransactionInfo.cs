using Newtonsoft.Json;

namespace HT_1.Models.OutputModels;

public class TransactionInfo
{
	[JsonProperty(PropertyName = "city")]
	public string City { get; set; }

	[JsonProperty(PropertyName = "services")]
	public Service[] Services { get; set; }

	[JsonProperty(PropertyName = "total")]
	public int ServicesTotal => Services.Length;
}
