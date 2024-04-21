using WebApplication_ToDoListAPI.Application.DTOs;
using WebApplication_ToDoListAPI.Application.Models;

namespace FetchToDos.Services.Interfaces
{
    public interface ITodoService
    {
        Task<List<CloudEvent<TodoDto>>> GetTodosEvents(Guid lastEventId);
    }
}