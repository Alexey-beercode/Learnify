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
        return await _dbContext.Users.FirstOrDefaultAsync(a => a.Id == id, cancellationToken);
    }

    public async Task<IEnumerable<User>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        return await _dbContext.Users.Where(u=>!u.IsDeleted).ToListAsync(cancellationToken);
    }

    public async Task CreateAsync(User user, CancellationToken cancellationToken=default)
    {
        await _dbContext.Users.AddAsync(user, cancellationToken);
        await _dbContext.SaveChangesAsync(cancellationToken);
    }

    public async Task DeleteAsync(User user, CancellationToken cancellationToken=default)
    {
        user.IsDeleted = true; 
        _dbContext.Users.Update(user);
        await _dbContext.SaveChangesAsync(cancellationToken);
    }

    public async Task UpdateAsync(User user, CancellationToken cancellationToken = default)
    {
        _dbContext.Users.Update(user);
        await _dbContext.SaveChangesAsync(cancellationToken);
    }

    public async Task<IEnumerable<User>> GetUsersByRoleId(Guid roleId, CancellationToken cancellationToken = default)
    {
        return await _dbContext.UserRoles
            .Where(ur => ur.RoleId == roleId)
            .Select(ur=>ur.User)
            .Where(u=>!u.IsDeleted)
            .ToListAsync(cancellationToken);
    }

    public async Task<User> GetByLoginAsync(string login, CancellationToken canaCancellationToken = default)
    {
        return await _dbContext.Users
            .FirstOrDefaultAsync(u => u.Login == login && !u.IsDeleted, canaCancellationToken);
    }
}