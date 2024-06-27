using AutoMapper;
using Hairhub.API.Controllers;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MilkPurchasingManagement.Repo.Dtos.Request.Order;
using MilkPurchasingManagement.Repo.Dtos.Request.Product;
using MilkPurchasingManagement.Repo.Service.OrderService;
using MilkPurchasingManagement.Repo.Service.ProductService;
using WareHouseManagement.API.Constant;

namespace MilkPurchasingManagement.Controllers
{
    [Route(APIEndPointConstant.Order.OrderEndpoint+ "/[action]")]
    [ApiController]
    public class OrderController : BaseController
    {
        private readonly OrderService _service;

        public OrderController(OrderService service, IMapper mapper) : base(mapper)
        {
            _service = service;
        }
        [HttpPost]
        public async Task<IActionResult> CreateOrder([FromBody] CreateOrderRequest request)
        {
            try
            {
                var result = await _service.CreateOrder(request);
                if (result == null)
                {
                    return BadRequest();
                }
                return Ok(result);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
