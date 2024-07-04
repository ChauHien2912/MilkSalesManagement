using AutoMapper;
using Hairhub.API.Controllers;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MilkPurchasingManagement.Repo.Dtos.Request.Cart;
using MilkPurchasingManagement.Repo.Service.CartService;
using WareHouseManagement.API.Constant;

namespace MilkPurchasingManagement.Controllers
{
    [Route(APIEndPointConstant.Cart.CartEndpoint+ "/[action]")]
    [ApiController]
    public class CartController : BaseController
    {
        private readonly CartService _cartService;

        public CartController(CartService cartService, IMapper mapper) : base(mapper) 
        {
            _cartService = cartService;
        }

        [HttpGet]
        [Route("{id}")]
        public async Task<IActionResult> GetCartByUserId([FromRoute]int userid, [FromQuery]int page = 1, [FromQuery]int size = 10)
        {
            var result = await _cartService.GetAllCartProduct(userid, page, size);
            return Ok(result);
        }



        [HttpPost]        
        public async Task<IActionResult> CreateCart([FromBody] CreateCartRequest request)
        {
            var result = await _cartService.AddToCart(request);
            return Ok(result);
        }

        [HttpDelete]
        [Route("{id}")]
        public async Task<IActionResult> DeleteCart([FromRoute] int id)
        {
            var result = await _cartService.RemoveFromCart(id);
            return Ok(result);
        }


    }
}
