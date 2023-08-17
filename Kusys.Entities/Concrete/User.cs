using Kusys.Core.Entities;

namespace Kusys.Entities.Concrete;

public class User : BaseEntity, IEntity
{
    public string Username { get; set; }
    public string Email { get; set; }
    public string Password { get; set; }
    public bool IsDeleted { get; set; }
    
    public int RoleId { get; set; }
    public Role Role { get; set; }
}