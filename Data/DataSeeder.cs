using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using SabisTest.Entities;

namespace SabisTest.Data
{
  public class DataSeeder
  {
    private readonly SabisDataContext _context;
    private readonly IHostingEnvironment _hosting;
    private readonly UserManager<UserInfo> _userManager;

    public DataSeeder(SabisDataContext ctx, IHostingEnvironment hosting, UserManager<UserInfo> userManager)
    {
      _context = ctx;
      _hosting = hosting;
      _userManager = userManager;
    }

    public async Task SeedAsync()
    {
      _context.Database.EnsureCreated();


      #region First User
      UserInfo user = await _userManager.FindByEmailAsync("mkhiami@gmail.com");
      if (user == null)
      {
        user = new UserInfo()
        {
          LastName = "Khiami",
          FirstName = "Mohammad",
          Email = "mkhiami@gmail.com",
          UserName = "mkhiami@gmail.com"
        };
          var result = await _userManager.CreateAsync(user, "khiami@Sabis091");
          if (result != IdentityResult.Success)
          {
            throw new InvalidOperationException("Hyada eli ba3d na2is");
          }

       
       
      }
      #endregion

      if (!_context.Students.Any())
      {

        _context.Students.AddRange(
          new Student() { NameEn = "Mohammad", NameAr = "Mohammad-A", Gender = Gender.Male, DateOfBirth = new DateTime(1990, 1, 1), Major = "Computer Science" },
          new Student() { NameEn = "Omar", NameAr = "Omar-A", Gender = Gender.Male, DateOfBirth = new DateTime(2011, 1, 1), Major = "Computer Science" },
          new Student() { NameEn = "Khaled", NameAr = "Khaled-A", Gender = Gender.Male, DateOfBirth = new DateTime(2015, 1, 1), Major = "Computer Science" });
        _context.Courses.AddRange(
        new Course() { NameAr = "Course 1-A", NameEn = "Course 1" },
        new Course() { NameAr = "Course 2-A", NameEn = "Course 2" },
        new Course() { NameAr = "Course 3-A", NameEn = "Course 3" },
        new Course() { NameAr = "Course 4-A", NameEn = "Course 4" }
        );
        _context.Semesters.AddRange(
         new Semester() { Term = Term.Fall, Year = 2018, Start = new DateTime(2018, 1, 1), End = new DateTime(2018, 4, 1), Title = "FALL-2018" },
        new Semester { Term = Term.Spring, Year = 2018, Start = new DateTime(2018, 4, 1), End = new DateTime(2018, 8, 1), Title = "SPR-2018" }
        );
        await _context.SaveChangesAsync();

        //make sure it is there
       try
        {
          var student = _context.Students.Where(s => s.Id == 1).FirstOrDefault();
          if (student != null)
          {
            student.Enrollments.Add(new Enrollment() { Course = _context.Courses.Find(1), Semester = _context.Semesters.Find(1), Grade = 0 });
            student.Enrollments.Add(new Enrollment() { Course = _context.Courses.Find(3), Semester = _context.Semesters.Find(1), Grade = 0 });
            student.Enrollments.Add(new Enrollment() { Course = _context.Courses.Find(4), Semester = _context.Semesters.Find(2), Grade = 0 });
            student.Enrollments.Add(new Enrollment() { Course = _context.Courses.Find(2), Semester = _context.Semesters.Find(2), Grade = 0 });


          }
          await _context.SaveChangesAsync();
        }
        catch (Exception ex)
        {
          
        }


      }
    }
  }
}
