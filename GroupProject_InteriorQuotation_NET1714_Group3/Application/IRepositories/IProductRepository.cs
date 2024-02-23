﻿using Application.Repositories;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.IRepositories
{
    public interface IProductRepository : IGenericRepository<Product>
    {
        public List<Product> GetProductsInRoom(Guid roomId);
        

    }
}