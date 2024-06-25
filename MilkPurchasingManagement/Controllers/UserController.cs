using AutoMapper;
using Hairhub.API.Controllers;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MilkPurchasingManagement.Repo.Dtos.Request.Order;
using MilkPurchasingManagement.Repo.Service.OrderService;
using MilkPurchasingManagement.Repo.Service.UserService;
using WareHouseManagement.API.Constant;

namespace MilkPurchasingManagement.Controllers
{
    [Route(APIEndPointConstant.User.UserEndpoint+ "/[action]")]
    [ApiController]
    public class UserController : BaseController
    {
        private readonly UserService _service;

        public UserController(UserService service, IMapper mapper) : base(mapper)
        {
            _service = service;
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetUserbyId([FromRoute]int id)
        {
            try
            {
                var result = await _service.GetUserByID(id);
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
