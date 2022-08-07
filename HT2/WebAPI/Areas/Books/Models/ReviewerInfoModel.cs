namespace WebAPI.Areas.Books.Models;

public class ReviewInfoModel
{
	public int BookId { get; set; }

	public string Reviewer { get; set; }

	public string Message { get; set; }
}