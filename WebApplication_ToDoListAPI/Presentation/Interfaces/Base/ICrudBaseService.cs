using WebApplication_ToDoListAPI.Application.Interfaces.Base;

namespace WebApplication_ToDoListAPI.Presentation.Interfaces.Base
{
    public interface ICrudBaseService<T> where T : IDto
    {
        Task<T> AddAsync(T dto, CancellationToken cancellationToken = default);
        Task<T> UpdateAsync(T dto, CancellationToken cancellationToken = default);
        Task<bool> DeleteAsync(int id, CancellationToken cancellationToken = default);
        Task<List<T>> GetListAsync(CancellationToken cancellationToken = default);
        Task<T> GetItemAsync(int id, CancellationToken cancellationToken = default);
    }
}
