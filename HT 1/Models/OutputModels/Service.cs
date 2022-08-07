using Newtonsoft.Json;

namespace HT_1.Models.OutputModels;

public class Service
{
	[JsonProperty(PropertyName = "name")]
	public string Name { get; set; }

	[JsonProperty(PropertyName = "payers")]
	public Payer[] Payers { get; set; }

	[JsonProperty(PropertyName = "total")]
	public int PayersTotal => Payers.Length;
}
