using AuthenticationService.Infrastructure.Database;
using AuthenticationService.Models.Entities;
using AuthenticationService.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace AuthenticationService.Repositories.Implementations;

public class UserRepository:IUserRepository
{
    private readonly AuthDbContext _dbContext;

    public UserRepository(AuthDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<User> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        return await _dbContext.Users.FirstOrDefaultAsync(a => a.Id == id, cancellationToken) ?? throw new InvalidOperationException($"User with id : {id} are not found");
    }

    public async Task<IEnumerable<User>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        return await _dbContext.Users.Where(u=>!u.IsDeleted).ToListAsync(cancellationToken);
    }

    public async Task CreateAsync(User role, CancellationToken cancellationToken=default)
    {
        await _dbContext.Users.AddAsync(role, cancellationToken);
        await _dbContext.SaveChangesAsync(cancellationToken);
    }

    public async Task DeleteAsync(User role, CancellationToken cancellationToken=default)
    {
        role.IsDeleted = true; 
        _dbContext.Users.Update(role);
        await _dbContext.SaveChangesAsync(cancellationToken);
    }

    public async Task UpdateAsync(User role, CancellationToken cancellationToken = default)
    {
        _dbContext.Users.Update(role);
        await _dbContext.SaveChangesAsync(cancellationToken);
    }

    public async Task<IEnumerable<User>> GetUsersByRoleId(Guid roleId, CancellationToken cancellationToken = default)
    {
        return await _dbContext.UserRoles.Where(ur => ur.RoleId == roleId).Select(ur=>ur.User).ToListAsync(cancellationToken);
    }
}