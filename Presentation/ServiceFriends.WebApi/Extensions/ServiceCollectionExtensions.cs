using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using ServiceFriends.DataEntityFramework.Repositories;
using ServiceFriends.DataEntityFramework;
using ServiceFriends.Domain.Interfaces;
using ServiceFriends.WebApi.Filters;
using ServiceFriends.Domain.Services;
using FluentValidation;

namespace ServiceFriends.WebApi.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static void ConfigureServices
            (this IServiceCollection services, IConfiguration configuration)
        {
            services.AddInfrastructure(configuration);
            services.AddApplicationComponents(configuration);
        }

        public static void ConfigureMiddleware(this WebApplication app)
        {
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();
            app.UseAuthorization();
            app.MapControllers();
        }

        private static void AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddValidatorsFromAssemblyContaining<Program>(ServiceLifetime.Scoped);
            services.AddControllers(options =>
            {
                //options.Filters.Add<CentralizedExceptionHandlingFilter>();
            });

            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen(options =>
            {
                options.SwaggerDoc("v1", new OpenApiInfo { Title = "FriendsService", Version = "v1" });
            });
            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
            services.AddHttpClient();
        }

        private static void AddApplicationComponents(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddRepositories(configuration);
            services.AddDomainServices();
        }

        private static void AddRepositories(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<AppDbContext>(options =>
                         options.UseNpgsql(configuration.GetConnectionString("Postgres")));

            services.AddScoped(typeof(IRepositoryEF<>), typeof(EFRepository<>));
            services.AddScoped<IFriendShipRepository, FriendShipRepository>();
            services.AddScoped<IReceivedFriendRequestRepository, ReceivedFriendRequestRepository>();
            services.AddScoped<ISentFriendRequestRepository, SentFriendRequestRepository>();
        }

        private static void AddDomainServices(this IServiceCollection services)
        {
            services.AddScoped<FriendShipService>();
        }
    }
}
