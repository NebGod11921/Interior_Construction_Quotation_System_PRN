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
    public interface IMaterialRepo : IGenericRepository<Material>
    {
        Task<List<MaterialDTO>> GetAllMaterial();
        Task<MaterialDTO> GetMaterialNameById(int id);
        Task<Material> GetmaterialByName(string mater);

    }
}
