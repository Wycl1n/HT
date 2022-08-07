using System.ComponentModel.DataAnnotations;

namespace WebAPI.Areas.Books.Models;

public class RateCreateModel
{
	[Required]
	[Range(0, double.PositiveInfinity)]
	public double Score { get; set; }
}
