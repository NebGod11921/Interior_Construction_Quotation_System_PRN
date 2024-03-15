using Application.Repositories;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.IRepositories
{
    public interface IImageRepo : IGenericRepository<Image>
    {
        Task<int?> AddImage(Image image);
        Task AddProductImage(ProductImage productImage);
    }
}
