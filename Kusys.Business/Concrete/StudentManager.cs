using Kusys.Business.Abstract;
using Kusys.Core;
using Kusys.Core.Abstract;
using Kusys.Core.Concrete;
using Kusys.Core.Helpers;
using Kusys.Core.Messages;
using Kusys.Core.Result;
using Kusys.Data.Abstract;
using Kusys.Data.Concrete.EntityFramework;
using Kusys.Entities.Concrete;
using Kusys.Entities.Dto;

namespace Kusys.Business.Concrete;

public class StudentManager : IStudentService
{
    private readonly IStudentDal _studentDal;
    private readonly IUserDal _userDal;
    private readonly ICommon _common;

    public StudentManager(IStudentDal studentDal, IUserDal userDal, ICommon common)
    {
        _studentDal = studentDal;
        _userDal = userDal;
        _common = common;
    }

    public BusinessLayerResult<Student> GetStudentById(int id)
    {
        var businessLayerResult = new BusinessLayerResult<Student>
        {
            // Veritabanında öğrenci bulunmaya çalışılır.
            Result = _studentDal.Find(x => x.Id == id && x.IsDeleted == false)
        };

        // Eğer öğrenci bulunamazsa hata mesajı eklenir.
        if (businessLayerResult.Result == null)
        {
            businessLayerResult.AddError(ErrorMessageCode.StudentNotFound, "Öğrenci bulunamadı.");
        }

        return businessLayerResult;
    }

    public BusinessLayerResult<Student> GetStudentByEmail(string email)
    {
        var businessLayerResult = new BusinessLayerResult<Student>
        {
            // Veritabanında öğrenci bulunmaya çalışılır.
            Result = _studentDal.Find(x => x.Email == email && x.IsDeleted == false)
        };

        // Eğer öğrenci bulunamazsa hata mesajı eklenir.
        if (businessLayerResult.Result == null)
        {
            businessLayerResult.AddError(ErrorMessageCode.StudentNotFound, "Öğrenci bulunamadı.");
        }

        return businessLayerResult;
    }

    public BusinessLayerResult<Student> RemoveStudentById(int id)
    {
        var businessLayerResult = new BusinessLayerResult<Student>();
        var student = _studentDal.Find(x => x.Id == id);

        // Eğer öğrenci bulunursa, öğrencinin silindiğini işaretler ve güncelleme yapar.
        if (student != null)
        {
            student.IsDeleted = true;
            _studentDal.Update(student);
        }
        else
        {
            // Eğer öğrenci bulunamazsa hata mesajı eklenir.
            businessLayerResult.AddError(ErrorMessageCode.StudentCouldNotFind, "Öğrenci bulunamadı.");
        }

        return businessLayerResult;
    }

