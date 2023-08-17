using Kusys.Core.Entities;

namespace Kusys.Entities.Dto;

public class UserLoginDto : IDto
{
    public string Email { get; set; }
    public string Password { get; set; }
}