using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using WareHouseManagement.API.Constant;

namespace Hairhub.API.Controllers
{
    [Route(APIEndPointConstant.ApiEndpoint)]
    [ApiController]
    
    public class BaseController: ControllerBase
    {
        protected readonly IMapper _mapper;

        public BaseController(IMapper mapper)
        {
            _mapper = mapper;
        }
    }
}
