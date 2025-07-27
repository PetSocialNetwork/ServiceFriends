using ServiceFriends.WebApi.Extensions;

namespace ServiceFriends.WebApi
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            builder.Services.ConfigureServices(builder.Configuration);
            var app = builder.Build();
            app.ConfigureMiddleware();
            app.Run();
        }
    }
}