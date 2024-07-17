using AuthenticationService.Models.Interfaces;

namespace AuthenticationService.Models.Entities;

public class UserRole:IBaseEntity
{
    public Guid Id { get; set; }
    public User User { get; set; }

    public int RoleId { get; set; }
    public Role Role { get; set; }
}