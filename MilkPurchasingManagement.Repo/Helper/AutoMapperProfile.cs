using AutoMapper;
using MilkPurchasingManagement.Repo.Dtos.Request.Product;
using MilkPurchasingManagement.Repo.Dtos.Response.Order;
using MilkPurchasingManagement.Repo.Dtos.Response.Product;
using MilkPurchasingManagement.Repo.Dtos.Response.User;
using MilkPurchasingManagement.Repo.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MilkPurchasingManagement.Repo.Helper
{
    public class AutoMapperProfile : Profile
    {
        // Product
        public AutoMapperProfile() {

            //Product
            CreateMap<GetProductResponse, Product>().ReverseMap();
            CreateMap<Product, GetProductResponse>().ReverseMap();
            CreateMap<CreateProductRequest, Product>().ReverseMap();
            CreateMap<UpdateProductRequest, Product>().ReverseMap();
            // Order mappings
            CreateMap<Order, OrderResponseModel>()
                .ForMember(dest => dest.OrderDetails, opt => opt.MapFrom(src => src.OrderDetails));

            // OrderDetail to OrderDetailModel mapping
            CreateMap<OrderDetail, OrderResponseModel.OrderDetailModel>()
                .ForMember(dest => dest.ProductName, opt => opt.MapFrom(src => src.Product.Name))
                .ForMember(dest => dest.Quantity, opt => opt.MapFrom(src => src.Quantity.ToString()))
                .ForMember(dest => dest.Price, opt => opt.MapFrom(src => src.Price.ToString()));


            //user
            CreateMap<User, GetUserResponse>().ReverseMap();


        }
        



    }
}
