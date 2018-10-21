using System;
using System.ComponentModel.DataAnnotations;
using SabisTest.Entities;

namespace SabisTest.Models
{
  public class StudentViewModel
  {
    public int Id
    {
      get;
      set;
    }//
    public int Gender
    {
      get;
      set;
    }//
    [Required]
    public string NameEn { get; set; }
    [Required]
    public string NameAr { get; set; }
    public DateTime DateOfBirth
    {
      get;
      set;
    } 
    public string Major
    {
      get;
      set;
    }

    /// <summary>
    /// Will return a student entity, should have used an aternative maping like automapper but short on time
    /// </summary>
    /// <returns>The student.</returns>
    public Student ToStudent(){
      return new Student() { Id = this.Id, NameAr = this.NameAr, NameEn = this.NameEn, DateOfBirth = this.DateOfBirth, Gender = this.Gender==0? SabisTest.Entities.Gender.Male: SabisTest.Entities.Gender.Female };

    }

  }
}
