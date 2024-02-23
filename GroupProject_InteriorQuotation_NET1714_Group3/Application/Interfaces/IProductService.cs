using Application.ServiceResponse;
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
        ServiceResponse<ProductDTO> GetProductsInRoomService(Guid roomId);      

    }
}
