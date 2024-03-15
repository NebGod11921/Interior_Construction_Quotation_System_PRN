using Application.IRepositories;
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
		//public IProductImageRepo ProductImageRepo { get; }
		//public IImageRepo ImageRepo { get; }
        public IQuotationRepository QuotationRepository { get; }
        public IRoomRepository RoomRepository { get; }

		public Task<int> SaveChangeAsync();

    }
}
