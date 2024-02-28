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

	public List<Product> getProductByRoomId(int roomId)
{
    var products = _appDbContext.RoomProduct
        .Where(rp => rp.RoomId == roomId)
        .Include(rp => rp.Product.Material)
        .Include(rp => rp.Product.Color)
        .Include(rp => rp.Product.ProductImages)
            .ThenInclude(pi => pi.Image)
        .Select(rp => rp.Product) // Select the product before applying any further includes
        .ToList();

    return products;
}



	}
}
