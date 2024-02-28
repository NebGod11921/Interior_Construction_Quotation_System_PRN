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
    } 
}
