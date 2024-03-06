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

        public List<ProductDto> GetAllProductByRoomId(int roomid)
        {
			var product = _unitOfWork.ProductRepository.getProductByRoomId(roomid);
            var productDtos = product.Select(p => new ProductDto
            {
                ProductId = p.Id,
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

        public ProductDto GetProductById(int id)
        {
            try
            {
                var product =  _unitOfWork.ProductRepository.GetProductById(id);

                if (product != null)
                {
                    return _mapper.Map<ProductDto>(product);
                }
                else
                {
                    return null;
                }
            }
            catch (Exception e)
            {
                return null;
            }
        }

        public ProductDto GetProductByIdToCart(int id)
        {
            var pro = _unitOfWork.ProductRepository.GetProductByIdToCart(id);
            if (pro == null)
            {
                return null;
            }
            else {
                var aa = new ProductDto
                {
                    ProductId = pro.Id,
                    ProductName = pro.ProductName,
                    Description = pro.Description,
                    Quantity = pro.Quantity,
                    Size = pro.Size,
                    Price = pro.Price,
                    ImageUrl = pro.ProductImages.FirstOrDefault()?.Image?.ImageName,
                    Color = pro.Color.ColourName,
                    Material = pro.Material.MaterialName
                };
                return aa;
            }
        }
    }
}
    