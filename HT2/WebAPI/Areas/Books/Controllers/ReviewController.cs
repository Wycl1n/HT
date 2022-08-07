using AutoMapper;
using BLL.Services.Abstraction;
using Domain.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using WebAPI.Areas.Books.Models;

namespace WebAPI.Areas.Books.Controllers
{
	[Route("api/[area]/{id}/[controller]")]
	[ApiController]
	public class ReviewController: BookControllerBase
	{
		private readonly Lazy<IReviewService> _reviewService;
		private readonly Lazy<IMapper> _mapper;

		public ReviewController(
			Lazy<IReviewService> reviewService,
			Lazy<IMapper> mapper)
		{
			_reviewService = reviewService;
			_mapper = mapper;
		}

		[HttpPut]
		public ActionResult AddReview([FromRoute]int id, [FromBody] ReviewCreateModel reviewModel)
		{
			var review = _mapper.Value.Map<Review>(reviewModel);
			review.BookId = id;
			var reviewId = _reviewService.Value.AddReview(review);

			return Success(reviewId);
		}
	}
}
