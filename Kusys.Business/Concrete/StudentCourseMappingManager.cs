using Kusys.Business.Abstract;
using Kusys.Data.Abstract;
using Kusys.Data.Concrete.EntityFramework;
using Kusys.Entities.Concrete;
using Kusys.Entities.ViewModel;

namespace Kusys.Business.Concrete;

public class StudentCourseMappingManager : IStudentCourseMappingService
{
    private readonly IStudentCourseMappingDal _studentCourseMappingDal;
    private readonly IStudentDal _studentDal;
    private readonly ICourseDal _courseDal;

    public StudentCourseMappingManager(IStudentCourseMappingDal studentCourseMappingDal, IStudentDal studentDal, ICourseDal courseDal)
    {
        _studentCourseMappingDal = studentCourseMappingDal;
        _studentDal = studentDal;
        _courseDal = courseDal;
    }

    public List<StudentCourseMapping> GetStudentCourseMappingCategory()
    {
        return _studentCourseMappingDal.List();
    }

    public StudentCourseMapping GetStudentCourseMappingById(int id)
    {
        return _studentCourseMappingDal.Find(x=>x.Id==id);
    }

    public void Insert(StudentCourseMapping studentCourseMapping)
    {
        _studentCourseMappingDal.Insert(studentCourseMapping);
    }

    public void Update(StudentCourseMapping studentCourseMapping)
    {
        _studentCourseMappingDal.Update(studentCourseMapping);
    }

    public void Delete(StudentCourseMapping studentCourseMapping)
    {
        _studentCourseMappingDal.Delete(studentCourseMapping);
    }
}