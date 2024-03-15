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
        public List<RoomHomePageTitle> GetAllRoomTypes()
        {
            var roomTypes = _unitOfWork.RoomTypeRepository.GetAllRoomTypes();
            var roomTypeViewModels = roomTypes.Select(roomType => new RoomHomePageTitle
            {
                RoomType = roomType.RoomTypeName,
                RoomInType = roomType.Rooms.Select(room => new RoomHomePageDTO
                {
                    RoomId = room.Id.ToString(),
                    RoomName = room.RoomDescription,
                    AreaRoom = room.Area,
                    ImageProduct = room.RoomProducts.Select(image => new Application.ViewModels.ImageRoomInTypeDTO
                    {
                        ImageUrl = image.Product.ProductImages.FirstOrDefault().Image.ImageName
                    }).ToList()
                }).ToList()
            }).ToList();
            return roomTypeViewModels;
        }

        public async Task<List<RoomTypeDTO>> GetAllRoomTypeToAdd()
        {
            return await _unitOfWork.RoomTypeRepository.GetAllRoomTypeToAdd();
        }

        

        public Task<RoomTypeDTO> GetRoomTypeNameById(int id)
        {
            return _unitOfWork.RoomTypeRepository.GetRoomTypeNameById(id);
        }
        public async Task<IEnumerable<RoomTypeDTOS>> GetAllTypesRoom()
        {
            try
            {
                var result = await _unitOfWork.RoomTypeRepository.GetAllRoomType();    
                if (result != null)
                {
                    var mapper = _mapper.Map<IEnumerable<RoomTypeDTOS>>(result);
                    return mapper;
                } else
                {
                    return null;
                }


            } catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<RoomTypeDTOS> GetRoomTypeById(int id)
        {
            try
            {
                var result = await _unitOfWork.RoomTypeRepository.GetByIdAsync(id);
                if (result != null)
                {
                    var mapper = _mapper.Map<RoomTypeDTOS>(result);
                    return mapper;
                }
                else
                {
                    return null;
                }


            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }





        public async Task<IEnumerable<RoomTypeDTOS>> GetRoomTypesById(int id)
        {
            try
            {
                var result = await _unitOfWork.RoomTypeRepository.GetAllRoomTypesById(id);
                if (result != null)
                {
                    var mapper = _mapper.Map<IEnumerable<RoomTypeDTOS>>(result);
                    return mapper;
                }
                else
                {
                    return null;
                }


            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
