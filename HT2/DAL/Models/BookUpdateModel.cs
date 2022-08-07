using System.ComponentModel.DataAnnotations;

namespace DAL.Models;

public class BookUpdateModel
{
	public int? BookId { get; set; }

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
}
