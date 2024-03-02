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
      public List<ProductDto> getProductByRoomId(int roomId);
      public Task<ProductDto> GetAllProductById(int id);

    }
}
