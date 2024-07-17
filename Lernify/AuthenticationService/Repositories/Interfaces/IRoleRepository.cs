using AuthenticationService.Models.Entities;

namespace AuthenticationService.Repositories.Interfaces;

public interface IRoleRepository:IBaseRepository<Role>
{
    Task<IEnumerable<Role>> GetRolesByUserId(Guid userId, CancellationToken cancellationToken = default);
}