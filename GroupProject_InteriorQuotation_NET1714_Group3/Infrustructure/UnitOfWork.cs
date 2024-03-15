using Application;
using Application.IRepositories;
using Infrustructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly AppDbContext _dbContext;

        private readonly IUserRepository _userRepository;
        private readonly IProductRepository _productRepo;
        private readonly IRoomTypeRepository _roomTypeRepository;
        private readonly IQuotationRepository _quotationRepository;
		//private readonly IProductImageRepo _productImageRepo;
		//private readonly IImageRepo _imageRepo;

        private readonly IRoomRepository _roomRepository;

		public UnitOfWork(AppDbContext dbContext, 
            IUserRepository userRepository, 
            IProductRepository productRepo,
             IRoomTypeRepository roomTypeRepository,
             IQuotationRepository quotationRepository,
             IRoomRepository roomRepository)
        {
            _dbContext = dbContext;
            _userRepository = userRepository;
            _productRepo = productRepo;
            _roomTypeRepository = roomTypeRepository;
            _quotationRepository = quotationRepository;
            _roomRepository = roomRepository;
        }

        public IUserRepository UserRepository => _userRepository;
        public IQuotationRepository QuotationRepository => _quotationRepository;

        public IProductRepository ProductRepository => _productRepo;

        public IRoomTypeRepository RoomTypeRepository => _roomTypeRepository;
        public IRoomRepository RoomRepository => _roomRepository;

        public async Task<int> SaveChangeAsync() 
        {
            return await _dbContext.SaveChangesAsync();
        }
    }
}
