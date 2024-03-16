using Application.ViewModels;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces
{
    public interface IRoomService
    {
        IEnumerable<Room> GetRoomsByRoomType(int roomTypeId);
        string GetRoomNameByRoomID(int roomId);

        //Luan
        public Task<IEnumerable<RoomDTOS>> GetAllRoom();
        public Task<RoomDTOS> GetRoomById(int roomId);
        public Task<RoomDTOS> CreateRoom(RoomDTOS roomDTOS);
        public Task<bool> DeleteRoom(RoomDTOS roomDTOS,int roomId);
        public Task<bool> UpdateRoom(RoomDTOS roomDTOS, int roomId);

    }
}
