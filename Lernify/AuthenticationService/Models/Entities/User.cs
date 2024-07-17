using AuthenticationService.Models.Interfaces;

namespace AuthenticationService.Models.Entities;

public class User:IBaseEntity
{
    public Guid Id { get; set; }
    public int UserId { get; set; }
    public string UserName { get; set; }
    public string PasswordHash { get; set; }

    public ICollection<UserRole> UserRoles { get; set; }
}