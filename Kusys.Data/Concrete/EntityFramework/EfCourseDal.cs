using Kusys.Core.Abstract;
using Kusys.Core.DataAccess;
using Kusys.Data.Abstract;
using Kusys.Entities.Concrete;

namespace Kusys.Data.Concrete.EntityFramework;

public class EfCourseDal: EfEntityRepositoryBase<Course, KusysContext>, ICourseDal
{
    public EfCourseDal(ICommon common) : base(common)
    {
    }
}