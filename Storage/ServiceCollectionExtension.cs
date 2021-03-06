﻿using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Storage.Repositories;
using Storage.Repositories.Interfaces;

namespace Storage
{
    public static class ServiceCollectionExtension
    {
        public static IServiceCollection AddStorageLibraryServices(this IServiceCollection services, string connectionString)
        {
            services.AddDbContext<JudgeContext>(
                options =>
                {
                    options.UseSqlServer(connectionString);
                });

            services.AddScoped<IGameRepository, GameRepository>();
            services.AddScoped<IItemRepository, ItemRepository>();
            services.AddScoped<ISummaryRepository, SummaryRepository>();
            services.AddScoped<IRatingRepository, RatingRepository>();
            services.AddScoped<ICategoryRepository, CategoryRepository>();
            services.AddScoped<ICategoryRatingRepository, CategoryRatingRepository>();
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IPlayerProfileRepository, PlayerProfileRepository>();
            return services;
        }
    }
}
