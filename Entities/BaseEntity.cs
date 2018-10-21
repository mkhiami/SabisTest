using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using SabisTest.Helpers;

namespace SabisTest.Entities
{
  public interface ISoftDelete
  {
    bool IsDeleted { get; set; }
  }


  public interface IName
  {
    string NameEn { get; set; }
    string NameAr { get; set; }

  }

  public interface ITitle
  {
    string Title { get; set; }

  }

  public abstract class BaseEntity : ISoftDelete
  {
    #region Properties
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    //[Column("Id", Order = 1)]
    public int? Id { get; set; }

    [NotMapped]
    public string Name { get { if (this is IName) return Utilities.IsArabic() ? ((IName)this).NameAr : ((IName)this).NameEn; else if (this is ITitle) return ((ITitle)this).Title; else return this.GetType().ToString(); } }


    [ScaffoldColumn(false)]
    public bool IsDeleted { get; set; }
    [Required]
    public DateTime? Created { get; set; }
    [Required]
    public string CreatedBy { get; set; }
    public DateTime? Modified { get; set; }
    public string ModifiedBy { get; set; }
    #endregion

  }
}
