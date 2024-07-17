using AuthenticationService.Models.Interfaces;

namespace AuthenticationService.Models.Entities;

public class UserRole
{
    public Guid UserId { get; set; }
    public User User { get; set; }

    public Guid RoleId { get; set; }
    public Role Role { get; set; }
}