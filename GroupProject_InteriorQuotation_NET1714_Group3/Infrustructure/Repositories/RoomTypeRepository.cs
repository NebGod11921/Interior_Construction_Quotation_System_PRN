using Application.IRepositories;
using Application.ViewModels;
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
    public class RoomTypeRepository : GenericRepository<RoomType>, IRoomTypeRepository
    {
        private readonly AppDbContext _appDbContext;
        public RoomTypeRepository(AppDbContext appDbContext) : base(appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public IQueryable<RoomType> GetAllRoomTypes()
        {
            return _appDbContext.RoomTypes
         .Include(rt => rt.Rooms)
             .ThenInclude(r => r.RoomProducts)
                 .ThenInclude(rp => rp.Product)
                     .ThenInclude(p => p.ProductImages);
        }

		

		public async Task<List<RoomTypeDTO>> GetAllRoomTypeToAdd()
        {
            var typename = await _appDbContext.RoomTypes.ToListAsync();
            return typename.Select(color => new RoomTypeDTO
            {
                RoomTypeId = color.Id,
                RoomTypeName = color.RoomTypeName,
                
            }).ToList();
        }
		

		public async Task<RoomTypeDTO> GetRoomTypeNameById(int id)
        {
            var aa = await _appDbContext.RoomTypes.FirstOrDefaultAsync(c => c.Id == id);
            if (aa != null)
            {
                var color = new RoomTypeDTO
                {
                    RoomTypeId = aa.Id,
                    RoomTypeName = aa.RoomTypeName
                };
                return color;
            }
            else
            {
                return null;
            }
        }

        public async Task<RoomType> GetRoomTypeNameByName(string typename)
        {
            return await _appDbContext.RoomTypes.FirstOrDefaultAsync(c => c.RoomTypeName == typename);
        }

        //Luan
		public async Task<IEnumerable<RoomType>> GetAllRoomTypesById(int roomTypeId)
		{
			try
            {
                var result = await _appDbContext.RoomTypes.Where(x => x.Id == roomTypeId).ToListAsync();
                if (result.Count > 0)
                {
                    return result;
                } else
                {
                    return null;
                }

            }catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
		}
		public Task<RoomType> GetRoomTypeById(int roomTypeId)
		{
			try
            {
                var result = _appDbContext.RoomTypes.Where(x => x.Id == roomTypeId).FirstOrDefaultAsync();
                if (result != null)
                {
                    return result;
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

        public async Task<IEnumerable<RoomType>> GetAllRoomType()
        {
            try
            {
                var result = await _appDbContext.RoomTypes.Include("Rooms").ToListAsync();
                if (result != null)
                {
                    return result;
                }
                else
                {
                    return null;
                }

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
    
}
