using WebApplication_ToDoListAPI.Application.DTOs;
using WebApplication_ToDoListAPI.Application.Models;

namespace ToDoListAPI.Tests.Unit.Mocks
{
    public static class TodoMocks
    {
        public static Task<List<TodoDto>> GetTestTodos()
        {
            return Task.FromResult(new List<TodoDto>
            {
                new TodoDto { Id = 1, Name = "Test 1", Description = "Test 1 Description", DueDate = new DateTime(DateTime.UtcNow.Year, DateTime.UtcNow.Month, 5) },
                new TodoDto { Id = 2, Name = "Test 2", Description = "Test 2 Description", DueDate = new DateTime(DateTime.UtcNow.Year, DateTime.UtcNow.Month, 10) },
                new TodoDto { Id = 3, Name = "Test 3", Description = "Test 3 Description", DueDate = new DateTime(DateTime.UtcNow.Year, DateTime.UtcNow.Month, 15) }
            });
        }

        public static Task<List<CloudEvent<TodoDto>>> GetTestCloudEventTodos()
        {
            return Task.FromResult(new List<CloudEvent<TodoDto>>
            {
                new CloudEvent<TodoDto> { Id = Guid.NewGuid(), Source = "Test", SpecVersion = "1.0", Type = "Test", Data = new TodoDto { Id = 1, Name = "Test 1", Description = "Test 1 Description", DueDate = new DateTime(DateTime.UtcNow.Year, DateTime.UtcNow.Month, 5) } },
                new CloudEvent<TodoDto> { Id = Guid.NewGuid(), Source = "Test", SpecVersion = "1.0", Type = "Test", Data = new TodoDto { Id = 2, Name = "Test 2", Description = "Test 2 Description", DueDate = new DateTime(DateTime.UtcNow.Year, DateTime.UtcNow.Month, 10) } },
                new CloudEvent<TodoDto> { Id = Guid.NewGuid(), Source = "Test", SpecVersion = "1.0", Type = "Test", Data = new TodoDto { Id = 3, Name = "Test 3", Description = "Test 3 Description", DueDate = new DateTime(DateTime.UtcNow.Year, DateTime.UtcNow.Month, 15) }}
            });
        }
    }
}
