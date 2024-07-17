using AuthenticationService.Models.Interfaces;

namespace AuthenticationService.Models.Entities;

public class User:IBaseEntity
{
    public Guid Id { get; set; }
    public string Login { get; set; }
    public string Name { get; set; }
    public string Surname { get; set; }
    public string PasswordHash { get; set; }
    public bool IsDeleted { get; set; }

    public ICollection<UserRole> UserRoles { get; set; }
}