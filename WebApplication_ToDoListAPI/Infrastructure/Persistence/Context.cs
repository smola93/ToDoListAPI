using Microsoft.EntityFrameworkCore;
using WebApplication_ToDoListAPI.Domain.Entities;

namespace WebApplication_ToDoListAPI.Infrastructure.Persistence
{
    public class Context : DbContext
    {
        public Context(DbContextOptions<Context> options) : base(options) { }

        public DbSet<Todo> Todos { get; set; }
    }
}