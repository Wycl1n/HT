﻿namespace DAL.Models;

public class BookListItem
{
	public int BookId { get; set; }

	public string Title { get; set; }

	[Obsolete("Need for 3rd task")]
	public string Cover { get; set; }

	public string Author { get; set; }

	public double Rating { get; set; }

	public int ReviewsNumber { get; set; }
}