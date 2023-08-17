using Kusys.Business.Abstract;
using Kusys.Core;
using Kusys.Core.Abstract;
using Kusys.Core.Concrete;
using Kusys.Core.Messages;
using Kusys.Core.Result;
using Kusys.Data.Abstract;
using Kusys.Data.Concrete.EntityFramework;
using Kusys.Entities.Concrete;
using Kusys.Entities.Dto;
using Microsoft.EntityFrameworkCore;

namespace Kusys.Business.Concrete;

public class LoginManager : ILoginService
{
    private readonly IUserDal _userDal;
    private readonly ICommon _common;

    public LoginManager(IUserDal userDal, ICommon common)
    {
        _userDal = userDal;
        _common = common;
    }

    public BusinessLayerResult<User> Register(UserRegisterDto registerViewModel)
    {
        // Yeni bir BusinessLayerResult nesnesi oluşturuluyor. Bu nesne işlem sonuçlarını ve hataları içerecektir.
        var businessLayerResult = new BusinessLayerResult<User>();

        // Kullanıcının e-posta veya kullanıcı adıyla veritabanında sorgu yapılıyor.
        var user = _userDal.Find(x =>
            x.Email == registerViewModel.Email || x.Username == registerViewModel.Username);

        // Eğer veritabanında aynı e-posta veya kullanıcı adıyla kayıtlı bir kullanıcı varsa
        if (user != null)
        {
            // Kullanıcı adı daha önceden kaydedilmişse hata mesajı ekleniyor.
            if (user.Username == registerViewModel.Username)
            {
                businessLayerResult.AddError(ErrorMessageCode.UsernameAlreadyExists, "Kullanıcı adı kayıtlı.");
            }

            // E-posta adresi daha önceden kaydedilmişse hata mesajı ekleniyor.
            if (user.Email == registerViewModel.Email)
            {
                businessLayerResult.AddError(ErrorMessageCode.EmailAlreadyExists, "E-posta adresi kayıtlı.");
            }
        }
        else
        {
            // Yeni bir kullanıcı nesnesi oluşturuluyor ve veritabanına ekleniyor.
            var newUser = new User()
            {
                Username = registerViewModel.Username,
                Email = registerViewModel.Email,
                Password = Security.EncryptString(registerViewModel.Password),
                RoleId = 3,
                IsDeleted = false,
                CreatedDate = DateTime.Now,
                ModifiedDate = DateTime.Now,
                ModifiedUsername = _common.GetCurrentUsername()
            };

            _userDal.Insert(newUser);

            // İşlem sonucu olarak oluşturulan kullanıcı nesnesi BusinessLayerResult nesnesine ekleniyor.
            businessLayerResult.Result = newUser;
        }

        // İşlem sonucu olan BusinessLayerResult nesnesi döndürülüyor.
        return businessLayerResult;
    }

    public BusinessLayerResult<User> Login(UserLoginDto loginViewModel)
    {
        // Yeni bir BusinessLayerResult nesnesi oluşturuluyor. Bu nesne işlem sonuçlarını ve hataları içerecektir.
        var businessLayerResult = new BusinessLayerResult<User>();

        // Kullanıcının e-posta adresine göre veritabanında sorgu yapılıyor.
        var data = _userDal.ListQueryable().Where(x =>
            x.Email == loginViewModel.Email).Include(x => x.Role).SingleOrDefault();

        // Veritabanından bir sonuç elde edilmişse
        if (data != null)
        {
            // Veritabanında saklanan şifre çözülerek orijinal hale getiriliyor.
            var decodePassword = Security.DecryptString(data.Password);

            // Kullanıcının girdiği şifre ile çözülen şifre karşılaştırılıyor.
            if (decodePassword != loginViewModel.Password)
            {
                // Eğer şifreler uyuşmuyorsa, hata mesajı ekleniyor.
                businessLayerResult.AddError(ErrorMessageCode.UserNotFound, "Şifre hatalı");
            }
            else
            {
                // Şifreler uyuşuyorsa, kullanıcı bilgisi sonuç olarak ayarlanıyor.
                businessLayerResult.Result = data;
            }
        }
        else
        {
            // Eğer veritabanında eşleşen bir kullanıcı bulunamazsa, hata mesajı ekleniyor.
            businessLayerResult.AddError(ErrorMessageCode.UserNotFound, "Kullanıcı Bulunamadı");
        }

        // İşlem sonucu olan BusinessLayerResult nesnesi döndürülüyor.
        return businessLayerResult;
    }
}