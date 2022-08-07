using AutoMapper;
using DAL.Models;
using Domain.Models;
using WebAPI.Areas.Books.Models;

namespace WebAPI.Infrastructure.AutoMapper;

public class ReviewProfile: Profile
{
	public ReviewProfile()
	{
		CreateMap<ReviewModel, ReviewInfoModel>()
			.ReverseMap();

		CreateMap<Review, ReviewCreateModel>()
			.ReverseMap();
	}
}
