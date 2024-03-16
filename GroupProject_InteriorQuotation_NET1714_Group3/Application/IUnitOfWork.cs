using Application.IRepositories;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application
{
    public interface IUnitOfWork
    {
        public IProductRepository ProductRepository { get; }
        public IUserRepository UserRepository { get; }
        public IRoomTypeRepository RoomTypeRepository { get; }
        public IColorRepo ColorRepository { get; }
        public IMaterialRepo MaterialRepo { get; } 
        public IRoomRepo RoomRepo { get; }
        public ICateRepo CateRepo { get; }
        public IImageRepo ImageRepo { get; }
        public void AddProductImage(ProductImage productImage);
        //.public IImageRepo _imageRepo { get; }
        public Task<int> SaveChangeAsync();
		//public IProductImageRepo ProductImageRepo { get; }
		//public IImageRepo ImageRepo { get; }
        public IQuotationRepository QuotationRepository { get; }
        public IRoomRepository RoomRepository { get; }

		//public Task<int> SaveChangeAsync();

    }
}
