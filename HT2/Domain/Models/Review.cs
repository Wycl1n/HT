using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Domain.Models;

public class Review
{
	[Key]
	public int ReviewId { get; set; }

	[Required]
	[MaxLength(100)]
	public string Message { get; set; }

	[Required]
	public int BookId { get; set; }

	[Required]
	[MaxLength(100)]
	public string Reviewer { get; set; }

	#region Relations

	[JsonIgnore]
	public Book Book { get; set; }

	#endregion
}
