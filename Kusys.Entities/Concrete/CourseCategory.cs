using Kusys.Core.Entities;

namespace Kusys.Entities.Concrete;

public class CourseCategory : BaseEntity, IEntity
{
    public string Name { get; set; }
    
    public ICollection<Course> Courses { get; set; }
}