using AutoMapper;
using Microsoft.EntityFrameworkCore;
using MilkPurchasingManagement.Repo.Dtos.Request.Order;
using MilkPurchasingManagement.Repo.Dtos.Response;
using MilkPurchasingManagement.Repo.Dtos.Response.Order;
using MilkPurchasingManagement.Repo.Models;
using MilkPurchasingManagement.Repo.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;

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

        public ApiResponse CreateOrder(CreateOrderRequest request, List<OrderDetailCreateRequestModel> orderDetails)
        {
            using (var scope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
            {
                try
                {
                    var order = new Order
                    {
                        UserId = request.UserId,
                        CreatedDate = DateTime.UtcNow,
                        PaymentId = request.PaymentId,
                        DeliveryAdress = request.DeliveryAdress,
                        Status = "Pending"
                    };

                    // Insert order and commit to generate OrderId
                    _uow.GetRepository<Order>().InsertAsync(order).Wait();
                    _uow.Commit();

                    decimal? totalPrice = 0;
                    foreach (var orderDetailModel in orderDetails)
                    {
                        var product = _uow.GetRepository<Product>().SingleOrDefaultAsync(predicate: e => e.Id == orderDetailModel.ProductId).Result;
                        if (product == null)
                        {
                            // Handle product not found error
                            return new ApiResponse
                            {
                                Success = false,
                                Message = "Product not found",
                            };
                        }

                        if (product.Quantity < orderDetailModel.Quantity)
                        {
                            // Handle insufficient stock error
                            return new ApiResponse
                            {
                                Success = false,
                                Message = "Insufficient stock for product: " + product.Name,
                            };
                        }

                        var orderDetail = new OrderDetail
                        {
                            ProductId = orderDetailModel.ProductId,
                            OrderId = order.Id,
                            Price = product.Price,
                            Quantity = orderDetailModel.Quantity
                        };

                        // Insert order detail
                        _uow.GetRepository<OrderDetail>().InsertAsync(orderDetail).Wait();
                        totalPrice += orderDetail.Price * orderDetail.Quantity;

                        // Decrease the product stock and update
                        product.Quantity -= orderDetailModel.Quantity;
                        _uow.GetRepository<Product>().UpdateAsync(product);
                    }

                    // Update order with total price
                    order.TotalAmount = totalPrice.Value;
                    _uow.GetRepository<Order>().UpdateAsync(order);
                    _uow.Commit();

                    // Map the order to OrderResponseModel
                    var orderResponse = _mapper.Map<OrderResponseModel>(order);

                    // Complete the transaction scope
                    scope.Complete();

                    return new ApiResponse
                    {
                        Success = true,
                        Message = "Order created successfully",
                        Data = orderResponse
                    };
                }
                catch (DbUpdateException ex)
                {
                    // Log the detailed error
                    var innerExceptionMessage = ex.InnerException?.Message;
                    return new ApiResponse
                    {
                        Success = false,
                        Message = $"An error occurred while saving the order: {innerExceptionMessage}"
                    };
                }
                catch (Exception ex)
                {
                    // Handle other possible exceptions
                    return new ApiResponse
                    {
                        Success = false,
                        Message = $"An unexpected error occurred: {ex.Message}"
                    };
                }
            }
        }

    }
}



    

    
