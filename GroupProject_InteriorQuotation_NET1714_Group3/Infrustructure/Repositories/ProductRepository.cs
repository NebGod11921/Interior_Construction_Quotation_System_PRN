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
    public  class ProductRepository : GenericRepository<Product>, IProductRepository
    {
        private readonly AppDbContext _appDbContext;
        public ProductRepository(AppDbContext appDbContext) : base(appDbContext)
        {
            _appDbContext = appDbContext;
        }
        public async Task<bool> DeleteProduct(Product product)
        {
            try
            {
                 _appDbContext.Products.Remove(product);
                int rowsAffected = await _appDbContext.SaveChangesAsync();
                return rowsAffected > 0;
            }
            catch (Exception ex)
            {
                throw new Exception("Failed to remove product to database.", ex);
            }
        }
        public async Task<bool> UpdateProduct(Product product)
        {
            try
            {
                
                int rowsAffected = await _appDbContext.SaveChangesAsync();

                if (rowsAffected > 0)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Failed to add product to database.", ex);
            }
        }

        public async Task<int?> AddProduct(Product product)
        {
            try
            {                
                await _appDbContext.AddAsync(product);
                int rowsAffected = await _appDbContext.SaveChangesAsync();

                if (rowsAffected > 0)
                {                   
                    return product.Id;
                }
                else
                {
                    return null; 
                }
            }
            catch (Exception ex)
            {                
                throw new Exception("Failed to add product to database.", ex);
            }
        }
        public async Task UpdateProductAsyncNew(Product product)
        {
             _appDbContext.Update(product);
            await _appDbContext.SaveChangesAsync();
        }
        public async Task AddProductToRoomAsync(RoomProduct roomProduct)
        {
            await _appDbContext.AddAsync(roomProduct);
            await _appDbContext.SaveChangesAsync();
        }

        

        //private async Task<int> SaveChangeAsync()
        //{
        //    re
        //    turn await _appDbContext.SaveChangesAsync();
        //}

        public async Task<Product> GetProductByIdWithAll(int id)
        {
            var product = _appDbContext.Products
        .Where(p => p.Id == id)
        .Include(p => p.Category)
        .Include(p => p.Color)
        .Include(p => p.Material)
        .Include(p => p.ProductImages)
            .ThenInclude(pi => pi.Image)
        .Include(p => p.RoomProducts)
            .ThenInclude(rp => rp.Room)
            .ThenInclude(r => r.RoomType)
        .FirstOrDefault();
            return product;
        }

      
        public Product GetProductById(int id)
        {
            var products = _appDbContext.Products.
                            Include(x => x.Category)
                           .FirstOrDefault(rp => rp.Id == id);
            return products;
        }

        public Product GetProductByIdToCart(int id)
        {
            var products = _appDbContext.Products
        .Where(p => p.Id == id)
        .Include(rp => rp.Category)
        .Include(rp => rp.Material)
        .Include(rp => rp.Color)
        .Include(rp => rp.ProductImages)
            .ThenInclude(pi => pi.Image)
        .FirstOrDefault();

            return products;
        }

        public List<Product> getProductByRoomId(int roomId)
{
    var products = _appDbContext.RoomProduct
        .Where(rp => rp.RoomId == roomId)
        .Include(rp => rp.Product.Material)
        .Include(rp => rp.Product.Color)
        .Include(rp => rp.Product.ProductImages)
            .ThenInclude(pi => pi.Image)
        .Select(rp => rp.Product) // Select the product before applying any further includes
        .ToList();
            return products;
        }
        //public List<Product> RandomizeProductList()
        //{
        //    var products = ListProduct();
        //    Random random = new Random();
        //    products.Shuffle(random); // Randomize the order of products
        //    return products;
        //}

        public List<Product> ListProduct()
        {
            var products = _appDbContext.Products
                    .Include(p => p.Category)
                    .Include(p => p.Color)
                    .Include(p => p.Material)
                    .Include(p => p.ProductImages)
                       .ThenInclude(pi => pi.Image)
                    .Include(p => p.RoomProducts)
                       .ThenInclude(rp => rp.Room)
                       .ThenInclude(r => r.RoomType).ToList();
            return products;
        }

        public List<Product> SearchListProduct(string input)
        {
            var search = _appDbContext.Products
                .Where(rp => rp.ProductName.Contains(input) || rp.Description.Contains(input))
                .Include(rp => rp.Category)
                .Include(rp => rp.Color)
                .Include(rp => rp.Material)
                .Include(rp => rp.ProductImages)
                    .ThenInclude(pi => pi.Image)
                .Include(rp => rp.RoomProducts)
                    .ThenInclude(pi => pi.Room)
                    .ThenInclude(pi => pi.RoomType).ToList();
            return search;
                        
        }

        public async Task<bool> IsProductNameExistsAsync(string productName)
        {
            var existingProduct = await _appDbContext.Products.AnyAsync(p => p.ProductName == productName);
            return existingProduct;
        }
    

    }
}
