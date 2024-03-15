using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces
{
    public interface IImageService
    {
        Task<int> AddImageToDatabaseAsync(string imagepath);
        Task<bool> AddProductImage(int productid, int imageid);
    }
}
