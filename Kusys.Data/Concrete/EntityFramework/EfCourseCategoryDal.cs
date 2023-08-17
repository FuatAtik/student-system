using Kusys.Core.Abstract;
using Kusys.Core.DataAccess;
using Kusys.Data.Abstract;
using Kusys.Entities.Concrete;

namespace Kusys.Data.Concrete.EntityFramework;

public class EfCourseCategoryDal: EfEntityRepositoryBase<CourseCategory, KusysContext>, ICourseCategoryDal
{
    public EfCourseCategoryDal(ICommon common) : base(common)
    {
    }
}