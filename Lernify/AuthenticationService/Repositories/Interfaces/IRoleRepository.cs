using AuthenticationService.Models.Entities;

namespace AuthenticationService.Repositories.Interfaces;

public interface IRoleRepository:IBaseRepository<Role>
{
    Task<IEnumerable<Role>> GetRolesByUserId(Guid userId, CancellationToken cancellationToken = default);
    Task SetRoleToUser(Guid userId, Guid roleId, CancellationToken cancellationToken = default);
    Task RemoveRoleFromUser(Guid userId, Guid roleId, CancellationToken cancellationToken = default);
}