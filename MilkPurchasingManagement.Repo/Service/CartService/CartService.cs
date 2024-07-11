using AutoMapper;
using MilkPurchasingManagement.Repo.Dtos.Request.Cart;
using MilkPurchasingManagement.Repo.Dtos.Response;
using MilkPurchasingManagement.Repo.Dtos.Response.Cart;
using MilkPurchasingManagement.Repo.Models;
using MilkPurchasingManagement.Repo.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WareHouseManagement.Repository.Specifications;

namespace MilkPurchasingManagement.Repo.Service.CartService
{
    public class CartService
    {
        private readonly IUnitOfWork _uow;
        private readonly IMapper _mapper;
        public CartService(IUnitOfWork uow, IMapper mapper)
        {
            _uow = uow;
            _mapper = mapper;
        }
        public async Task<bool> AddToCart(CreateCartRequest request)
        {
            var product = await _uow.GetRepository<Product>().SingleOrDefaultAsync(predicate: p => p.Id == request.ProductId); 
            if (product == null)
            {
                return false;
            }
            var existingCart = await _uow.GetRepository<Cart>().SingleOrDefaultAsync(predicate: p => p.ProductId ==  request.ProductId && p.UserId == request.UserId);
            if (existingCart != null)
            {
                return false;
            }

            var newCart = new Cart
            {
                Quantity = request.Quantity,
                UserId = request.UserId,
                ProductId = request.ProductId,
                Price = product.Price * request.Quantity
            };

            await _uow.GetRepository<Cart>().InsertAsync(newCart);

            var isCreated = await _uow.CommitAsync() > 0;
            return isCreated;
            
        }


        public async Task<IPaginate<GetCartResponse>> GetAllCartProduct(int id, int page, int size)
        {
            var cartEntries = await _uow.GetRepository<Cart>().GetPagingListAsync(predicate: p => p.UserId == id, page: page, size: size );

            if (cartEntries == null)
            {
                throw new Exception("Không tìm thấy giỏ hàng của bạn");
            }
            var cartsResponses = new Paginate<GetCartResponse>()
            {
                Page = cartEntries.Page,
                Size = cartEntries.Size,
                Total = cartEntries.Total,
                TotalPages = cartEntries.TotalPages,
                Items = _mapper.Map<IList<GetCartResponse>>(cartEntries.Items),
            };
            return cartsResponses;
        }

        public async Task<bool> RemoveFromCart(int id)
        {
            var cart = await _uow.GetRepository<Cart>().SingleOrDefaultAsync(predicate: p => p.Id == id);
            if (cart == null)
            {
                throw new Exception("Không tìm thấy giỏ hàng");
            }
            else
            {
                _uow.GetRepository<Cart>().DeleteAsync(cart);
                bool isDeleted = await _uow.CommitAsync() > 0;
                return isDeleted;
            }
        }




    }
}
