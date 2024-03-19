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


		//Create Rooms
		public async Task<RoomDTOS> CreateRoom(RoomDTOS roomDTOS)
		{
			try
			{
				var mapping = _mapper.Map<Room>(roomDTOS);

				

                mapping.Area = roomDTOS.Area;
			
				mapping.IsDeleted = roomDTOS.IsDeleted ;
				mapping.CreationDate = roomDTOS.CreationDate;
				mapping.RoomDescription = roomDTOS.RoomDescription;
                await _unitOfWork.RoomRepo.CreateRoom(mapping);
                var mappedDTOS = _mapper.Map<RoomDTOS>(mapping);
				if (mappedDTOS != null)
				{
                    return mappedDTOS;
                } else
				{
					return null;
				}
				


			} catch (Exception ex)
			{
				
				throw new Exception(ex.Message);
                
            }
		}

		public async Task<bool> DeleteRoom2nd(int roomId)
		{
			try
			{
				var getRoomId = await _unitOfWork.RoomRepo.GetRoomById(roomId);
				if (getRoomId != null)
				{
					await _unitOfWork.RoomRepo.DeleteRoom(getRoomId.Id);
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

		public async Task<IEnumerable<RoomDTOS>> GetAllRooms()
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

		public async Task<RoomDTOS> GetRoomById2nd(int roomId)
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
					getRoomId.CreationDate = roomDTOS.CreationDate;
					var roomType = await _unitOfWork.RoomTypeRepository.GetRoomTypeById(roomDTOS.RoomTypeId);
					if (roomType != null )
					{
						getRoomId.RoomType = roomType;
                        _unitOfWork.RoomRepo.Update(getRoomId);
                        var IsSuccess = await _unitOfWork.SaveChangeAsync() > 0;
                        if (IsSuccess)
                        {
                            return true;
                        } else
						{
                            return false;
                        }
                    }
				}
				return false;

			} catch (Exception ex)
			{
				throw new Exception(ex.Message);
			}
		}
	}
}
