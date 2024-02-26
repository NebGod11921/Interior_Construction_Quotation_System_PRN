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
        public UnitOfWork(AppDbContext dbContext, 
            IUserRepository userRepository, 
            IProductRepository productRepo,
             IRoomTypeRepository roomTypeRepository
            )
        {
            _dbContext = dbContext;
            _userRepository = userRepository;
            _productRepo = productRepo;
            _roomTypeRepository = roomTypeRepository;
        }

        public IUserRepository UserRepository => _userRepository;

        public IProductRepository ProductRepository => _productRepo;

        //public IRoomTypeRepository RoomRepository => _roomRepository;

        public IRoomTypeRepository RoomTypeRepository => _roomTypeRepository;

        public async Task<int> SaveChangeAsync()
        {
            return await _dbContext.SaveChangesAsync();
        }
    }
}
