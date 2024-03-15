using Application.IRepositories;
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
    public class ImageRepocs : GenericRepository<Image>, IImageRepo
    {
        private readonly AppDbContext _appDbContext;
        public ImageRepocs(AppDbContext appDbContext) : base(appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public async Task<int?> AddImage(Image image)
        {
            try
            {
                await _appDbContext.AddAsync(image);
                int rowsAffected = await _appDbContext.SaveChangesAsync();

                if (rowsAffected > 0)
                {
                    return image.Id;
                }
                else
                {
                    return null;
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Failed to add image to database.", ex);
            }
        }

        public async Task AddProductImage(ProductImage productImage)
        {
            await _appDbContext.AddAsync(productImage);
            await _appDbContext.SaveChangesAsync();
        }
    }
}
