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
    public  class ProductRepository : GenericRepository<Product>, IProductRepository
    {
        private readonly AppDbContext _appDbContext;
        public ProductRepository(AppDbContext appDbContext) : base(appDbContext)
        {
            _appDbContext = appDbContext;
        }
        public List<Product> GetProductsInRoom(Guid roomId)
        {
            return _appDbContext.Rooms
           .Where(r => r.Id == roomId)
           .SelectMany(r => r.RoomProducts.Select(rp => rp.Product))
           .Include(p => p.Color)
           .Include(p => p.Material)
           .Include(p => p.Category)
           .ToList();
        }
    }
}
