using System;
namespace SabisTest.Entities
{
  public enum Term{
    Fall,Spring, Summer

  }
  public class Semester : BaseEntity, ITitle
  {
    public Semester()
    {
    }

    public int Year
    {
      get;
      set;
    }

    public Term Term
    {
      get;
      set;
    }

    public DateTime Start
    {
      get;
      set;
    }

    public DateTime End
    {
      get;
      set;
    }
    public string Title { get; set; }
  }
}
