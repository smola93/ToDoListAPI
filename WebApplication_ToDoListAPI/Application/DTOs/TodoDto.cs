using WebApplication_ToDoListAPI.Application.Interfaces.Base;

namespace WebApplication_ToDoListAPI.Application.DTOs
{
    public class TodoDto : IDto
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
        public DateTime? DueDate { get; set; }
    }
}
