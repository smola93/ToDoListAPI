using WebApplication_ToDoListAPI.Application.DTOs;
using WebApplication_ToDoListAPI.Application.Services;
using WebApplication_ToDoListAPI.Presentation.Interfaces.Base;

namespace WebApplication_ToDoListAPI.Application.Extensions
{
    public static class IServiceCollectionExtensions
    {
        public static void AddApplicationLayer(this IServiceCollection services)
        {
            services.AddScoped<ICrudBaseService<TodoDto>, TodoService>();
            services.AddScoped<IEventBaseService<TodoDto>, TodoService>();
        }
    }
}
