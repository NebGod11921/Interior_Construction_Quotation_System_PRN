﻿using Application.ViewModels;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces
{
    public interface IColorService
    {
        Task<List<ColorDTO>> GetAllColors();
        Task<ColorDTO> GetColorNameById(int id);
        
    }
}
