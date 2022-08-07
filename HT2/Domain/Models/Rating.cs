using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Domain.Models;

public class Rating
{
	[Key]
	public int RatingId { get; set; }

	[Required]
	public int BookId { get; set; }

	[Required]
	[Range(0, double.PositiveInfinity)]
	public double Score { get; set; }


	#region Relations

	[JsonIgnore]
	public Book Book { get; set; }

	#endregion
}
