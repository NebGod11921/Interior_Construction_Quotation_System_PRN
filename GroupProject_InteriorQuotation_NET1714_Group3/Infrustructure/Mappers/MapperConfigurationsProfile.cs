using Application.ViewModels;
using AutoMapper;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Mappers
{
    public class MapperConfigurationsProfile : Profile
    {
        public MapperConfigurationsProfile()
        {
            CreateMap<Product, ProductDTO>().ReverseMap();
            //CreateMap<Room, RoomHomePageProduct>().ReverseMap();
            CreateMap<RoomType, RoomHomePageProduct>()
            .ForMember(dest => dest.RoomType, opt => opt.MapFrom(src => src.RoomTypeName))
            .ForMember(dest => dest.Products, opt => opt.MapFrom(src => src.Rooms.SelectMany(room => room.RoomProducts.Select(rp => new ProductDTO
            {
                ProductName = rp.Product.ProductName,
                ImageUrl = rp.Product.ProductImages.FirstOrDefault().Image.ImageName

            }).ToList())));
            CreateMap<User,AccountDTO>().ReverseMap();
            CreateMap<User, AccountLoginDTO>().ReverseMap();
        }
    }
}
