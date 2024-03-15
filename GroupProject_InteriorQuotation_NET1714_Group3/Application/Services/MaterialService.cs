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
    public class MaterialService : IMaterialService

    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;
        public MaterialService(IUnitOfWork unitOfWork, IMapper mapper)
        {

            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public async Task<List<MaterialDTO>> GetAllMaterial()
        {
            return await _unitOfWork.MaterialRepo.GetAllMaterial();
        }

        public Task<MaterialDTO> GetMaterialById(int id)
        {
            return _unitOfWork.MaterialRepo.GetMaterialNameById(id);
        }
    }
}
