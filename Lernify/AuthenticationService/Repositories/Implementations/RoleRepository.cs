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

    public async Task CreateAsync(Role user, CancellationToken cancellationToken = default)
    {
        await _dbContext.Roles.AddAsync(user,cancellationToken);
        await _dbContext.SaveChangesAsync(cancellationToken);
    }

    public async Task<Role> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        return await _dbContext.Roles.FirstOrDefaultAsync(r => r.Id == id && !r.IsDeleted, cancellationToken);
    }

    public async Task<IEnumerable<Role>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        return await _dbContext.Roles.Where(r=>!r.IsDeleted).ToListAsync(cancellationToken);
    }

    public async Task DeleteAsync(Role user, CancellationToken cancellationToken = default)
    {
        user.IsDeleted = true;
        _dbContext.Roles.Update(user);
       await _dbContext.SaveChangesAsync(cancellationToken);
    }

    public async Task UpdateAsync(Role user, CancellationToken cancellationToken = default)
    {
        _dbContext.Roles.Update(user);
        await _dbContext.SaveChangesAsync(cancellationToken);
    }

    public async Task<IEnumerable<Role>> GetRolesByUserId(Guid userId, CancellationToken cancellationToken = default)
    {
        return await _dbContext.UserRoles
            .Where(ur => ur.UserId == userId)
            .Select(ur=>ur.Role)
            .ToListAsync(cancellationToken);
    }

    public async Task SetRoleToUser(Guid userId, Guid roleId, CancellationToken cancellationToken = default)
    {
        var user = await _dbContext.Users.FirstOrDefaultAsync(u => u.Id == userId, cancellationToken);
        var role = await _dbContext.Roles.FirstOrDefaultAsync(r => r.Id == roleId, cancellationToken);

        if (user == null || role == null)
        {
            throw new InvalidOperationException("Role or user are not found");
        }

        var isExists =
            await _dbContext.UserRoles.AnyAsync(ur => ur.UserId == userId && ur.RoleId == roleId, cancellationToken);
        if (isExists)
        {
            throw new InvalidOperationException($"This user has role with id : {roleId}");
        }

        var userRole = new UserRole { UserId = userId, RoleId = roleId };
        await _dbContext.UserRoles.AddAsync(userRole, cancellationToken);
        await _dbContext.SaveChangesAsync(cancellationToken);
    }
    public async Task RemoveRoleFromUser(Guid userId, Guid roleId, CancellationToken cancellationToken = default)
    {
        var userRole = await _dbContext.UserRoles
            .FirstOrDefaultAsync(ur => ur.UserId == userId && ur.RoleId == roleId, cancellationToken);

        if (userRole == null)
        {
            throw new InvalidOperationException("User role not found");
        }

        _dbContext.UserRoles.Remove(userRole);
        await _dbContext.SaveChangesAsync(cancellationToken);
    }
}