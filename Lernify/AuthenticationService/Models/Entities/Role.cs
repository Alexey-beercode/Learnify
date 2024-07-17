using AuthenticationService.Models.Interfaces;

namespace AuthenticationService.Models.Entities;

public class Role:IBaseEntity
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public bool IsDeleted { get; set; }

    public ICollection<UserRole> UserRoles { get; set; }
   
}