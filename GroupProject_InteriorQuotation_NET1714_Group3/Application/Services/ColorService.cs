using Application.Interfaces;
using Application.ViewModels;
using AutoMapper;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services
{
    public class ColorService : IColorService

    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;
        public ColorService(IUnitOfWork unitOfWork, IMapper mapper)
        {

            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public async Task<List<ColorDTO>> GetAllColors()
        {
             return await _unitOfWork.ColorRepository.GetAllColors();          
        }

        public Task<ColorDTO> GetColorNameById(int id)
        {
            return _unitOfWork.ColorRepository.GetColorNameById(id);
        }
    }
}
