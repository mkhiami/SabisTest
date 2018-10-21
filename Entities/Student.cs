using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace SabisTest.Entities
{
  public enum Gender { Male, Female }
  public class Student : BaseEntity, IName
  {
    public Student()
    {

    }

    [Required]
    [MaxLength(250)]
    public string NameEn { get; set; }

    [Required]
    [MaxLength(250)]
    public string NameAr { get; set; }


    public string Major { get; set; }

    public DateTime DateOfBirth { get; set; }
    public Gender Gender
    {
      get;
      set;
    }

    public List<Enrollment> Enrollments
    {
      get;
      set;
    }
  }
}

