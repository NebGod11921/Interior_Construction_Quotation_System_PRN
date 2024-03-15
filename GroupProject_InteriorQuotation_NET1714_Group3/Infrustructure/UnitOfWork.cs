﻿using Application;
using Application.IRepositories;
using Domain.Entities;
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
		private readonly IColorRepo _colorRepo;
        private readonly IMaterialRepo _materialRepo;
        private readonly IRoomRepo _roomRepo;
        private readonly ICateRepo _cateRepo;
        private readonly IImageRepo _imageRepo;
		public UnitOfWork(AppDbContext dbContext, 
            IUserRepository userRepository, 
            IProductRepository productRepo,
             IRoomTypeRepository roomTypeRepository,
             IColorRepo colorRepo, IMaterialRepo materialRepo, IRoomRepo roomRepo, ICateRepo cateRepo, IImageRepo imageRepo )
        {
            _dbContext = dbContext;
            _userRepository = userRepository;
            _productRepo = productRepo;
            _roomTypeRepository = roomTypeRepository;
            _colorRepo = colorRepo;
            _materialRepo = materialRepo;
            _roomRepo = roomRepo;
            _cateRepo = cateRepo;
            _imageRepo = imageRepo;
        }

        public IUserRepository UserRepository => _userRepository;

        public IProductRepository ProductRepository => _productRepo;
        public IRoomTypeRepository RoomTypeRepository => _roomTypeRepository;

        public IColorRepo ColorRepository => _colorRepo;
        public IMaterialRepo MaterialRepo => _materialRepo;

        public IRoomRepo RoomRepo => _roomRepo;

        public ICateRepo CateRepo => _cateRepo;

        public IImageRepo ImageRepo => _imageRepo;

        public void AddProductImage(ProductImage productImage)
        {
            _dbContext.ProductImage.Add(productImage);
        }

        public async Task<int> SaveChangeAsync() 
        {
            return await _dbContext.SaveChangesAsync();
        }
    }
}
