using Application.Repositories;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.IRepositories
{
    public interface IProductRepository : IGenericRepository<Product>
    {
      public List<Product> getProductByRoomId(int roomId);
        public List<Product> SearchListProduct(string input);
        public List<Product> ListProduct();
        Task<Product> GetProductByIdWithAll(int id);
        Task<int?> AddProduct(Product product);
          Task<bool> DeleteProduct(Product product);
        Task AddProductToRoomAsync(RoomProduct roomProduct);
        Task<bool> IsProductNameExistsAsync(string productName);

    }
}
