using AutoMapper;
using Hairhub.API.Controllers;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MilkPurchasingManagement.Repo.Dtos.Request.Order;
using MilkPurchasingManagement.Repo.Dtos.Request.User;
using MilkPurchasingManagement.Repo.Dtos.Response;
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
        [HttpPost]
        public async Task<ActionResult<ApiResponse>> Login([FromBody] LoginModel login)
        {
            var response = await _service.Login(login);
            if (!response.Success)
            {
                return Unauthorized(response);
            }
            return Ok(response);
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] SignUpModel registerModel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var response = await _service.Register(registerModel);
            if (!response.Success)
            {
                return BadRequest(response);
            }

            return Ok(response);
        }

        [HttpPost("logout")]
        public IActionResult Logout()
        {
            var response = _service.Logout();
            return Ok(response);
        }
        [HttpPost("changePassword")]
        public async Task<IActionResult> ChangePassword([FromBody] ChangePasswordModel model)
        {
            var response = await _service.ChangePassword(model);
            if (!response.Success)
            {
                return BadRequest(response.Message);
            }

            return Ok(response.Message);
        }
    }
}
