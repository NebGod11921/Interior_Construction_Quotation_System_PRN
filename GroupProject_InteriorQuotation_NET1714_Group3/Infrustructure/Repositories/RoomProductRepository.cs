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

        public async Task<RoomProduct> AddRoomProduct(RoomProduct roomProduct)
        {
            try
            {
                var result = _appDbContext.RoomProduct.Add(roomProduct);
                if (result != null)
                {
                    return result.Entity;
                }
                else
                {
                    return null;
                }


            }catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
