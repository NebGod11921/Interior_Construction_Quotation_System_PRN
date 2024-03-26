using Application.IRepositories;
using Domain.Entities;
using Infrustructure;


namespace Infrastructure.Repositories
{
    public class RoomProductRepository : IRoomProductRepositiory 
    {
        private readonly AppDbContext _appDbContext;
        public RoomProductRepository(AppDbContext appDbContext) 
        {
            _appDbContext = appDbContext;
        }
    }
}
