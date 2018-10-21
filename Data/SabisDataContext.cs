using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using SabisTest.Entities;

namespace SabisTest.Data
{
  public class SabisDataContext : IdentityDbContext<UserInfo>
  {
    private readonly IUserService _userService;

    #region DBSets
    public DbSet<Course> Courses
    {
      get;
      set;
    }

    public DbSet<Semester> Semesters
    {
      get;
      set;
    }

    public DbSet<Student> Students
    {
      get;
      set;
    }

    public DbSet<Assesment> Assesments
    {
      get;
      set;
    }


    public DbSet<AssignmentSubmission> AssesmentSubmissions
    {
      get;
      set;
    }

    public DbSet<Enrollment> Enrollments
    {
      get;
      set;
    }
    #endregion



    public SabisDataContext(DbContextOptions<SabisDataContext> options, IUserService userService) : base(options)
    {
      // userService is a required argument
      _userService = userService ?? throw new ArgumentNullException(nameof(userService));
    }

    public override Task<int> SaveChangesAsync(CancellationToken cancellationToken
        = default(CancellationToken))
    {
      // get added or updated entries
      var addedOrUpdatedEntries = ChangeTracker.Entries()
              .Where(x => (x.State == EntityState.Added || x.State == EntityState.Modified));

      // fill out the audit fields
      foreach (var entry in addedOrUpdatedEntries)
      {

        var entity = entry.Entity as BaseEntity;
        if(entity != null) { //account for none BaseEntity types (the user tables)

          if (entry.State == EntityState.Added)
          {
            entity.CreatedBy = _userService.UserId;
            entity.Created = DateTime.UtcNow;
          }

          entity.ModifiedBy = _userService.UserId;
          entity.Modified = DateTime.UtcNow;

        }
      }


      return base.SaveChangesAsync(cancellationToken);
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
      base.OnModelCreating(modelBuilder);
      return;
      modelBuilder.Entity<Student>().HasData(
        new Student() { NameEn = "Mohammad", NameAr = "Mohammad-A", Gender = Gender.Male, DateOfBirth = new DateTime(1990, 1, 1), Major="Computer Science" },
                new Student() { NameEn = "Omar", NameAr = "Omar-A", Gender = Gender.Male, DateOfBirth = new DateTime(2011, 1, 1), Major = "Computer Science" },
                new Student() { NameEn = "Khaled", NameAr = "Khaled-A", Gender = Gender.Male, DateOfBirth = new DateTime(2015, 1, 1), Major = "Computer Science" }

           );
      modelBuilder.Entity<Semester>().HasData(
        new Semester() { Term= Term.Fall, Year=2018, Start =  new DateTime(2018,1,1), End= new DateTime(2018, 4, 1), Title= "FALL-2018" }, 
        new Semester { Term = Term.Spring, Year = 2018, Start = new DateTime(2018, 4, 1), End = new DateTime(2018, 8, 1), Title = "SPR-2018" } );
      modelBuilder.Entity<Course>().HasData(
        new Course() { NameAr=  "Course 1-A", NameEn="Course 1"}, 
        new Course() { NameAr = "Course 2-A", NameEn = "Course 2" }, 
        new Course() { NameAr = "Course 3-A", NameEn = "Course 3" }, 
        new Course() { NameAr = "Course 4-A", NameEn = "Course 4" }
      );

      modelBuilder.Entity<Enrollment>().HasData(
                new Enrollment() { CourseId=1, SemesterId=1, StudentId=1, Grade=90} ,
                new Enrollment() { CourseId = 2, SemesterId = 1, StudentId = 1, Grade = 90 },
                new Enrollment() { CourseId = 1, SemesterId = 1, StudentId = 2, Grade = 90 },
                new Enrollment() { CourseId = 2, SemesterId = 1, StudentId = 2, Grade = 90 },
                new Enrollment() { CourseId = 3, SemesterId = 2, StudentId = 3, Grade = 90 },
                new Enrollment() { CourseId = 4, SemesterId = 2, StudentId = 3, Grade = 90 },
                new Enrollment() { CourseId = 3, SemesterId = 2, StudentId = 1, Grade = 90 },
                new Enrollment() { CourseId = 4, SemesterId = 2, StudentId = 1, Grade = 90 }
     );
    }//end OnModelCreating



  }
}
