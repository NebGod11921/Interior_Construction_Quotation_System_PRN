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
    public class ColorRepo : GenericRepository<Color>, IColorRepo
    {
        private readonly AppDbContext _appDbContext;
        public ColorRepo(AppDbContext appDbContext) : base(appDbContext)
        {
            _appDbContext = appDbContext;
        }
        public async  Task<List<ColorDTO>> GetAllColors()
        {
            var colors = await _appDbContext.Colors.ToListAsync();
            return colors.Select(color => new ColorDTO
            {
                ColorId = color.Id,
                ColourName = color.ColourName,                
            }).ToList();
        }

        public async  Task<Color> GetColorByName(string colorName)
        {
            return await _appDbContext.Colors.FirstOrDefaultAsync(c => c.ColourName == colorName);
        }

        public async Task<ColorDTO> GetColorNameById(int id)
        {
            var aa = await _appDbContext.Colors.FirstOrDefaultAsync(c => c.Id == id);
            if(aa != null)
            {
                var color = new ColorDTO
                {
                    ColorId = aa.Id,
                    ColourName = aa.ColourName
                };
                return color;
            } else
            {
                return null;
            }
        }

        
    }
}
