using Application.Repositories;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.IRepositories
{
    public interface IRoomRepo : IGenericRepository<Room>
    {
        IEnumerable<Room> GetRoomsByRoomType(int roomTypeId);
        string GetRoomNameByRoomId(int roomId);

        //Luan
        public Task<Room> GetRoomById(int roomId);
        public Task<IEnumerable<Room>> GetAllRooms();
        


    }
}
