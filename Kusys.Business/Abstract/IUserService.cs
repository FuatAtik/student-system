using Kusys.Core.Result;
using Kusys.Entities.Concrete;

namespace Kusys.Business.Abstract;

public interface IUserService
{
    /// <summary>
    /// Belirtilen id ye sahip kullanıcıyı getirir.
    /// </summary>
    /// <param name="id">Kullanıcının kimlik numarası.</param>
    /// <returns><see cref="BusinessLayerResult{T}"/> türünde işlem sonucu ve kullanıcı bilgisi.</returns>
    BusinessLayerResult<User> GetUserById(int id);

    /// <summary>
    /// Sistemdeki oturum açmış geçerli kullanıcıyı getirir.
    /// </summary>
    /// <param name="id">Kullanıcının kimlik numarası.</param>
    /// <returns><see cref="BusinessLayerResult{T}"/> türünde işlem sonucu ve kullanıcı bilgisi.</returns>
    BusinessLayerResult<User> GetCurrentUser(int id);

    /// <summary>
    /// Belirtilen id ye sahip kullanıcıyı siler.
    /// </summary>
    /// <param name="id">Kullanıcının kimlik numarası.</param>
    /// <returns><see cref="BusinessLayerResult{T}"/> türünde işlem sonucu.</returns>
    BusinessLayerResult<User> RemoveUserById(int id);

    /// <summary>
    /// Yeni bir kullanıcı ekler.
    /// </summary>
    /// <param name="data">Eklenecek kullanıcının bilgileri.</param>
    /// <returns><see cref="BusinessLayerResult{T}"/> türünde işlem sonucu ve eklenen kullanıcı bilgisi.</returns>
    BusinessLayerResult<User> Insert(User data);

    /// <summary>
    /// Kullanıcı bilgilerini günceller.
    /// </summary>
    /// <param name="data">Güncellenecek kullanıcının bilgileri.</param>
    /// <returns><see cref="BusinessLayerResult{T}"/> türünde işlem sonucu ve güncellenen kullanıcı bilgisi.</returns>
    BusinessLayerResult<User> Update(User data);

    /// <summary>
    /// Tüm kullanıcıları getirir.
    /// </summary>
    /// <returns><see cref="IQueryable{T}"/> türünde kullanıcı koleksiyonu.</returns>
    IQueryable<User> GetAllUser();
}