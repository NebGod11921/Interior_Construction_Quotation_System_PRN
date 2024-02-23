using Application.Interfaces;
using Application.ServiceResponse;
using Application.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services
{
    public class ProductService : IProductService
    {

        public ServiceResponse<ProductDTO> GetProductsInRoomService(Guid roomId)
        {
            throw new NotImplementedException();
        }
        
    }
}
