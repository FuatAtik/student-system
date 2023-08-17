using System.ComponentModel.DataAnnotations;
using Kusys.Core.Entities;

namespace Kusys.Entities.Concrete;

public class Student : BaseEntity, IEntity
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    // TODO profile image eklenebilir
    // public string ProfileImage { get; set; }
    public string TrIdentityNumber { get; set; }
    
    [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
    public DateTime BirthDate { get; set; }
    public string Email { get; set; }
    public bool IsDeleted { get; set; }
    
    public ICollection<StudentCourseMapping> StudentCourseMapping { get; set; }

}