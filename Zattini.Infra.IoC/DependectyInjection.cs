using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Zattini.Application.Services;
using Zattini.Application.Services.Interfaces;

namespace Zattini.Infra.IoC
{
    public static class DependectyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            //var connectionString = configuration["ConnectionStrings:DefaultConnection"];
            //var connectionString = Environment.GetEnvironmentVariable("DATABASE_URL") ?? configuration["ConnectionStrings:DefaultConnection"];
            //var connectionString = configuration.GetConnectionString("Default");
            //services.AddAutoMapper(typeof(DomainToDtoMapping));
            //services.AddAutoMapper(typeof(DtoToDomainMapping));

            //services.AddDbContext<ApplicationDbContext>(
            //      options => options.UseNpgsql(connectionString)); 
            // Server=ms-sql-server; quando depender dele no Docker-Compose

            //services.AddStackExchangeRedisCache(redis =>
            //{
            //    redis.Configuration = "localhost:7006";
            //});

            //services.AddScoped<IUnitOfWork, UnitOfWork>();
            return services;
        }

        public static IServiceCollection AddServices(this IServiceCollection services, IConfiguration configuration)
        {
            //services.AddAutoMapper(typeof(DomainToDtoMapping));

            //services.AddSingleton<ICodeRandomDictionary, CodeRandomDictionary>();
            //services.AddSingleton<IQuantityAttemptChangePasswordDictionary, QuantityAttemptChangePasswordDictionary>();

            //services.AddScoped<ITransactionalEmailsApi, TransactionalEmailsApi>();

            services.AddScoped<IUserAuthenticationService, UserAuthenticationService>();
            //services.AddScoped<IUserManagementService, UserManagementService>();
            //services.AddScoped<IUserCreateAccountFunction, UserCreateAccountFunction>();
            //services.AddScoped<IAddressService, AddressService>();
            //services.AddScoped<IProductService, ProductService>();
            //services.AddScoped<ITokenGeneratorUser, TokenGeneratorUser>();
            //services.AddScoped<ICloudinaryUti, CloudinaryUti>();
            //services.AddScoped<ICacheRedisUti, CacheRedisUti>();
            //services.AddScoped<ISendEmailBrevo, SendEmailBrevo>();
            //services.AddScoped<ISendEmailUser, SendEmailUser>();
            //services.AddScoped<ITransactionalEmailApiUti, TransactionalEmailApiUti>();
            //services.AddScoped<IProductAdditionalInfoService, ProductAdditionalInfoService>();
            //services.AddScoped<IUserProductLikeService, UserProductLikeService>();
            //services.AddScoped<IProductCommentService, ProductCommentService>();
            //services.AddScoped<IProductCommentLikeService, ProductCommentLikeService>();

            //services.AddScoped<IUserSendCodeEmailDTOValidator, UserSendCodeEmailDTOValidator>();
            //services.AddScoped<IUserCreateDTOValidator, UserCreateDTOValidator>();
            //services.AddScoped<IUserUpdateProfileDTOValidator, UserUpdateProfileDTOValidator>();
            //services.AddScoped<IChangePasswordUserDTOValidator, ChangePasswordUserDTOValidator>();
            //services.AddScoped<IUserChangePasswordTokenDTOValidator, UserChangePasswordTokenDTOValidator>();
            //services.AddScoped<IAddressCreateDTOValidator, AddressCreateDTOValidator>();
            //services.AddScoped<IAddressUpdateDTOValidator, AddressUpdateDTOValidator>();
            //services.AddScoped<IProductCreateDTOValidator, ProductCreateDTOValidator>();
            //services.AddScoped<IUserProductLikeCreateDTOValidator, UserProductLikeCreateDTOValidator>();
            //services.AddScoped<IProductCommentCreateDTOValidator, ProductCommentCreateDTOValidator>();
            //services.AddScoped<IProductAdditionalInfoDTOValidator, ProductAdditionalInfoDTOValidator>();
            //services.AddScoped<IProductCommentLikeCreateDTOValidator, ProductCommentLikeCreateDTOValidator>();

            return services;
        }
    }
}
