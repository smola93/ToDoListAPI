using WebApplication_ToDoListAPI.Application.DTOs;
using WebApplication_ToDoListAPI.Application.Interfaces.Base;
using WebApplication_ToDoListAPI.Application.Models;
using WebApplication_ToDoListAPI.Presentation.Interfaces.Base;

namespace WebApplication_ToDoListAPI.Application.Services
{
    public class TodoService : ICrudBaseService<TodoDto>, IEventBaseService<TodoDto>
    {
        private readonly IBaseRepository<TodoDto> _repository;
        private readonly IEventRepository<TodoDto> _eventRepository;
        public TodoService(IBaseRepository<TodoDto> repository, IEventRepository<TodoDto> eventRepository)
        {
            _repository = repository;
            _eventRepository = eventRepository;
        }

        public async Task<TodoDto> AddAsync(TodoDto dto, CancellationToken cancellationToken = default)
        {
            return await _repository.AddAsync(dto, cancellationToken);
        }

        public async Task<TodoDto> UpdateAsync(TodoDto dto, CancellationToken cancellationToken = default)
        {
            return await _repository.UpdateAsync(dto, cancellationToken);
        }

        public async Task<bool> DeleteAsync(int id, CancellationToken cancellationToken = default)
        {
            return await _repository.DeleteAsync(id, cancellationToken);
        }

        public async Task<TodoDto> GetItemAsync(int id, CancellationToken cancellationToken = default)
        {
            return await _repository.GetItemAsync(id, cancellationToken);
        }

        public async Task<List<TodoDto>> GetListAsync(CancellationToken cancellationToken = default)
        {
            return await _repository.GetListAsync(cancellationToken);
        }

        public async Task<List<CloudEvent<TodoDto>>> GetEventListAsync(Guid lastEventId, CancellationToken cancellationToken = default)
        {
            return await _eventRepository.GetEventListAsync(lastEventId, cancellationToken);
        }
    }
}
