namespace AuthenticationService.Repositories.Interfaces;

public interface IBaseRepository<T>
{
    Task CreateAsync(T entity, CancellationToken cancellationToken = default);
    Task<T> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
    Task<IEnumerable<T>> GetAllAsync(CancellationToken cancellationToken = default);
    Task DeleteAsync(T role, CancellationToken cancellationToken = default);
    Task UpdateAsync(T role, CancellationToken cancellationToken = default);
}