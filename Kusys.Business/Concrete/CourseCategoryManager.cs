using Kusys.Business.Abstract;
using Kusys.Data.Abstract;
using Kusys.Data.Concrete.EntityFramework;
using Kusys.Entities.Concrete;

namespace Kusys.Business.Concrete;

public class CourseCategoryManager : ICourseCategoryService
{
    private readonly ICourseCategoryDal _categoryDal;

    public CourseCategoryManager(ICourseCategoryDal categoryDal)
    {
        _categoryDal = categoryDal;
    }

    public List<CourseCategory> GetCourseCategory()
    {
        return _categoryDal.List();
    }

    public CourseCategory GetCourseCategoryById(int id)
    {
        return _categoryDal.Find(x => x.Id == id);
    }

    public int Insert(CourseCategory category)
    {
        return _categoryDal.Insert(category);
    }

    public int Update(CourseCategory category)
    {
        var findData = _categoryDal.Find(x => x.Id == category.Id);
        findData.Name = category.Name;
        var result = _categoryDal.Update(findData);
        return result > 0 ? result : 0;
    }

    public int Delete(CourseCategory category)
    {
       return _categoryDal.Delete(category);
    }
}