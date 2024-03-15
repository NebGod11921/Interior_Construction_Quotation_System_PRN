using Application.IRepositories;
using Domain.Entities;
using Infrustructure;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repositories
{
    public class RoomRepo : GenericRepository<Room>, IRoomRepo
    {
        private readonly AppDbContext _appDbContext;
        public RoomRepo(AppDbContext appDbContext) : base(appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public string GetRoomNameByRoomId(int roomId)
        {
            return _appDbContext.Rooms
                .Where(r => r.Id == roomId)
                .Select(r => r.RoomDescription).FirstOrDefault();
        }

        public IEnumerable<Room> GetRoomsByRoomType(int roomTypeId)
        {
            return _appDbContext.Rooms.Where(r => r.RoomType.Id == roomTypeId).ToList();
        }
    }
}
