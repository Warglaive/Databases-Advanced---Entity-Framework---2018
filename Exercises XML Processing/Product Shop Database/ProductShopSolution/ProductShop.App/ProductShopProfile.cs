﻿using AutoMapper;
using ProductShop.App.Dto;
using ProductShop.Models;

namespace ProductShop.App
{
    public class ProductShopProfile : Profile
    {
        public ProductShopProfile()
        {
            CreateMap<UserDto, Users>();
            CreateMap<ProductDto, Products>();
            CreateMap<CategoryDto, Categories>();
        }
    }
}