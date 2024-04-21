using Microsoft.AspNetCore.Mvc;
using WebApplication_ToDoListAPI.Application.DTOs;
using WebApplication_ToDoListAPI.Application.Models;
using WebApplication_ToDoListAPI.Presentation.Interfaces.Base;

namespace WebApplication_ToDoListAPI.Presentation.Controllers
{
    [ApiController]
    [Route("[controller]/[action]")]
    public class TodoController : ControllerBase
    {
        private readonly ICrudBaseService<TodoDto> _todoService;
        private readonly IEventBaseService<TodoDto> _todoEventService;
        public TodoController(ICrudBaseService<TodoDto> todoService, IEventBaseService<TodoDto> todoEventService)
        {
            _todoService = todoService;
            _todoEventService = todoEventService;
        }

        [HttpPost(Name = "Add")]
        public async Task<TodoDto> Add(TodoDto dto, CancellationToken cancellationToken)
        {
            return await _todoService.AddAsync(dto, cancellationToken);
        }

        [HttpPut(Name = "Update")]
        public async Task<TodoDto> Update(TodoDto dto, CancellationToken cancellationToken)
        {
            return await _todoService.UpdateAsync(dto, cancellationToken);
        }

        [HttpDelete(Name = "Delete")]
        public async Task<bool> Delete(int id, CancellationToken cancellationToken)
        {
            return await _todoService.DeleteAsync(id, cancellationToken);
        }

        [HttpGet(Name = "GetList")]
        public async Task<List<TodoDto>> GetList(CancellationToken cancellationToken)
        {
            return await _todoService.GetListAsync(cancellationToken);
        }

        [HttpGet(Name = "GetItem")]
        public async Task<TodoDto> GetItem(int id, CancellationToken cancellationToken)
        {
            return await _todoService.GetItemAsync(id, cancellationToken);
        }

        [HttpGet(Name = "GetEventList")]
        public async Task<List<CloudEvent<TodoDto>>> GetEventList([FromQuery] Guid lastEventId, CancellationToken cancellationToken)
        {
            return await _todoEventService.GetEventListAsync(lastEventId, cancellationToken);
        }
    }
}
