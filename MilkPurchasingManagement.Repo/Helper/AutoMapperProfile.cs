using AutoMapper;
using MilkPurchasingManagement.Repo.Dtos.Request.Product;
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


            //user
            CreateMap<User, GetUserResponse>().ReverseMap();


        }
        



    }
}
