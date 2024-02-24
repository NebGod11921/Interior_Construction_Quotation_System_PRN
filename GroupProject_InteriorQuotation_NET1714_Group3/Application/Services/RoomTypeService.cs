using Application.Interfaces;
using Application.ViewModels;
using AutoMapper;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services
{
    public class RoomTypeService : IRoomTypeService
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;
        public RoomTypeService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public List<RoomHomePageProduct> GetAllRoomTypesWithProducts()
        {
            var roomTypes = _unitOfWork.RoomTypeRepository.GetAllRoomTypesWithProducts();
            var roomTypeViewModels = roomTypes.Select(roomType => new RoomHomePageProduct
            {
                RoomType = roomType.RoomTypeName,
                Products = roomType.Rooms.SelectMany(room => room.RoomProducts.Select(rp => new ProductDTO
                {
                    ProductName = rp.Product.ProductName,
                    ImageUrl = rp.Product.ProductImages.FirstOrDefault().Image.ImageName
                })).ToList()
            }).ToList();

            return roomTypeViewModels;

            //var roomTypes = _unitOfWork.RoomTypeRepository.GetAllRoomTypesWithProducts();
            //var roomTypeViewModels = _mapper.Map<List<RoomHomePageProduct>>(roomTypes);
            //return roomTypeViewModels;
        }
    }
}
