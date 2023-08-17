using Kusys.Business.Abstract;
using Kusys.Business.Concrete;
using Kusys.Core.Abstract;
using Kusys.Core.Concrete;
using Kusys.Data.Abstract;
using Kusys.Data.Concrete.EntityFramework;
using Kusys.WebUI.Init;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddSession();


builder.Services.AddDbContext<KusysContext>();
builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
builder.Services.AddHttpContextAccessor();

builder.Services.AddSingleton<ICommon, WebCommon>();

builder.Services.AddSingleton<IUserService,UserManager>();
builder.Services.AddSingleton<IUserDal,EfUserDal>();

builder.Services.AddSingleton<IStudentService,StudentManager>();
builder.Services.AddSingleton<IStudentDal,EfStudentDal>();

builder.Services.AddSingleton<ICourseService,CourseManager>();
builder.Services.AddSingleton<ICourseDal,EfCourseDal>();

builder.Services.AddSingleton<ICourseCategoryService,CourseCategoryManager>();
builder.Services.AddSingleton<ICourseCategoryDal,EfCourseCategoryDal>();

builder.Services.AddSingleton<IStudentCourseMappingService,StudentCourseMappingManager>();
builder.Services.AddSingleton<IStudentCourseMappingDal,EfStudentCourseMappingDal>();

builder.Services.AddSingleton<ILoginService,LoginManager>();



var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
