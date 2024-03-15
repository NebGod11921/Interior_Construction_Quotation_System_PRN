using Application.Interfaces;
using Application.ViewModels;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services
{
    public class CategoryService : ICategoryService

    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;
        public CategoryService(IUnitOfWork unitOfWork, IMapper mapper)
        {

            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<List<CategoryDTO>> GetAllCate()
        {
            return await _unitOfWork.CateRepo.GetAllCate();
        }

        public Task<CategoryDTO> GetCateNameById(int id)
        {
            return _unitOfWork.CateRepo.GetCateNameById(id);
        }
    }
}
