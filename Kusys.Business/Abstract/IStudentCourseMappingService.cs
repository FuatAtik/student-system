using Kusys.Entities.Concrete;
using Kusys.Entities.ViewModel;

namespace Kusys.Business.Abstract;

public interface IStudentCourseMappingService
{
    /// <summary>
    /// Öğrenci ders eşleme kategorilerini getirir.
    /// </summary>
    /// <returns><see cref="List{T}"/> türünde öğrenci ders eşleme kategorileri koleksiyonu.</returns>
    List<StudentCourseMapping> GetStudentCourseMappingCategory();

    /// <summary>
    /// Belirtilen id ye sahip öğrenci ders eşlemesini getirir.
    /// </summary>
    /// <param name="id">Öğrenci ders eşleme kimlik numarası.</param>
    /// <returns><see cref="StudentCourseMapping"/> türünde öğrenci ders eşleme bilgisi.</returns>
    StudentCourseMapping GetStudentCourseMappingById(int id);

    /// <summary>
    /// Yeni bir öğrenci ders eşlemesi ekler.
    /// </summary>
    /// <param name="studentCourseMapping">Eklenecek öğrenci ders eşleme bilgisi.</param>
    void Insert(StudentCourseMapping studentCourseMapping);

    /// <summary>
    /// Öğrenci ders eşlemesi bilgisini günceller.
    /// </summary>
    /// <param name="studentCourseMapping">Güncellenecek öğrenci ders eşleme bilgisi.</param>
    void Update(StudentCourseMapping studentCourseMapping);

    /// <summary>
    /// Belirtilen öğrenci ders eşlemesini siler.
    /// </summary>
    /// <param name="studentCourseMapping">Silinecek öğrenci ders eşleme bilgisi.</param>
    void Delete(StudentCourseMapping studentCourseMapping);
}