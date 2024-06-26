﻿using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using RMS.Business.Services.Abstracts;
using RMS.Business.Services.Concretes;
using RMS.Data.Repositories.Abstractions;
using RMS.Data.Repositories.Implementations;

namespace RMS.Business
{
    public static class ServiceRegistration
    {
        public static void AddServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddScoped<ITableService, TableService>();
            services.AddScoped<ISlideService, SlideService>();
            services.AddScoped<ICategoryService, CategoryService>();

            services.AddScoped<ITableRepository, TableRepository>();
            services.AddScoped<ISlideRepository, SlideRepository>();
            services.AddScoped<ICategoryRepository, CategoryRepository>();

        }
    }
}
