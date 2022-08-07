using AutoMapper;
using Domain.Models;
using WebAPI.Areas.Books.Models;

namespace WebAPI.Infrastructure.AutoMapper;

public class RatingProfile: Profile
{
	public RatingProfile()
	{
		CreateMap<Rating, RateCreateModel>()
			.ReverseMap();
	}
}
