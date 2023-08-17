using Kusys.Core.Entities;

namespace Kusys.Entities.Dto;

public class UserRegisterDto:IDto
{
    public string Username { get; set; }
    public string Email { get; set; }
    public string Password { get; set; }
}