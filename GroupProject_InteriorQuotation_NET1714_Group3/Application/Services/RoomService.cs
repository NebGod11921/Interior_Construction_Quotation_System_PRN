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
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;
        public RoomService(IUnitOfWork unitOfWork, IMapper mapper)
        {

            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

		public string GetRoomNameByRoomID(int roomId)
        {
            return _unitOfWork.RoomRepo.GetRoomNameByRoomId(roomId);
        }

        public IEnumerable<Room> GetRoomsByRoomType(int roomTypeId)
        {
            return _unitOfWork.RoomRepo.GetRoomsByRoomType(roomTypeId);
        }


		//Create Rooms
		public async Task<bool> CreateRoom(RoomDTOS roomDTOS)
		{
			try
			{
				var mapping = _mapper.Map<Room>(roomDTOS);
				
				await _unitOfWork.RoomRepo.AddAsync(mapping);
				var isSuccess = await _unitOfWork.SaveChangeAsync() > 0;
				if (isSuccess)
				{
					return true;
				}
				else
				{
					return false;
				}


			} catch (Exception ex)
			{
				throw new Exception(ex.Message);
			}
		}

		public async Task<bool> DeleteRoom(RoomDTOS r, int roomId)
		{
			try
			{
				var getRoomId = await _unitOfWork.RoomRepo.GetRoomById(roomId);
				if (getRoomId != null)
				{
					getRoomId.IsDeleted = true;
					var IsSuccess = await _unitOfWork.SaveChangeAsync() > 0;
					if (IsSuccess)
					{
						var mapped = _mapper.Map(r, getRoomId);
						return true;
					}
					return false;
				}
				return false;

			} catch (Exception ex)
			{
				throw new Exception(ex.Message);
			}
		}

		public async Task<IEnumerable<RoomDTOS>> GetAllRoom()
		{
			try
			{
				var result = await _unitOfWork.RoomRepo.GetAllRooms();
				if (result != null)
				{
					var mapped = _mapper.Map<IEnumerable<RoomDTOS>>(result);
					return mapped;
				}
				else
				{
					return null;
				}


			} catch (Exception ex)
			{
				throw new Exception(ex.Message);
			}
		}

		public async Task<RoomDTOS> GetRoomById(int roomId)
		{
			try
			{
				var result = await _unitOfWork.RoomRepo.GetRoomById(roomId);
				if (result != null)
				{
					var mapped = _mapper.Map<RoomDTOS>(result);
					return mapped;
				}
				else
				{
					return null;
				}




			} catch (Exception ex)
			{
				throw new Exception(ex.Message);
			}
		}


		public async Task<bool> UpdateRoom(RoomDTOS roomDTOS, int roomId)
		{
			try
			{
				var getRoomId = await _unitOfWork.RoomRepo.GetRoomById(roomId);
				if (getRoomId != null)
				{
					getRoomId.Area = roomDTOS.Area;
					getRoomId.RoomDescription = roomDTOS.RoomDescription;
					var IsSuccess = await _unitOfWork.SaveChangeAsync() > 0;
					if (IsSuccess)
					{
						return true;
					}
					return false;
				}
				return false;

			} catch (Exception ex)
			{
				throw new Exception(ex.Message);
			}
		}
	}
}
