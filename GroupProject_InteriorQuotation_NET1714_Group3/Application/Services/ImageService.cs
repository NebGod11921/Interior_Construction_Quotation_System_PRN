using Application.Interfaces;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Entities;
namespace Application.Services
{
    public class ImageService : IImageService
    {
        private readonly IUnitOfWork _unit;
        private readonly IMapper _mapper;
        public ImageService(IUnitOfWork unit, IMapper mapper)
        {
            _unit = unit;
            _mapper = mapper;
        }

        public async Task<int> AddImageToDatabaseAsync(string imagepath)
        {
            try
            {
                var image = new Image { ImageName = imagepath };
                int? idimage = await _unit.ImageRepo.AddImage(image);
                if(idimage > 0) {
                 return image.Id;
                }
                else
                {
                    throw new Exception("Failed to add image.");
                }
               
            }catch(Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<bool> AddProductImage(int productid, int imageid)
        {
            var productimage = new ProductImage
            {
                ProductId = productid,
                ImageId = imageid
            };
            await _unit.ImageRepo.AddProductImage(productimage);
            return true;
        }
    }
}
