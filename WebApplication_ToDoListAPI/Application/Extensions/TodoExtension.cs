using WebApplication_ToDoListAPI.Application.DTOs;
using WebApplication_ToDoListAPI.Domain.Entities;

namespace WebApplication_ToDoListAPI.Application.Extensions
{
    public static class TodoExtension
    {
        public static TodoDto ToDto(this Todo todo)
        {
            return new TodoDto
            {
                Id = todo.Id,
                Name = todo.Name,
                Description = todo.Description,
                DueDate = todo.DueDate,
            };
        }

        public static Todo ToEntity(this TodoDto todoDto)
        {
            return new Todo
            {
                Id = todoDto.Id,
                Name = todoDto.Name!,
                Description = todoDto.Description!,
                DueDate = todoDto.DueDate!.Value
            };
        }
    }
}
