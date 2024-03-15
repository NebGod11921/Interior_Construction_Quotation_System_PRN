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
    public class RoomService : IRoomService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public RoomService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<bool> CreateRoom(RoomDTO roomDTO)
        {
            try
            {
                Room r_mapper = _mapper.Map<Room>(roomDTO);
                await _unitOfWork.RoomRepository.AddAsync(r_mapper);
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

        public Task<bool> DeleteRoom(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<List<RoomDTO>> GetAllRoom()
        {
            try
            {
                List<Room> rs = await _unitOfWork.RoomRepository.GetAllAsync();
                List<RoomDTO> r_mapper = _mapper.Map<List<RoomDTO>>(rs);
                if (r_mapper.Count > 0 && r_mapper != null)
                {
                    return r_mapper;
                }
                else
                {
                    return r_mapper;
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message, e);
            }
        }

        public List<RoomDTO> GetRoomByCsId(int csId)
        {
            throw new NotImplementedException();
        }

        public Task<RoomDTO> GetRoomById(int id)
        {
            throw new NotImplementedException();
        }

        public Task<bool> UpdateRoom(RoomDTO roomDTO, int id)
        {
            throw new NotImplementedException();
        }
        public string GetRoomNameByRoomID(int roomId)
        {
            return _unitOfWork.RoomRepo.GetRoomNameByRoomId(roomId);
        }

        public IEnumerable<Room> GetRoomsByRoomType(int roomTypeId)
        {
            return _unitOfWork.RoomRepo.GetRoomsByRoomType(roomTypeId);
        }
    }
}
