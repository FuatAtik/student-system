using Kusys.Core.Result;
using Kusys.Entities.Concrete;

namespace Kusys.Business.Abstract;

public interface ICourseService
{
    /// <summary>
    /// Tüm dersleri sorgulanabilir (queryable) şekilde getirir.
    /// </summary>
    /// <returns><see cref="IQueryable{T}"/> türünde ders koleksiyonu.</returns>
    IQueryable<Course> GetCourses();

    /// <summary>
    /// Belirtilen id ye sahip dersi getirir.
    /// </summary>
    /// <param name="id">Ders id.</param>
    /// <returns><see cref="Course"/> türünde ders bilgisi.</returns>
    Course GetCourseById(int id);

    /// <summary>
    /// Yeni bir ders ekler.
    /// </summary>
    /// <param name="course">Eklenecek dersin bilgileri.</param>
    /// <returns>Eklendiği durumda  1, aksi halde 0.</returns>
    int Insert(Course course);

    /// <summary>
    /// Ders bilgilerini günceller.
    /// </summary>
    /// <param name="course">Güncellenecek dersin bilgileri.</param>
    /// <returns>Güncellendiği durumda 1, aksi halde 0.</returns>
    int Update(Course course);

    /// <summary>
    /// Belirtilen dersi siler.
    /// </summary>
    /// <param name="course">Silinmek istenen dersin bilgileri.</param>
    /// <returns>Silindiği durumda 1, aksi halde 0.</returns>
    int Delete(Course course);
}