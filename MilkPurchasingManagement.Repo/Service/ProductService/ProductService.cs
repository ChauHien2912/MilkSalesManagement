using AutoMapper;
using MilkPurchasingManagement.Repo.Dtos.Request.Product;
using MilkPurchasingManagement.Repo.Dtos.Response.Product;
using MilkPurchasingManagement.Repo.Models;
using MilkPurchasingManagement.Repo.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WareHouseManagement.Repository.Specifications;

namespace MilkPurchasingManagement.Repo.Service.ProductService
{
    public class ProductService
    {
        private readonly IUnitOfWork _uow;
        private readonly IMapper _mapper;
        public ProductService(IUnitOfWork uow, IMapper mapper)
        {
            _uow = uow;
            _mapper = mapper;
        }


        public async Task<GetProductResponse> GetProductById(int id)
        {
            var product = await _uow.GetRepository<Product>().SingleOrDefaultAsync(predicate: p => p.Id == id);
            if(product == null)
            {
                throw new Exception("Product not found!");
            }
            return _mapper.Map<GetProductResponse>(product);
        }

        public async Task<IPaginate<GetProductResponse>> GetProducts(int page, int size)
        {
            var products = await _uow.GetRepository<Product>()
            .GetPagingListAsync(               
                page: page,
                size: size
            );

            var voucherResponses = new Paginate<GetProductResponse>()
            {
                Page = products.Page,
                Size = products.Size,
                Total = products.Total,
                TotalPages = products.TotalPages,
                Items = _mapper.Map<IList<GetProductResponse>>(products.Items),
            };
            return voucherResponses;
        }


        public async Task<bool> CreateProduct(CreateProductRequest request)
        {
            var product = new Product {
             Name = request.Name,
             Description = request.Description,
             Price = request.Price,
             Quantity = request.Quantity,
             ExpirationDate = request.ExpirationDate,
             Brand = request.Brand,
             ImgUrl = request.ImgUrl,
             Volume = request.Volume,
             AgeAllowed = request.AgeAllowed            
            };

            await _uow.GetRepository<Product>().InsertAsync(product);
            bool isCreated = await _uow.CommitAsync() > 0;
            return isCreated;

        }

        public async Task<bool> UpdateProduct(int id, UpdateProductRequest request)
        {
            var productexisting = await _uow.GetRepository<Product>().SingleOrDefaultAsync(predicate: e => e.Id == id);
            if(productexisting == null)
            {
                throw new Exception("Product not found!");
            }

            productexisting = _mapper.Map<Product>(request);
            productexisting.Id = id;
            _uow.GetRepository<Product>().UpdateAsync(productexisting);
            bool isUpdate = await _uow.CommitAsync() > 0;
            return isUpdate;
        }

        public async Task<bool> DeleteProduct(int id)
        {
            var productexisting = await _uow.GetRepository<Product>().SingleOrDefaultAsync(predicate: e => e.Id == id);
            if(productexisting == null)
            {
                throw new Exception("Product not found");
            }
            _uow.GetRepository<Product>().DeleteAsync(productexisting);
            bool isDeleted = await _uow.CommitAsync() > 0;
            return isDeleted;


        }
    }
}
