using Kusys.Core.Entities;

namespace Kusys.Entities.Concrete;

public class Role: BaseEntity, IEntity
{
    public string Name { get; set; }
    public string SecretName { get; set; }
}