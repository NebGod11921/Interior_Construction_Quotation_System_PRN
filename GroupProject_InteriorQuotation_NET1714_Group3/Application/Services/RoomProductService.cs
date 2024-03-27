using Application.Interfaces;
using Application.ViewModels;
using AutoMapper;
using Domain.Entities;


namespace Application.Services
{
    public class RoomProductService : IRoomProductService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public RoomProductService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public async Task<bool> CreateRoomProduct(RoomProductDTO roomProductDTO)
        {
            try
            {
                RoomProduct r_mapper = _mapper.Map<RoomProduct>(roomProductDTO);
                await _unitOfWork.RoomProductRepositiory.AddRoomProduct(r_mapper);
                if (await _unitOfWork.SaveChangeAsync() > 0)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message, e);
            }
        }
    }
}
