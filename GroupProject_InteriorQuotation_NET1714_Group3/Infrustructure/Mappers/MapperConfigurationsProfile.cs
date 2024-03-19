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
            CreateMap<Product, ProductDto>()
                .ForMember(dest => dest.Color, opt => opt.MapFrom(src => src.Color))
                .ForMember(dest => dest.Material, opt => opt.MapFrom(src => src.Material)).ReverseMap(); //add reser if need

            //            CreateMap<ProductDto, Product>()
            //.ForMember(dest => dest.Color, opt => opt.MapFrom(src => src.Color))
            //.ForMember(dest => dest.Material, opt => opt.MapFrom(src => src.Material));
            CreateMap<ProductDto, Product>()
                .ForMember(dest => dest.Color, opt => opt.MapFrom(src => new Color { ColourName = src.Color }))
            .ForMember(dest => dest.Material, opt => opt.MapFrom(src => new Material { MaterialName = src.Material }))
            .ForMember(dest => dest.Category, opt => opt.MapFrom(src => new Category { CategoryName = src.Categorys }));

            CreateMap<Category, CategoryDTO>().ReverseMap();
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
            CreateMap<RoomType, RoomTypeDTO1>().ReverseMap();
            CreateMap<Product, ProductDto1>().ReverseMap();

            CreateMap<RoomProduct, RoomProductDTO>().ReverseMap();


            CreateMap<Room, RoomDTOS>().ReverseMap();
            CreateMap<RoomType, RoomTypeDTOS>().ReverseMap();
            CreateMap<RoomTypeDTOS, RoomDTOS>().ReverseMap();
            

        }
    }
}
