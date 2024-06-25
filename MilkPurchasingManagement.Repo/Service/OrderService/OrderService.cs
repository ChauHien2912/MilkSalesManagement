using AutoMapper;
using MilkPurchasingManagement.Repo.Dtos.Request.Order;
using MilkPurchasingManagement.Repo.Models;
using MilkPurchasingManagement.Repo.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MilkPurchasingManagement.Repo.Service.OrderService
{
    public class OrderService
    {
        private readonly IUnitOfWork _uow;
        private readonly IMapper _mapper;

        public OrderService(IUnitOfWork uow, IMapper mapper)
        {
            _uow = uow;
            _mapper = mapper;
        }


        public async Task<bool> CreateOrder(CreateOrderRequest request)
        {
            var userexisting = await _uow.GetRepository<User>().SingleOrDefaultAsync(predicate: e => e.Id == request.UserId);
            if (userexisting == null)
            {
                throw new Exception("User Not Found");
            }
            var paymentexisting = await _uow.GetRepository<Payment>().SingleOrDefaultAsync(predicate: e => e.Id == request.PaymentId);
            if (paymentexisting == null)
            {
                throw new Exception("Payment Not Found");
            }

            var order = _mapper.Map<Order>(request);    
            await _uow.GetRepository<Order>().InsertAsync(order);
            bool isCreated = await _uow.CommitAsync() > 0;
            return isCreated;
        }


        



    }
}
