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
                var orderDetails = request.OrderDetails; // Get order details from request

                var response = _service.CreateOrder(request, orderDetails);
                if (response == null)
                {
                    return BadRequest();
                }
                return Ok(response);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        [HttpGet]
        public async Task<IActionResult> GetAllOrders()
        {
            try
            {
                var response = await _service.GetAllOrdersAsync();
                if (response == null)
                {
                    return BadRequest();
                }
                return Ok(response);
            }
            catch (Exception ex)
            {
                // You might want to log the exception here
                throw new Exception(ex.Message);
            }
        }
         [HttpPost("update-status")]
    public async Task<IActionResult> UpdateOrderStatus([FromBody] OrderStatusUpdateRequestModel orderStatusUpdateDto)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        var response = await _service.AutoChangeOrderStatusAsync(orderStatusUpdateDto);
        
        if (response.Success)
        {
            return Ok(response);
        }
        else
        {
            return BadRequest(response);
        }
    }
        [HttpGet("{orderId}")]
        public async Task<IActionResult> GetOrderById(int orderId)
        {
            var response = await _service.GetOrderByIdAsync(orderId);

            if (response.Success)
            {
                return Ok(response);
            }
            else
            {
                return NotFound(response);
            }
        }
    }
}
