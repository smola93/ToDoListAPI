using WebApplication_ToDoListAPI.Application.Models;

namespace WebApplication_ToDoListAPI.Application.Interfaces.Base
{
    public interface IEventRepository<T> where T : class
    {
        Task<List<CloudEvent<T>>> GetEventListAsync(Guid lastEventId, CancellationToken cancellationToken = default);
    }
}
