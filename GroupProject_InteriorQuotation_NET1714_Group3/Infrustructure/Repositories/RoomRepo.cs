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
                var result = await _appDbContext.Rooms.Include(x => x.RoomType).ToListAsync();
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

        public async Task<Room> GetNewRooms()
        {
            try
            {
                var latestRoomId = await _appDbContext.Rooms.MaxAsync(x => x.Id);
                if (latestRoomId == null)
                {
                    throw new InvalidOperationException("No rooms found in the database.");
                } else
                {
					var latestRoom = await _appDbContext.Rooms.FirstOrDefaultAsync(x => x.Id == latestRoomId);

					return latestRoom;
				}

                
            }
            catch (Exception ex)
            {
                throw new Exception("Error occurred while retrieving the latest room: " + ex.Message);
            }
        }


        public async Task<Room> GetRoomById(int roomId)
		{
			try
            {
                var result = await _appDbContext.Rooms.Include(r => r.RoomType).Where(x => x.Id == roomId).FirstOrDefaultAsync();
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

                 var r =    await _appDbContext.Rooms.AddAsync(room);
                if (r != null)
                {
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

        public async Task<bool> DeleteRoom(int roomId)
        {
            try
            {
                var result = await _appDbContext.Rooms.Where(x => x.Id == roomId).FirstOrDefaultAsync();
                if (result != null)
                {
                    _appDbContext.Remove(result);
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
    }
}
