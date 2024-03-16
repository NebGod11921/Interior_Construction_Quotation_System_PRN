using Application.Interfaces;
using Application.IRepositories;
using Application.ViewModels;
using AutoMapper;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
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

        public async Task<int> AddProductAsync(ProductDto productDto)
        {
            try
            {
                Product product_mapper = _mapper.Map<Product>(productDto);

                int? productid = await _unitOfWork.ProductRepository.AddProduct(product_mapper);
                if (productid.HasValue && productid > 0)
                {
                   
                    return productid.Value;
                }
                else
                {
                    throw new Exception("Failed to add product.");
                }

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<bool> AddRoomProductAsync(int productid, int roomid)
        {
            var roomProduct = new RoomProduct
            {
                ProductId = productid,
                RoomId = roomid
            };
            await _unitOfWork.ProductRepository.AddProductToRoomAsync(roomProduct);
            return true;
        }

       

        public List<ProductDto> GetAllProduct()
        {
            var products = _unitOfWork.ProductRepository.ListProduct();
            var dtos = products.Select(p => new ProductDto
            {
                ProductId = p.Id,
                RoomTypeName = p.RoomProducts.FirstOrDefault()?.Room?.RoomType?.RoomTypeName,
                RoomName = p.RoomProducts.FirstOrDefault()?.Room?.RoomDescription,
                ProductName = p.ProductName,
                Description = p.Description,
                Quantity = p.Quantity,
                Size = p.Size,
                Price = p.Price,
                ImageUrl = p.ProductImages.FirstOrDefault()?.Image?.ImageName,
                Color = p.Color.ColourName,
                Material = p.Material.MaterialName,
                IsDeleted = p.IsDeleted
            }).ToList();
            return dtos;
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
        public List<ProductDto> GetAllProductSearch(string input)
        {
            var productt = _unitOfWork.ProductRepository.SearchListProduct(input);
            var dto = productt.Select(p => new ProductDto
            {
                ProductId = p.Id,
                RoomTypeName = p.RoomProducts.FirstOrDefault().Room.RoomType.RoomTypeName,
                RoomName = p.RoomProducts.FirstOrDefault()?.Room?.RoomDescription,
                ProductName = p.ProductName,
                Description = p.Description,
                Quantity = p.Quantity,
                Size = p.Size,
                Price = p.Price,
                ImageUrl = p.ProductImages.FirstOrDefault()?.Image?.ImageName,
                Color = p.Color.ColourName,
                Material = p.Material.MaterialName,
                IsDeleted = p.IsDeleted
            }).ToList();
            return dto;
        }

        public async Task<ProductDto> GetProductByIdWithAll(int id)
        {
            var product = await  _unitOfWork.ProductRepository.GetProductByIdWithAll(id);
            if (product == null)
            {
                return null;
            }
            else
            {
                var productDto = new ProductDto
                {
                    ProductId = product.Id,
                    ProductName = product.ProductName,
                    RoomTypeName = product.RoomProducts.FirstOrDefault()?.Room?.RoomType.RoomTypeName,
                    RoomName = product.RoomProducts.FirstOrDefault()?.Room?.RoomDescription,
                    Description = product.Description,
                    Quantity = product.Quantity,
                    Size = product.Size,
                    Price = product.Price,
                    ImageUrl = product.ProductImages.FirstOrDefault()?.Image?.ImageName,
                    Color = product.Color.ColourName,
                    Material = product.Material.MaterialName,
                    IsDeleted = product.IsDeleted.HasValue
                    
                };
			return productDto;
            }
        }

        public async Task<bool> UpdateProduct(ProductDto productDto)
        {
            try
            {
                var product = await  _unitOfWork.ProductRepository.GetProductByIdWithAll(productDto.ProductId);
                if (product != null)
                {
                    product.ProductName = productDto.ProductName;
                    product.Description = productDto.Description;
                    product.Quantity = productDto.Quantity;
                    product.Size = productDto.Size;
                    product.Price = productDto.Price;
                    var color = await _unitOfWork.ColorRepository.GetColorByName(productDto.Color);                
                    if(color != null)
                    {
                        product.Color = color;
                    }
                    else
                    {
                        throw new Exception("Color not found in the database.");
                    }
                    var mater = await _unitOfWork.MaterialRepo.GetmaterialByName(productDto.Material);
                    if (mater != null)
                    {
                        product.Material = mater;
                    }
                    else
                    {
                        throw new Exception("material not found in the database.");
                    }
                    product.IsDeleted = productDto.IsDeleted;
                    await _unitOfWork.ProductRepository.UpdateProductAsyncNew(product);               
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            
        }
        public async Task<bool> DeleteProduct(int productDto)
        {
            try
            {
                var product = await _unitOfWork.ProductRepository.GetProductByIdWithAll(productDto);
                if (product != null)
                {
                    product.IsDeleted = false;
                    await _unitOfWork.SaveChangeAsync();
                    return true;
                }
                else
                {
                    return false; 
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<bool> CheckExistProductName(string name)
        {
            return await _unitOfWork.ProductRepository.IsProductNameExistsAsync(name);
        }
        public ProductDto1 GetProductById(int id)
        {
            try
            {
                var product =  _unitOfWork.ProductRepository.GetProductById(id);

                if (product != null)
                {
                    return _mapper.Map<ProductDto1>(product);
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
    