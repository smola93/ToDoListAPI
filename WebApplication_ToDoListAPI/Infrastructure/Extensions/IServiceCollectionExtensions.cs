using Microsoft.EntityFrameworkCore;
using WebApplication_ToDoListAPI.Application.DTOs;
using WebApplication_ToDoListAPI.Application.Interfaces.Base;
using WebApplication_ToDoListAPI.Infrastructure.Persistence;
using WebApplication_ToDoListAPI.Infrastructure.Persistence.Repositories;

namespace WebApplication_ToDoListAPI.Infrastructure.Extensions
{
    public static class IServiceCollectionExtensions
    {
        public static void AddInfrastructureLayer(this IServiceCollection services, WebApplicationBuilder builder)
        {
            var connectionString = builder.Configuration.GetConnectionString("ToDoApiDatabase");
            builder.Services.AddDbContext<Context>(options => options.UseNpgsql(connectionString));

            services.AddScoped<IBaseRepository<TodoDto>, TodoRepository>();
            services.AddScoped<IEventRepository<TodoDto>, TodoRepository>();
        }
    }
}
