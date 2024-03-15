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
    public interface IColorRepo : IGenericRepository<Color>  
    {
        Task<List<ColorDTO>> GetAllColors();
        Task<ColorDTO> GetColorNameById(int id);
        Task<Color> GetColorByName(string colorName);
    }
}
