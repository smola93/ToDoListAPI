using FetchToDos.Services.Interfaces;
using System.Text.Json;
using WebApplication_ToDoListAPI.Application.DTOs;
using WebApplication_ToDoListAPI.Application.Models;

namespace FetchToDos.Services
{
    public class TodoService : ITodoService
    {
        private readonly HttpClient _httpClient;
        public TodoService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<List<CloudEvent<TodoDto>>> GetTodosEvents(Guid lastEventId)
        {
            var query = string.Empty;

            if (lastEventId != Guid.Empty)
            {
                query = $"?lastEventId={lastEventId}";
            }

            var response = await _httpClient.GetAsync($"/Todo/GetEventList{query}");
            response.EnsureSuccessStatusCode();
            var content = await response.Content.ReadAsStringAsync();

            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            };

            return JsonSerializer.Deserialize<List<CloudEvent<TodoDto>>>(content, options)!;
        }
    }
}
