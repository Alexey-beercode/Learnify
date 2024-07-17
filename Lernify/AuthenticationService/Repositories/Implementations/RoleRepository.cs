using AuthenticationService.Infrastructure.Database;
using AuthenticationService.Models.Entities;
using AuthenticationService.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace AuthenticationService.Repositories.Implementations;

public class RoleRepository:IRoleRepository
{
    private readonly AuthDbContext _dbContext;

    public RoleRepository(AuthDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task CreateAsync(Role role, CancellationToken cancellationToken = default)
    {
        await _dbContext.Roles.AddAsync(role,cancellationToken);
        await _dbContext.SaveChangesAsync(cancellationToken);
    }

    public async Task<Role> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        return await _dbContext.Roles.FirstOrDefaultAsync(r => r.Id == id && !r.IsDeleted,cancellationToken) ?? throw new InvalidOperationException($"Role with id : {id} are not found");
    }

    public async Task<IEnumerable<Role>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        return await _dbContext.Roles.Where(r=>!r.IsDeleted).ToListAsync(cancellationToken);
    }

    public async Task DeleteAsync(Role role, CancellationToken cancellationToken = default)
    {
        role.IsDeleted = true;
        _dbContext.Roles.Update(role);
       await _dbContext.SaveChangesAsync(cancellationToken);
    }

    public async Task UpdateAsync(Role role, CancellationToken cancellationToken = default)
    {
        _dbContext.Roles.Update(role);
        await _dbContext.SaveChangesAsync(cancellationToken);
    }

    public async Task<IEnumerable<Role>> GetRolesByUserId(Guid userId, CancellationToken cancellationToken = default)
    {
        return await _dbContext.UserRoles.Where(ur => ur.UserId == userId).Select(ur=>ur.Role).ToListAsync(cancellationToken);
    }
}