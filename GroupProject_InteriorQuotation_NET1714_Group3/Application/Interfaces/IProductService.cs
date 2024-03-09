
using Application.Repositories;
using Application.ViewModels;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces
{
    public interface IProductService
    {
       List<ProductDto> GetAllProductByRoomId(int roomid);
       ProductDto GetProductById(int id);
        ProductDto GetProductByIdToCart(int id);
	}
}
