using Application.IRepositories;
using Application.Repositories;
using Application.ViewModels;
using Domain.Entities;
using Infrustructure;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repositories
{
    public class MaterialRepo : GenericRepository<Material>, IMaterialRepo
    {
        private readonly AppDbContext _appDbContext;
        public MaterialRepo(AppDbContext appDbContext) : base(appDbContext)
        {
            _appDbContext = appDbContext;
        }
 
        public async Task<List<MaterialDTO>> GetAllMaterial()
        {
            var materials = await _appDbContext.Materials.ToListAsync();
            return materials.Select(color => new MaterialDTO
            {
                MaterialId = color.Id,
                MaterialName = color.MaterialName,
            }).ToList();
        }

        public async Task<Material> GetmaterialByName(string mater)
        {
            return await _appDbContext.Materials.FirstOrDefaultAsync(c => c.MaterialName == mater);
        }

        public async Task<MaterialDTO> GetMaterialNameById(int id)
        {
            var aa = await _appDbContext.Materials.FirstOrDefaultAsync(c => c.Id == id);
            if (aa != null)
            {
                var material = new MaterialDTO 
                {
                    MaterialId = aa.Id,
                    MaterialName = aa.MaterialName
                };
                return material;
            }
            else
            {
                return null;
            }
        }
    }
}
