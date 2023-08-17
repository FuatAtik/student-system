using Kusys.Business.Abstract;
using Kusys.Data.Abstract;
using Kusys.Data.Concrete.EntityFramework;
using Kusys.Entities.Concrete;

namespace Kusys.Business.Concrete;

public class CourseManager : ICourseService
{
    private readonly ICourseDal _courseDal;

    public CourseManager(ICourseDal courseDal)
    {
        _courseDal = courseDal;
    }

    public IQueryable<Course> GetCourses()
    {
        return _courseDal.ListQueryable();
    }

    public Course GetCourseById(int id)
    {
        return _courseDal.Find(x => x.Id == id);
    }

    public int Insert(Course course)
    {
        // Eklemek istenen kursun benzersiz CourseId ve CourseCategoryId kombinasyonu veritabanında zaten mevcut mu diye kontrol edilir.
        var findData =
            _courseDal.Find(x => x.CourseId == course.CourseId && x.CourseCategoryId == course.CourseCategoryId);

        // Eğer aynı CourseId ve CourseCategoryId ile bir kurs zaten varsa, 101 kodu ile hata döndürülür.
        if (findData != null)
            return 101;

        // Kurs veritabanına eklenir ve sonucu döndürülür.
        var result = _courseDal.Insert(course);
        return result > 0 ? result : 0;
    }

    public int Update(Course course)
    {
        // Güncellenecek kursun mevcut veritabanındaki karşılığı bulunur.
        var findData = _courseDal.Find(x => x.Id == course.Id);

        // Kursun özellikleri güncellenir.
        findData.CourseCategoryId = course.CourseCategoryId;
        findData.Name = course.Name;
        findData.CourseId = course.CourseId;

        // Kursun güncellenmiş halini veritabanında güncelle ve işlem sonucunu döndür.
        var result = _courseDal.Update(findData);
        return result > 0 ? result : 0;
    }

    public int Delete(Course course)
    {
        // Silinecek kursun ID'sine göre veritabanından ilgili kurs bilgileri çekilir.
        var findData = GetCourseById(course.Id);

        // Çekilen kurs bilgilerine göre veritabanından kurs silinir ve işlem sonucu döndürülür.
        var result = _courseDal.Delete(findData);
        return result;
    }
}