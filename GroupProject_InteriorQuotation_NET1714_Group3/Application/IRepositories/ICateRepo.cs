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
    public interface ICateRepo : IGenericRepository<Category>
    {
        Task<List<CategoryDTO>> GetAllCate();
        Task<CategoryDTO> GetCateNameById(int id);
    }
}
