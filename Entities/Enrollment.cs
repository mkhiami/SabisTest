using System;
namespace SabisTest.Entities
{
  public class Enrollment: BaseEntity
  {
    public Enrollment()
    {
    }

    public int StudentId
    {
      get;
      set;
    }

    public int CourseId
    {
      get;
      set;
    }


    public int SemesterId
    {
      get;
      set;
    }



    public Student Student
    {
      get;
      set;
    }

    public Course Course
    {
      get;
      set;
    }

    public Semester Semester
    {
      get;
      set;
    }

    public decimal Grade
    {
      get;
      set;
    }

  }
}
