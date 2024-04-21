using FetchToDos.Services.Interfaces;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System.Text.Json;
using WebApplication_ToDoListAPI.Application.DTOs;
using WebApplication_ToDoListAPI.Application.Models;

namespace FetchToDos.Services
{
    public class FetchService : IFetchService
    {
        private readonly ITodoService _todoService;
        private readonly IConfiguration _configuration;
        private readonly ILogger<FetchService> _logger;

        public FetchService(ITodoService todoService, IConfiguration configuration, ILogger<FetchService> logger)
        {
            _todoService = todoService;
            _configuration = configuration;
            _logger = logger;
        }

        public async Task FetchTodos()
        {   
            var lastEventId = Guid.Empty;
            string filePath = _configuration.GetValue<string>("JsonFilePath")!;

            if (!File.Exists(filePath))
            {
                File.Create(filePath);
            }
            else
            {
                var lastObj = File.ReadLines(filePath).LastOrDefault();

                if (lastObj != null)
                {
                    var options = new JsonSerializerOptions
                    {
                        PropertyNameCaseInsensitive = true
                    };

                    var todos = JsonSerializer.Deserialize<CloudEvent<TodoDto>>(lastObj, options)!;

                    lastEventId = todos.Id;
                }
            }

            while (true)
            {
                try
                {
                    var events = await _todoService.GetTodosEvents(lastEventId);

                    if (events.Count == 0)
                    {
                        await Task.Delay(5000);
                        continue;
                    }

                    lastEventId = events.OrderBy(x => x.Time).Last().Id;
                    foreach (var cloudEvent in events)
                    {
                        var serialized = JsonSerializer.Serialize(cloudEvent);

                        var fileInfo = new FileInfo(filePath);

                        if (fileInfo.Length == 0)
                        {
                            File.AppendAllText(filePath, serialized);
                        }
                        else
                        {
                            File.AppendAllText(filePath, "," + Environment.NewLine + serialized);
                        }
                            
                        _logger.LogInformation($"New event appended into the json file: {serialized}");
                    }
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, ex.Message);
                    await Task.Delay(5000);
                    continue;
                }
            }
        }
    }
}
