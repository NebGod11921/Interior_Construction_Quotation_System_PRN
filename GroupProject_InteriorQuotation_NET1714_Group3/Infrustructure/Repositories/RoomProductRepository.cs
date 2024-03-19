using Application.IRepositories;
using Domain.Entities;
using Infrustructure;


namespace Infrastructure.Repositories
{
    public class RoomProductRepository : GenericRepository<RoomProduct>, IRoomProductRepositiory 
    {
        private readonly AppDbContext _appDbContext;
        public RoomProductRepository(AppDbContext appDbContext) : base(appDbContext)
        {
            _appDbContext = appDbContext;
        }
    }
}
