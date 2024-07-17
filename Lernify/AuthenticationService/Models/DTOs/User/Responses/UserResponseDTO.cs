using AuthenticationService.Models.Entities;

namespace AuthenticationService.Models.DTOs.User.Responses;

public class UserResponseDTO
{
    public Guid Id { get; set; }
    public string Login { get; set; }
    public string Name { get; set; }
    public string Surname { get; set; }
    public List<RoleResponseDTO> Roles { get; set; }
}