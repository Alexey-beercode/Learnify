﻿using AuthenticationService.Models.Entities;

namespace AuthenticationService.Repositories.Interfaces;

public interface IUserRepository:IBaseRepository<User>
{
    Task<IEnumerable<User>> GetUsersByRoleId(Guid roleId, CancellationToken cancellationToken = default);
    Task<User> GetByLoginAsync(string login, CancellationToken canaCancellationToken = default);
}