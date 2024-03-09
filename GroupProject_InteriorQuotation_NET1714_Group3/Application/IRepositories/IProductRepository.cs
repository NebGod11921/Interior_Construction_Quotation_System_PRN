using Application.Repositories;
using Application.ViewModels;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.IRepositories
{
    public interface IProductRepository
    {
      public List<Product> getProductByRoomId(int roomId);
      Product GetProductById(int id);
        Product GetProductByIdToCart(int id);

    }
}
