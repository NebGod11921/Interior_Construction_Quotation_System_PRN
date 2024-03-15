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
            CreateMap<Product, RoomHomePageDTO>().ReverseMap();
            CreateMap<Product, ProductDto>().ReverseMap();
            //CreateMap<Room, RoomHomePageProduct>().ReverseMap();
            CreateMap<RoomType, RoomHomePageTitle>()
            .ForMember(dest => dest.RoomType, opt => opt.MapFrom(src => src.RoomTypeName))
            .ForMember(dest => dest.RoomInType, opt => opt.MapFrom(src => src.Rooms.SelectMany(room => room.RoomProducts.Select(rp => new RoomHomePageDTO
            {
                RoomName = rp.Room.RoomDescription,
                ImageProduct = room.RoomProducts.Select(rp => new ImageRoomInTypeDTO
                {
                    ImageUrl = rp.Product.ProductImages.FirstOrDefault().Image.ImageName
                }).ToList()

            }).ToList())));

            CreateMap<User,AccountDTO>().ReverseMap();
            CreateMap<User, AccountLoginDTO>().ReverseMap();
            CreateMap<Quotation, QuotationDTO>().ReverseMap();
            CreateMap<Room, RoomDTO>().ReverseMap();
            CreateMap<RoomType, RoomTypeDTO>().ReverseMap();
        }
    }
}
