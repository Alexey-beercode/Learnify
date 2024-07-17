namespace AuthenticationService.Repositories.Interfaces;

public interface IBaseRepository<T>
{
    Task CreateAsync(T user, CancellationToken cancellationToken = default);
    Task<T> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
    Task<IEnumerable<T>> GetAllAsync(CancellationToken cancellationToken = default);
    Task DeleteAsync(T user, CancellationToken cancellationToken = default);
    Task UpdateAsync(T user, CancellationToken cancellationToken = default);
}