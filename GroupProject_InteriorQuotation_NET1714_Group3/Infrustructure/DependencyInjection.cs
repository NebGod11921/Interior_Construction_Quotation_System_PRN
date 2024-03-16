using Application;
using Application.Interfaces;
using Application.IRepositories;
using Application.Services;
using Infrastructure;
using Infrastructure.Mappers;
using Infrastructure.Repositories;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
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
            {

                options.UseSqlServer(databaseConnection);
                options.UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);
            });
            //return services;
            services.AddScoped<MapperConfigurationsProfile>();

            services.AddScoped<IProductRepository, ProductRepository>();
            services.AddScoped<IProductService, ProductService>();

            services.AddScoped<IRoomTypeRepository, RoomTypeRepository>();
            services.AddScoped<IRoomTypeService, RoomTypeService>();

            services.AddScoped<IUserService,UserService>();
            services.AddAutoMapper(typeof(MapperConfigurationsProfile).Assembly);
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IImageRepo, ImageRepocs>();
            services.AddScoped<IImageService, ImageService>();
            services.AddScoped<IProductRepository, ProductRepository>();
            services.AddScoped<IProductService, ProductService>();
            services.AddScoped<IRoomRepo, RoomRepo>();
            services.AddScoped<IRoomService, RoomService>();

            services.AddSingleton<ICartService, CartService>();
            services.AddScoped<IQuotationRepository, QuotationRepository>();
            services.AddScoped<IQuotationService, QuotationService>();

            //Service
            services.AddScoped<ICategoryService, CategoryService>();
            services.AddScoped<ICateRepo, CateRepo>();
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IAccountService, AccountService>();
            services.AddScoped<IRoomService, RoomService>();
            services.AddScoped<IRoomRepository, RoomRepository>();

            services.AddScoped<IColorRepo, ColorRepo>();
            services.AddScoped<IColorService, ColorService>();
            services.AddScoped<IMaterialService, MaterialService>();
            services.AddScoped<IMaterialRepo, MaterialRepo>();
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.Configure<FormOptions>(options =>
            {
                options.MultipartBodyLengthLimit = long.MaxValue;
            });

            //services.AddScoped<IImage, ProductService>();


            return services;
        }
       
    }
}

