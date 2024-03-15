using Application.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces
{
    public interface IMaterialService
    {
        Task<List<MaterialDTO>> GetAllMaterial();

        Task<MaterialDTO> GetMaterialById(int id);
    }
}
