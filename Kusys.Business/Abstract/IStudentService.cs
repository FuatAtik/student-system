using Kusys.Core.Result;
using Kusys.Entities.Concrete;
using Kusys.Entities.Dto;

namespace Kusys.Business.Abstract;

public interface IStudentService
{
    /// <summary>
    /// Belirtilen id ye sahip öğrenciyi getirir.
    /// </summary>
    /// <param name="id">Öğrencinin kimlik numarası.</param>
    /// <returns><see cref="BusinessLayerResult{T}"/> türünde işlem sonucu ve öğrenci bilgisi.</returns>
    BusinessLayerResult<Student> GetStudentById(int id);

    /// <summary>
    /// Belirtilen e-posta adresine sahip öğrenciyi getirir.
    /// </summary>
    /// <param name="email">Öğrencinin e-posta adresi.</param>
    /// <returns><see cref="BusinessLayerResult{T}"/> türünde işlem sonucu ve öğrenci bilgisi.</returns>
    BusinessLayerResult<Student> GetStudentByEmail(string email);

    /// <summary>
    /// Belirtilen id ye sahip öğrenciyi siler.
    /// </summary>
    /// <param name="id">Öğrencinin kimlik numarası.</param>
    /// <returns><see cref="BusinessLayerResult{T}"/> türünde işlem sonucu.</returns>
    BusinessLayerResult<Student> RemoveStudentById(int id);

    /// <summary>
    /// Yeni bir öğrenci ekler.
    /// </summary>
    /// <param name="data">Eklenecek öğrencinin bilgileri.</param>
    /// <param name="password">Öğrencinin şifresi.</param>
    /// <returns><see cref="BusinessLayerResult{T}"/> türünde işlem sonucu ve eklenen öğrenci bilgisi.</returns>
    BusinessLayerResult<Student> Insert(Student data, string password);

    /// <summary>
    /// Öğrenci bilgilerini günceller.
    /// </summary>
    /// <param name="data">Güncellenecek öğrencinin bilgileri.</param>
    /// <returns><see cref="BusinessLayerResult{T}"/> türünde işlem sonucu ve güncellenen öğrenci bilgisi.</returns>
    BusinessLayerResult<Student> Update(Student data);

    /// <summary>
    /// Belirtilen id ye sahip öğrenciyi siler.
    /// </summary>
    /// <param name="id">Öğrencinin kimlik numarası.</param>
    /// <returns><see cref="BusinessLayerResult{T}"/> türünde işlem sonucu.</returns>
    BusinessLayerResult<Student> Delete(int id);

    /// <summary>
    /// Tüm öğrencileri sorgulanabilir (queryable) şekilde getirir.
    /// </summary>
    /// <returns><see cref="IQueryable{T}"/> türünde öğrenci koleksiyonu.</returns>
    IQueryable<Student> GetAllStudentQueryable();

    /// <summary>
    /// Tüm öğrencileri list olarak getirir.
    /// </summary>
    /// <returns><see cref="List{T}"/> türünde öğrenci koleksiyonu.</returns>
    List<Student> GetAllStudent();
}