using Kusys.Core.Result;
using Kusys.Entities.Concrete;

namespace Kusys.Business.Abstract;

public interface ICourseCategoryService
{
    /// <summary>
    /// Tüm ders kategorilerini getirir.
    /// </summary>
    /// <returns><see cref="List{T}"/> türünde ders kategorileri koleksiyonu.</returns>
    List<CourseCategory> GetCourseCategory();

    /// <summary>
    /// Belirtilen id ye sahip ders kategorisini getirir.
    /// </summary>
    /// <param name="id">Ders kategorisinin kimlik numarası.</param>
    /// <returns><see cref="CourseCategory"/> türünde ders kategorisi bilgisi.</returns>
    CourseCategory GetCourseCategoryById(int id);

    /// <summary>
    /// Yeni bir ders kategorisi ekler.
    /// </summary>
    /// <param name="category">Eklenecek ders kategorisinin bilgileri.</param>
    /// <returns>Eklendiği durumda 1, aksi halde 0.</returns>
    int Insert(CourseCategory category);

    /// <summary>
    /// Ders kategorisi bilgilerini günceller.
    /// </summary>
    /// <param name="category">Güncellenecek ders kategorisinin bilgileri.</param>
    /// <returns>Güncellendiği durumda 1, aksi halde 0.</returns>
    int Update(CourseCategory category);

    /// <summary>
    /// Belirtilen ders kategorisini siler.
    /// </summary>
    /// <param name="category">Silinmek istenen ders kategorisinin bilgileri.</param>
    /// <returns>Silindiği durumda 1, aksi halde 0.</returns>
    int Delete(CourseCategory category);
}