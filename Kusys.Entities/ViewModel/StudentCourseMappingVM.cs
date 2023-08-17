using Kusys.Entities.Concrete;

namespace Kusys.Entities.ViewModel;

public class StudentViewModel
{
    
    public string StudentCourseMappingId { get; set; }
    public int StudentId { get; set; }
    public string StudentFullname { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string TrIdentityNumber { get; set; }
    public DateTime BirthDate { get; set; }
    public string Email { get; set; }
    public List<string>? Courses { get; set; }
    
    public List<string>? CourseId { get; set; }
    public List<string>? CourseName { get; set; }
}