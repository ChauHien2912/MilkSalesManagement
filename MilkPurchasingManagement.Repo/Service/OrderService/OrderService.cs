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
                        Status = "Chưa giao"
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

        public async Task<ApiResponse> GetAllOrdersAsync()
        {
            try
            {
                var orders = await _uow.GetRepository<Order>().GetListAsync(include: p => p.Include(m => m.User).Include(m => m.OrderDetails).ThenInclude(od => od.Product));
                var orderResponseModels = _mapper.Map<List<OrderResponseModel>>(orders);

                return new ApiResponse
                {
                    Success = true,
                    Message = "Orders retrieved successfully",
                    Data = orderResponseModels
                };
            }
            catch (Exception ex)
            {
                // Log the detailed error
                return new ApiResponse
                {
                    Success = false,
                    Message = $"An error occurred while retrieving the orders: {ex.Message}"
                };
            }

        }
        public async Task<ApiResponse> AutoChangeOrderStatusAsync(OrderStatusUpdateRequestModel orderStatusUpdateDto)
        {
            using (var scope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
            {
                try
                {
                    var order = await _uow.GetRepository<Order>().SingleOrDefaultAsync(predicate: e => e.Id == orderStatusUpdateDto.OrderId);
                    if (order == null)
                    {
                        return new ApiResponse
                        {
                            Success = false,
                            Message = "Order not found",
                        };
                    }

                    // Change status based on current status
                    if (order.Status == "Chưa giao")
                    {
                        order.Status = "Đang giao";
                    }
                    else
                    {
                        order.Status = orderStatusUpdateDto.NewStatus;
                    }

                     _uow.GetRepository<Order>().UpdateAsync(order);
                    await _uow.CommitAsync();

                    // Complete the transaction scope
                    scope.Complete();

                    return new ApiResponse
                    {
                        Success = true,
                        Message = order.Status == "Đang giao" ? "Order status automatically updated to 'Đang giao'" : $"Order status automatically updated to '{orderStatusUpdateDto.NewStatus}'",
                    };
                }
                catch (DbUpdateException ex)
                {
                    // Log the detailed error
                    var innerExceptionMessage = ex.InnerException?.Message;
                    return new ApiResponse
                    {
                        Success = false,
                        Message = $"An error occurred while automatically updating the order status: {innerExceptionMessage}"
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
        public async Task<ApiResponse> GetOrderByIdAsync(int orderId)
        {
            try
            {
                var order = await _uow.GetRepository<Order>().SingleOrDefaultAsync(
                    predicate: o => o.Id == orderId,
                    include: o => o.Include(o => o.User).Include(o => o.OrderDetails).ThenInclude(od => od.Product));

                if (order == null)
                {
                    return new ApiResponse
                    {
                        Success = false,
                        Message = "Order not found"
                    };
                }

                var orderResponseModel = _mapper.Map<OrderResponseModel>(order);

                return new ApiResponse
                {
                    Success = true,
                    Message = "Order retrieved successfully",
                    Data = orderResponseModel
                };
            }
            catch (Exception ex)
            {
                // Log the detailed error
                return new ApiResponse
                {
                    Success = false,
                    Message = $"An error occurred while retrieving the order: {ex.Message}"
                };
            }
        }
    }
}

    













