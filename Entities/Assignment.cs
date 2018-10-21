using System;
namespace SabisTest.Entities
{
  public enum AssesmentType {
    Assignment =1, Project=4,Exam=2, FinalExam=3

  }

  public class Assesment:BaseEntity
  {
    public Assesment()
    {
    }

    public int GradeRatio
    {
      get;
      set;
    }

    public DateTime AnnoucmentDate
    {
      get;
      set;
    }

    public DateTime DuetDate
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
    public Semester Semester
    {
      get;
      set;
    }

    public Course Course
    {
      get;
      set;
    }
  }
}
