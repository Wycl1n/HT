using System.Collections.Generic;

namespace WebAPI.Areas.Books.Models;

public class BookInfoModel
{
	public int BookId { get; set; }

	public string Title { get; set; }

	public string Cover { get; set; }

	public string Content { get; set; }

	public string Author { get; set; }

	public string Genre { get; set; }

	public double Raiting { get; set; }

	public List<ReviewInfoModel> Reviews { get; set; }
}
