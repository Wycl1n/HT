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
	public class RateController: BookControllerBase
	{
		private readonly Lazy<IRatingService> _ratingService;
		private readonly Lazy<IMapper> _mapper;

		public RateController(
			Lazy<IRatingService> ratingService,
			Lazy<IMapper> mapper)
		{
			_ratingService = ratingService;
			_mapper = mapper;
		}

		[HttpPut]
		public ActionResult AddRating([FromRoute] int id, [FromBody] RateCreateModel rateModel)
		{
			var rate = _mapper.Value.Map<Rating>(rateModel);
			rate.BookId = id;
			var rateId = _ratingService.Value.AddRate(rate);

			return Success(rateId);
		}
	}
}
