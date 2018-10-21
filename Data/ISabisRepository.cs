using System.Collections.Generic;
using System.Threading.Tasks;
using SabisTest.Entities;

namespace SabisTest.Data
{
    public interface ISabisRepository
    {
        Task<IEnumerable<Course>> GetCourses();
        Task<IEnumerable<Enrollment>> GetEnrollments(int? studentId = null, int? courseId = null, int? semesterId = null);
        Task<IEnumerable<Semester>> GetSemesters();
        Task<IEnumerable<Student>> GetStudents();
    Task<Student> GetStudentById(int id);
    Task<Course> GetCourseById(int id);
    Task<Semester> GetSemesterById(int id);
    Task<Enrollment> GetEnrollmentById(int id);
    Task<int> SaveStudent(Student student);
    Task<int> SaveEnrollment(Enrollment enrollment);

    Task<bool> SaveAsync();
    }
}