    public BusinessLayerResult<Student> Insert(Student data, string password)
    {
        // Yeni bir BusinessLayerResult nesnesi oluşturuluyor. Bu nesne işlem sonuçlarını ve hataları içerecektir.
        var businessLayerResult = new BusinessLayerResult<Student>();

        // Eğer veritabanında aynı Tc Kimlik numarası veya e-posta ile kayıtlı bir öğrenci varsa, hata mesajları eklenir.
        var student = _studentDal.Find(x =>
            x.TrIdentityNumber == data.TrIdentityNumber || x.Email == data.Email);
        if (student != null && student.IsDeleted != true)
        {
            if (student.TrIdentityNumber == data.TrIdentityNumber)
            {
                businessLayerResult.AddError(ErrorMessageCode.TrIdentityNumberAlreadyExists, "Tc Kimlik kayıtlı.");
            }

            if (student.Email == data.Email)
            {
                businessLayerResult.AddError(ErrorMessageCode.EmailAlreadyExists, "E-posta adresi kayıtlı.");
            }
        }
        else
        {
            // Yeni bir kullanıcı nesnesi oluşturulur ve veritabanına eklenir.
            var newUser = new User()
            {
                Username = data.FirstName + data.LastName,
                Email = data.Email,
                Password = Security.EncryptString(password),
                RoleId = 3,
                IsDeleted = false,
            };

            // Öğrenci ve kullanıcı veritabanına eklenir. E-posta ile giriş bilgileri gönderilir.
            var studentInsert = _studentDal.Insert(data);
            if (studentInsert > 0)
            {
                var userInsert = _userDal.Insert(newUser);
                if (userInsert <= 0)
                {
                    businessLayerResult.AddError(ErrorMessageCode.EmailAlreadyExists, "Kaydedilemedi.");
                }
                else
                {
                    //E-posta gönderme işlemi yapılır.
                    string siteUri = "http://localhost:7099/login";
                    string body =
                        $"Merhaba {data.FirstName + " " + data.LastName};<br><br>Öğrenci sistemine girmek için <a href='{siteUri}' target='_blank'>tıklayınız.</a> <br> E-Posta: {newUser.Email}  <br> Şifreniz: {password}.";


                    var sendMail = MailHelper.SendMail(body, data.Email, "Öğrenci sistemi giriş bilgileri");
                    if (!sendMail)
                    {
                        businessLayerResult.AddError(ErrorMessageCode.EmailAlreadyExists, "Mail Gönderilemedi.");
                    }

                    businessLayerResult.Result = data;
                }
            }
            else
            {
                businessLayerResult.AddError(ErrorMessageCode.EmailAlreadyExists, "Kaydedilemedi.");
            }
        }

        // İşlem sonucu olan BusinessLayerResult nesnesi döndürülür.
        return businessLayerResult;
    }

    public BusinessLayerResult<Student> Update(Student data)
    {
        // Yeni bir BusinessLayerResult nesnesi oluşturuluyor. Bu nesne işlem sonuçlarını ve hataları içerecektir.
        var businessLayerResult = new BusinessLayerResult<Student>();

        // Güncellenecek öğrencinin mevcut veritabanındaki karşılığı bulunur.
        var checkData = _studentDal.Find(x => x.Id == data.Id);

        // Eğer öğrenci bulunamazsa hata mesajı eklenir.
        if (checkData == null)
        {
            businessLayerResult.AddError(ErrorMessageCode.StudentNotFound, "Öğrenci bulunamadı.");
        }

        // Öğrenci bilgileri güncellenir.
        checkData.FirstName = data.FirstName;
        checkData.LastName = data.LastName;
        checkData.TrIdentityNumber = data.TrIdentityNumber;
        checkData.BirthDate = data.BirthDate;
        checkData.Email = data.Email;
        checkData.CreatedDate = data.CreatedDate;
        checkData.ModifiedDate = DateTime.Now;
        checkData.ModifiedUsername = _common.GetCurrentUsername();

        // Öğrenci veritabanında güncellenir ve işlem sonucu döndürülür.
        _studentDal.Update(checkData);
        businessLayerResult.Result = checkData;
        return businessLayerResult;
    }

    public BusinessLayerResult<Student> Delete(int id)
    {
        // Yeni bir BusinessLayerResult nesnesi oluşturuluyor. Bu nesne işlem sonuçlarını ve hataları içerecektir.
        var businessLayerResult = new BusinessLayerResult<Student>();

        // Belirli bir öğrenci ID'sine göre öğrenci veritabanından çekilir.
        var checkData = _studentDal.Find(x => x.Id == id);

        // Eğer öğrenci bulunamazsa hata mesajı eklenir.
        if (checkData == null)
        {
            businessLayerResult.AddError(ErrorMessageCode.StudentNotFound, "Öğrenci bulunamadı.");
        }

        // Öğrencinin "IsDeleted" özelliği işaretlenir ve güncelleme işlemi yapılır.
        checkData.IsDeleted = true;
        _studentDal.Update(checkData);

        // İşlem sonucu olan BusinessLayerResult nesnesi döndürülür.
        businessLayerResult.Result = checkData;
        return businessLayerResult;
    }

    public List<Student> GetAllStudent()
    {
        return _studentDal.List(x => x.IsDeleted == false);
    }

    public IQueryable<Student> GetAllStudentQueryable()
    {
        return _studentDal.ListQueryable().Where(x => x.IsDeleted == false);
    }
}