using AutoMapper;
using Hairhub.API.Controllers;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MilkPurchasingManagement.Repo.Dtos.Request.Product;
using MilkPurchasingManagement.Repo.Service.ProductService;
using WareHouseManagement.API.Constant;

namespace MilkPurchasingManagement.Controllers
{
    [Route(APIEndPointConstant.Product.ProductEndpoint + "/[action]")]
    [ApiController]
    public class ProductController : BaseController
    {
        private readonly ProductService _service;

        public ProductController(ProductService service, IMapper mapper) : base(mapper)
        {
            _service = service;
        }


        [HttpGet]
        [Route("{id}")]
        public async Task<IActionResult> GetProductById([FromRoute]int id)
        {
            var result = await _service.GetProductById(id);
            if (result == null)
            {
                return BadRequest(result);
            }
            return Ok(result);
        }


        [HttpGet]
        public async Task<IActionResult> GetProducts(int page, int size)
        {
            var result = await _service.GetProducts(page, size);
            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> CreateProdcut([FromForm] CreateProductRequest request)
        {
            try
            {
                var result = await _service.CreateProduct(request);
                if(result == null)
                {
                    return BadRequest();
                }
                return Ok(result);
            } catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }



        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateProduct([FromRoute]int id ,[FromBody]UpdateProductRequest request)
        {
            try
            {
                var result = await _service.UpdateProduct(id, request);
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

        [HttpDelete]
        [Route("{id}")]
        public async Task<IActionResult> DeleteProdcut([FromRoute] int id)
        {
            try
            {
                var result = await _service.DeleteProduct(id);
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
