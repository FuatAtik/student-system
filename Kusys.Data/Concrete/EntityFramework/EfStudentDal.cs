using Kusys.Core.Abstract;
using Kusys.Core.DataAccess;
using Kusys.Data.Abstract;
using Kusys.Entities.Concrete;

namespace Kusys.Data.Concrete.EntityFramework;

public class EfStudentDal: EfEntityRepositoryBase<Student, KusysContext>, IStudentDal
{
    public EfStudentDal(ICommon common) : base(common)
    {
    }
}