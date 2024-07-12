using AutoMapper;
using Hairhub.API.Controllers;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MilkPurchasingManagement.Repo.Dtos.Request.Review;
using MilkPurchasingManagement.Repo.Service.ReviewService;
using WareHouseManagement.API.Constant;

namespace MilkPurchasingManagement.Controllers
{
    [Route(APIEndPointConstant.Review.ReviewEndpoint+ "/[action]")]
    [ApiController]
    public class ReviewController : BaseController
    {
        private readonly ReviewService _service;

        public ReviewController(ReviewService service, IMapper mapper) : base(mapper) 
        {
            _service = service;
        }

        [HttpGet]
        [Route("{id}")]
        public async Task<IActionResult> GetReviewById([FromRoute]int id)
        {
            var result = await _service.GetReviewById(id);
            return Ok(result);
        }
        [HttpGet]
        [Route("{id}")]
        public async Task<IActionResult> GetReviewByProductId([FromRoute] int id, [FromQuery]int page =1, [FromQuery] int size =10)
        {
            var result = await _service.GetReviewProductId(id, page, size);
            return Ok(result);
        }
        [HttpGet]
        [Route("{id}")]
        public async Task<IActionResult> GetReviewByUserID([FromRoute] int id, [FromQuery] int page = 1, [FromQuery] int size = 10)
        {
            var result = await _service.GetReviewByUserId(id, page, size);
            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> CreateReview([FromBody] CreateReviewRequest request)
        {
            var result = await _service.CreateReview(request);
            return Ok(result);
        }

        [HttpDelete]
        [Route("{id}")]
        public async Task<IActionResult> DeleteReview([FromRoute] int id)
        {
            var result = await _service.Delete(id);
            return Ok(result);
        }

        [HttpPut]
        [Route("{id}")]
        public async Task<IActionResult> UpdateReview([FromRoute]int id,[FromBody] UpdateReview request)
        {
            var result = await _service.UpdateReview(id, request);
            return Ok(result);
        }

    }
}
