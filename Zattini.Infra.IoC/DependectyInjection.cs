using brevo_csharp.Api;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Zattini.Application.DTOs.Validations.Interfaces;
using Zattini.Application.DTOs.Validations.UserAddressValidator;
using Zattini.Application.DTOs.Validations.UserValidator;
using Zattini.Application.Mappings;
using Zattini.Application.Services;
using Zattini.Application.Services.Interfaces;
using Zattini.Domain.Authentication;
using Zattini.Domain.Repositories;
using Zattini.Infra.Data.Authentication;
using Zattini.Infra.Data.Context;
using Zattini.Infra.Data.Repositories;
using Zattini.Infra.Data.UtilityExternal;
using Zattini.Infra.Data.UtilityExternal.Interface;

namespace Zattini.Infra.IoC
{
    public static class DependectyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            //var connectionString = configuration["ConnectionStrings:DefaultConnection"];
            var connectionString = Environment.GetEnvironmentVariable("DATABASE_URL") ?? configuration["ConnectionStrings:DefaultConnection"];
            //var connectionString = configuration.GetConnectionString("Default");

            services.AddAutoMapper(typeof(DomainToDtoMapping));
            services.AddAutoMapper(typeof(DtoToDomainMapping));

            services.AddDbContext<ApplicationDbContext>(
                  options => options.UseNpgsql(connectionString));
            //Server = ms - sql - server; quando depender dele no Docker - Compose

            services.AddStackExchangeRedisCache(redis =>
            {
                redis.Configuration = "localhost:7006";
            });

            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IUserAddressRepository, UserAddressRepository>();
            return services;
        }

        public static IServiceCollection AddServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddAutoMapper(typeof(DomainToDtoMapping));

            //services.AddSingleton<ICodeRandomDictionary, CodeRandomDictionary>();
            //services.AddSingleton<IQuantityAttemptChangePasswordDictionary, QuantityAttemptChangePasswordDictionary>();

            services.AddScoped<ITransactionalEmailsApi, TransactionalEmailsApi>();

            services.AddScoped<IUserAuthenticationService, UserAuthenticationService>();
            services.AddScoped<IUserManagementService, UserManagementService>();
            services.AddScoped<IUserCreateAccountFunction, UserCreateAccountFunction>();
            services.AddScoped<IUserAddressService, UserAddressService>();
            services.AddScoped<ICloudinaryUti, CloudinaryUti>();
            services.AddScoped<ICacheRedisUti, CacheRedisUti>();
            services.AddScoped<ISendEmailBrevo, SendEmailBrevo>();
            services.AddScoped<ISendEmailUser, SendEmailUser>();
            services.AddScoped<ITransactionalEmailApiUti, TransactionalEmailApiUti>();
            services.AddScoped<ITokenGeneratorUser, TokenGeneratorUser>();

            services.AddScoped<IUserAddressCreateDTOValidator, UserAddressCreateDTOValidator>();
            services.AddScoped<IUserCreateDTOValidator, UserCreateDTOValidator>();
            //services.AddScoped<IUserUpdateProfileDTOValidator, UserUpdateProfileDTOValidator>();

            return services;
        }
    }
}
