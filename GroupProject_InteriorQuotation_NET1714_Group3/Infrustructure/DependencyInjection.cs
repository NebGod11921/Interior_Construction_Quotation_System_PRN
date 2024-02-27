using Application;
using Application.Interfaces;
using Application.IRepositories;
using Application.Services;
using Infrastructure;
using Infrastructure.Mappers;
using Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrustructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructuresService(this IServiceCollection services, string databaseConnection)
        {
            services.AddDbContext<AppDbContext>(options =>
                options.UseSqlServer(databaseConnection));
            //return services;
            services.AddScoped<MapperConfigurationsProfile>();

            services.AddScoped<IProductRepository, ProductRepository>();
            services.AddScoped<IProductService, ProductService>();

            services.AddScoped<IRoomTypeRepository, RoomTypeRepository>();
            services.AddScoped<IRoomTypeService, RoomTypeService>();

            services.AddScoped<IUserService,UserService>();
            services.AddAutoMapper(typeof(MapperConfigurationsProfile).Assembly);

            //Service
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IAccountService, AccountService>();
            
            services.AddScoped<IUnitOfWork, UnitOfWork>();


            return services;
        }
       
    }
}

