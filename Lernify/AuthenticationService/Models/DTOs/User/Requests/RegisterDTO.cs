namespace AuthenticationService.Models.DTOs.User.Requests;

public class RegisterDTO
{
    public string Login { get; set; }
    public string Name { get; set; }
    public string Surname { get; set; }
    public string Password { get; set; }
}