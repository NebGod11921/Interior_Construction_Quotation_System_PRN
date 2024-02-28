using Application.Repositories;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.IRepositories
{
    public interface IRoomTypeRepository : IGenericRepository<RoomType>
    {
        public IQueryable<RoomType> GetAllRoomTypes();
    }
}
