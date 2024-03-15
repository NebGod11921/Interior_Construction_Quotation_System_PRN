
using Application.Repositories;
using Application.ViewModels;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces
{
    public interface IProductService
    {
       List<ProductDto> GetAllProductByRoomId(int roomid);
        List<ProductDto> GetAllProductSearch(string input);
        List<ProductDto> GetAllProduct();
        Task<ProductDto> GetProductByIdWithAll(int id);
        Task<bool> UpdateProduct(ProductDto productDto);
        Task<bool> DeleteProduct(int productDto);
        Task<int> AddProductAsync(ProductDto productDto);
        Task<bool> AddRoomProductAsync(int productid, int roomid);
        Task<bool> CheckExistProductName(string name);
        
        
    
       ProductDto GetProductById(int id);
        ProductDto GetProductByIdToCart(int id);
	}
}
