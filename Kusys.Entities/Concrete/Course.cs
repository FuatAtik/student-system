using Kusys.Core.Entities;

namespace Kusys.Entities.Concrete;

public class Course : BaseEntity, IEntity
{
    public string CourseId { get; set; }
    public string Name { get; set; }
    public int CourseCategoryId { get; set; }
    
    public CourseCategory CourseCategory { get; set; }
    
    public ICollection<StudentCourseMapping> StudentCourseMapping { get; set; }

}