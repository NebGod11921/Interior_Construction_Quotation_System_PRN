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

        

        public async Task<IEnumerable<Room>> GetAllRooms()
        {
            try
            {
                var result = await _appDbContext.Rooms.Include("RoomType").ToListAsync();
                if (result == null)
                {
                    return null;
                }
                else
                {
                    return result;
                }

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<Room> GetRoomById(int roomId)
		{
			try
            {
                var result = await _appDbContext.Rooms.Where(x => x.Id == roomId).FirstOrDefaultAsync();
                if (result == null)
                {
                    return null;
                }
                else
                {
                    return result;
                }
                
            } catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
		}
        public async Task<bool> CreateRoom(Room room)
        {
            try
            {
                var r = _appDbContext.Rooms.Include(b => b.RoomType).Select(x => x.RoomType.Id);
                if (r != null)
                {
                    await _appDbContext.AddAsync(room);
                    
                    return true;
                }

                return false;

            }catch(Exception ex)
            {
                throw new Exception(ex.Message);
            }
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
