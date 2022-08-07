using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Domain.Models;

public class Book
{
	[Key]
	public int BookId { get; set; }

	[Required]
	[MaxLength(100)]
	public string Title { get; set; }

	[Required]
	public string Cover { get; set; }

	[Required]
	[MaxLength(1000)]
	public string Content { get; set; }

	[Required]
	[MaxLength(100)]
	public string Author { get; set; }

	[Required]
	[MaxLength(100)]
	public string Genre { get; set; }


	#region Relations

	[JsonIgnore]
	public IEnumerable<Review> Reviews { get; set; }

	[JsonIgnore]
	public IEnumerable<Rating> Ratings { get; set; }

	#endregion
}
