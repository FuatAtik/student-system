using Kusys.Core.Abstract;
using Kusys.Core.DataAccess;
using Kusys.Data.Abstract;
using Kusys.Entities.Concrete;

namespace Kusys.Data.Concrete.EntityFramework;

public class EfUserDal: EfEntityRepositoryBase<User, KusysContext>, IUserDal
{
    public EfUserDal(ICommon common) : base(common)
    {
    }
}