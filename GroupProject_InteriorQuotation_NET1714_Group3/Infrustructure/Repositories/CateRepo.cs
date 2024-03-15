using Application.IRepositories;
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
    public class CateRepo : GenericRepository<Category>, ICateRepo
    {
        private readonly AppDbContext _appDbContext;
        public CateRepo(AppDbContext appDbContext) : base(appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public async Task<List<CategoryDTO>> GetAllCate()
        {
            var cate = await _appDbContext.Categories.ToListAsync();
            return cate.Select(c => new CategoryDTO
            {
                CateId = c.Id,
                CateName = c.CategoryName
            }).ToList();
        }

        public async Task<CategoryDTO> GetCateNameById(int id)
        {
            var aa = await _appDbContext.Categories.FirstOrDefaultAsync(c => c.Id == id);
            if (aa != null)
            {
                var catee = new CategoryDTO
                {
                    CateId = aa.Id,
                    CateName = aa.CategoryName
                };
                return catee;
            }
            else
            {
                return null;
            }
        }
    }
}
