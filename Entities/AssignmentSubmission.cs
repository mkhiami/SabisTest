using System;
namespace SabisTest.Entities
{
  public class AssignmentSubmission:BaseEntity
  {
    public AssignmentSubmission()
    {
    }
    public int Grade
    {
      get;
      set;
    }

    public int EnrollmentId
    {
      get;
      set;
    }

    public Enrollment Enrollment
    {
      get;
      set;
    }

    public int AssignmentId
    {
      get;
      set;
    }

    public Assesment Assesment
    {
      get;
      set;
    }

  }
}
