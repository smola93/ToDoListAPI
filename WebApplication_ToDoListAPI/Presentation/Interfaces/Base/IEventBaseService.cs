using WebApplication_ToDoListAPI.Application.Models;

namespace WebApplication_ToDoListAPI.Presentation.Interfaces.Base
{
    public interface IEventBaseService<T> where T : class
    {
        Task<List<CloudEvent<T>>> GetEventListAsync(Guid lastEventId, CancellationToken cancellationToken = default);
    }
}
