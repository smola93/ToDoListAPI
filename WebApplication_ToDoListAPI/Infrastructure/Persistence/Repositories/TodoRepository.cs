using Microsoft.EntityFrameworkCore;
using WebApplication_ToDoListAPI.Application.DTOs;
using WebApplication_ToDoListAPI.Application.Extensions;
using WebApplication_ToDoListAPI.Application.Interfaces.Base;
using WebApplication_ToDoListAPI.Application.Models;
using WebApplication_ToDoListAPI.Domain.Entities;

namespace WebApplication_ToDoListAPI.Infrastructure.Persistence.Repositories
{
    public class TodoRepository : IBaseRepository<TodoDto>, IEventRepository<TodoDto>
    {
        private readonly Context _context;
        private readonly IConfiguration _configuration;

        public TodoRepository(Context context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
        }

        public async Task<TodoDto> AddAsync(TodoDto dto, CancellationToken cancellationToken = default)
        {
            var entity = dto.ToEntity();

            await _context.AddAsync(entity, cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);

            return entity.ToDto();
        }

        public async Task<TodoDto> UpdateAsync(TodoDto dto, CancellationToken cancellationToken = default)
        {
            var entity = await _context.Todos.FirstOrDefaultAsync(x => x.Id == dto.Id, cancellationToken);

            if (entity == null)
            {
                throw new NullReferenceException($"Could not found entity of provided id: {dto.Id}");
            }

            entity.Name = !string.IsNullOrEmpty(dto.Name) ? dto.Name : entity.Name;
            entity.Description = !string.IsNullOrEmpty(dto.Description) ? dto.Description : entity.Description;
            entity.DueDate = dto.DueDate != null && dto.DueDate != DateTime.MinValue ? dto.DueDate.Value : entity.DueDate;

            _context.Update(entity);
            await _context.SaveChangesAsync(cancellationToken);

            return entity.ToDto();
        }

        public async Task<bool> DeleteAsync(int id, CancellationToken cancellationToken = default)
        {
            _context.Remove(_context.Todos.Find(id) ?? 
                throw new NullReferenceException($"Could not found entity of provided id: {id}"));
            await _context.SaveChangesAsync(cancellationToken);

            return true;
        }

        public async Task<TodoDto> GetItemAsync(int id, CancellationToken cancellationToken = default)
        {
            var entity = await _context.Todos.FirstOrDefaultAsync(x => x.Id == id, cancellationToken);

            return entity?.ToDto() ?? 
                throw new NullReferenceException($"Could not found entity of provided id: {id}");
        }

        public async Task<List<TodoDto>> GetListAsync(CancellationToken cancellationToken = default)
        {
            return await _context.Todos.Select(x => x.ToDto()).ToListAsync(cancellationToken);
        }

        public async Task<List<CloudEvent<TodoDto>>> GetEventListAsync(Guid lastEventId, CancellationToken cancellationToken = default)
        {
            if (lastEventId == Guid.Empty)
            {
                return await _context.Todos.Select(x => new CloudEvent<TodoDto>
                {
                    Id = x.EventGuid,
                    SpecVersion = "1.0",
                    Type = $"{_configuration.GetValue<string>("CloudEvent:TypeBase")}.{nameof(Todo)}.GetEventList",
                    Source = $"{_configuration.GetValue<string>("CloudEvent:SourceBaseAddress")}/{nameof(Todo)}/GetEventList",
                    Time = x.CreatedDate,
                    Data = x.ToDto()
                }).ToListAsync(cancellationToken);
            }
            else
            {
                var lastEvent = await _context.Todos.SingleOrDefaultAsync(x => x.EventGuid == lastEventId, cancellationToken);

                if (lastEvent == null)
                {
                    throw new NullReferenceException($"Could not found entity of provided id: {lastEventId}");
                }

                return await _context.Todos.Where(x => x.CreatedDate > lastEvent.CreatedDate).Select(x => new CloudEvent<TodoDto>
                {
                    Id = x.EventGuid,
                    SpecVersion = "1.0",
                    Type = $"{_configuration.GetValue<string>("CloudEvent:TypeBase")}.{nameof(Todo)}.GetEventList",
                    Source = $"{_configuration.GetValue<string>("CloudEvent:SourceBaseAddress")}/{nameof(Todo)}/GetEventList",
                    Time = x.CreatedDate,
                    Data = x.ToDto()
                }).ToListAsync(cancellationToken);
            }
        }
    }
}
