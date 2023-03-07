using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using TicTacToe.DataModels.DAL;
using TicTacToe.Services;

namespace TIcTackToe.BLL.Services
{
    public static class DIServices
    {
        public static IServiceCollection AddServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<EfContext>(cfg => cfg.UseSqlServer(configuration.GetConnectionString("default")));
            services.AddScoped<PlayerService>();
            services.AddScoped<RoomService>();
            return services;
        }
    }
}
