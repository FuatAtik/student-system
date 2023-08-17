using Kusys.Core.Result;
using Kusys.Entities.Concrete;
using Kusys.Entities.Dto;

namespace Kusys.Business.Abstract;

public interface ILoginService
{
    /// <summary>
    /// Kullanıcı kaydı işlemini gerçekleştirir.
    /// </summary>
    /// <param name="registerViewModel">Kayıt yapılacak kullanıcının bilgileri.</param>
    /// <returns><see cref="BusinessLayerResult{T}"/> türünde işlem sonucu ve kaydedilen kullanıcı bilgisi.</returns>
    BusinessLayerResult<User> Register(UserRegisterDto registerViewModel);

    /// <summary>
    /// Kullanıcı girişi işlemini gerçekleştirir.
    /// </summary>
    /// <param name="loginViewModel">Giriş yapacak kullanıcının bilgileri.</param>
    /// <returns><see cref="BusinessLayerResult{T}"/> türünde işlem sonucu ve giriş yapan kullanıcı bilgisi.</returns>
    BusinessLayerResult<User> Login(UserLoginDto loginViewModel);

}