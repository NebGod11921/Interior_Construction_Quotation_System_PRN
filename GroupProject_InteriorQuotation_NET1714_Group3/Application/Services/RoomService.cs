using Application.Interfaces;
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
    }
}
