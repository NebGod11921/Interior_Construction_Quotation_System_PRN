using Application.Interfaces;
using Application.IRepositories;
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
    public class ProductService : IProductService

    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;
        public ProductService(IUnitOfWork unitOfWork, IMapper mapper)
        {

            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<List<Product>> GetAllProduct()
        {
            try
            {
                var products = await _unitOfWork.ProductRepository.GetAllAsync();
                if(products.Any() == true)
                {
                    return products;
                }
                else
                {
                    return null;
                }
            }catch(Exception e)
            {
                throw new Exception(e.InnerException.ToString());
            }
        }

        public List<ProductDto> GetAllProductByRoomId(int roomid)
        {
			var product = _unitOfWork.ProductRepository.getProductByRoomId(roomid);
			var productDtos = product.Select(p => new ProductDto
			{				
				ProductName = p.ProductName,
				Description = p.Description,
				Quantity = p.Quantity,
				Size = p.Size,
				Price = p.Price,
				ImageUrl = p.ProductImages.FirstOrDefault()?.Image?.ImageName,
				Color = p.Color.ColourName,
				Material = p.Material.MaterialName
			}).ToList();


			return productDtos;
        }

		
	
	}
}
    