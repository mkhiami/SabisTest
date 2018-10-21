using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SabisTest.Entities
{
  public class Course: BaseEntity, IName
  {
    [Required]
    [MaxLength(250)]
    public string NameEn { get; set; }

    [Required]
    [MaxLength(250)]
    public string NameAr { get; set; }


  }
}
