using System.ComponentModel.DataAnnotations;

namespace WebAPI.Areas.Books.Models;

public class ReviewCreateModel
{
	[Required]
	[MaxLength(100)]
	public string Message { get; set; }

	[Required]
	[MaxLength(100)]
	public string Reviewer { get; set; }
}
