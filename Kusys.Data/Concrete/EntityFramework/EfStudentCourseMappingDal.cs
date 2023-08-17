using Kusys.Core.Abstract;
using Kusys.Core.DataAccess;
using Kusys.Data.Abstract;
using Kusys.Entities.Concrete;

namespace Kusys.Data.Concrete.EntityFramework;

public class EfStudentCourseMappingDal: EfEntityRepositoryBase<StudentCourseMapping, KusysContext>, IStudentCourseMappingDal
{
    public EfStudentCourseMappingDal(ICommon common) : base(common)
    {
    }
}