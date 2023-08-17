using System.ComponentModel.DataAnnotations;

namespace Kusys.Entities.Dto;

public class StudentDto
{
    public int Id { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string TrIdentityNumber { get; set; }
    [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
    public DateTime BirthDate { get; set; }
    public string Email { get; set; }
    public bool IsDeleted { get; set; }
}