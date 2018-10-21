using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using SabisTest.Entities;
using System.Linq;



namespace SabisTest.Data
{
  public class SabisRepository : ISabisRepository
  {
    private readonly SabisDataContext _context;
    public SabisRepository(SabisDataContext context)
    {
      _context = context;
    }

    public async Task<bool> SaveAsync()
    {
      return (await _context.SaveChangesAsync() >= 0);
    }

    public async Task<IEnumerable<Course>> GetCourses()
    {
      return await _context.Courses.ToListAsync();
    }

    public async Task<IEnumerable<Student>> GetStudents()
    {
      return await _context.Students.ToListAsync();
    }

    public async Task<Student> GetStudentById(int id)
    {
      return await _context.Students.FindAsync(id);
    }

    public async Task<Course> GetCourseById(int id)
    {
      return await _context.Courses.FindAsync(id);
    }

    public async Task<Semester> GetSemesterById(int id)
    {
      return await _context.Semesters.FindAsync(id);
    }

    public async Task<Enrollment> GetEnrollmentById(int id)
    {
      return await _context.Enrollments.FindAsync(id);
    }

    public async Task<IEnumerable<Semester>> GetSemesters()
    {
      return await _context.Semesters.ToListAsync();
    }

    public async Task<IEnumerable<Enrollment>> GetEnrollments(int? studentId = null, int? courseId=null, int? semesterId=null)
    {
      return await _context.Enrollments.Where(enrollment=>(studentId == null || enrollment.StudentId == studentId)
                                              && (courseId == null || enrollment.CourseId == courseId)
                                              && (semesterId == null || enrollment.SemesterId == semesterId)).ToListAsync();
    }

    public Task<int> SaveStudent(Student student){
      if (student.Id == 0) _context.Add(student); else   _context.Update(student);
      return  _context.SaveChangesAsync();
    }


    public Task<int> SaveEnrollment(Enrollment enrollment)
    {
      if (enrollment.Id == 0) _context.Add(enrollment); else _context.Update(enrollment);
      return _context.SaveChangesAsync();
    }




  }
}
