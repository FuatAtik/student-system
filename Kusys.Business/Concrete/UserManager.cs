using Kusys.Business.Abstract;
using Kusys.Core;
using Kusys.Core.Concrete;
using Kusys.Core.Messages;
using Kusys.Core.Result;
using Kusys.Data.Abstract;
using Kusys.Data.Concrete.EntityFramework;
using Kusys.Entities.Concrete;

namespace Kusys.Business.Concrete;

public class UserManager : IUserService
{
    private readonly IUserDal _userDal;

    public UserManager(IUserDal userDal)
    {
        _userDal = userDal;
    }

    public BusinessLayerResult<User> GetUserById(int id)
    {
        var businessLayerResult = new BusinessLayerResult<User>();

        // Veritabanında kullanıcı bulunmaya çalışılır.
        businessLayerResult.Result = _userDal.Find(x => x.Id == id && x.IsDeleted == false);

        // Eğer kullanıcı bulunamazsa hata mesajı eklenir.
        if (businessLayerResult.Result == null)
        {
            businessLayerResult.AddError(ErrorMessageCode.UserNotFound, "Kullanıcı bulunamadı.");
        }

        return businessLayerResult;
    }
    public BusinessLayerResult<User> GetCurrentUser(int id)
    {
        var businessLayerResult = new BusinessLayerResult<User>();

        // Veritabanında kullanıcı bulunmaya çalışılır.
        var result = _userDal.Find(x => x.Id == id && x.IsDeleted == false);

        // Kullanıcının şifresi çözümlenir.
        result.Password = Security.DecryptString(result.Password);

        businessLayerResult.Result = result;

        // Eğer kullanıcı bulunamazsa hata mesajı eklenir.
        if (businessLayerResult.Result == null)
        {
            businessLayerResult.AddError(ErrorMessageCode.UserNotFound, "Kullanıcı bulunamadı.");
        }

        return businessLayerResult;
    }
    public BusinessLayerResult<User> RemoveUserById(int id)
    {
        var businessLayerResult = new BusinessLayerResult<User>();
        var user = _userDal.Find(x => x.Id == id);

        // Eğer kullanıcı bulunursa, kullanıcının silindiğini işaretler ve güncelleme yapar.
        if (user != null)
        {
            user.IsDeleted = true;
            _userDal.Update(user);
        }
        else
        {
            // Eğer kullanıcı bulunamazsa hata mesajı eklenir.
            businessLayerResult.AddError(ErrorMessageCode.UserCouldNotFind, "Kullanıcı bulunamadı.");
        }

        return businessLayerResult;
    }
    public BusinessLayerResult<User> Insert(User data)
    {
        var businessLayerResult = new BusinessLayerResult<User>();

        // Veritabanında aynı kullanıcı adı veya e-posta ile kayıtlı bir kullanıcı varsa, hata mesajları eklenir.
        var user = _userDal.Find(x => x.Username == data.Username || x.Email == data.Email);
        businessLayerResult.Result = data;

        if (user != null)
        {
            if (user.Username == data.Username)
            {
                businessLayerResult.AddError(ErrorMessageCode.UsernameAlreadyExists, "Kullanıcı adı kayıtlı.");
            }

            if (user.Email == data.Email)
            {
                businessLayerResult.AddError(ErrorMessageCode.EmailAlreadyExists, "E-posta adresi kayıtlı.");
            }
        }
        else
        {
            // Kullanıcının şifresi şifrelenir ve veritabanına eklenir.
            businessLayerResult.Result.Password = Security.EncryptString(data.Password);
            _userDal.Insert(businessLayerResult.Result);
        }

        return businessLayerResult;
    }
    public BusinessLayerResult<User> Update(User data)
    {
        var businessLayerResult = new BusinessLayerResult<User>();

        // Güncellenecek kullanıcının mevcut veritabanındaki karşılığı bulunur.
        businessLayerResult.Result = _userDal.Find(x => x.Id == data.Id);

        // Kullanıcı bilgileri güncellenir.
        businessLayerResult.Result.Email = data.Email;
        businessLayerResult.Result.Password = Security.EncryptString(data.Password);
        businessLayerResult.Result.Username = data.Username;
        businessLayerResult.Result.RoleId = data.RoleId;

        // Kullanıcı veritabanında güncellenir ve işlem sonucu döndürülür.
        _userDal.Update(businessLayerResult.Result);
        return businessLayerResult;
    }
    public IQueryable<User> GetAllUser()
    {
        return _userDal.ListQueryable().Where(x => x.IsDeleted == false);
    }
}