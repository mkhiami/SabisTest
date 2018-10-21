using System;
namespace SabisTest.Data
{
  public class UserService : IUserService
  {
 

    public string Roles
    {
      get;
      set;
    }

    public string FirstName
    {
      get;
      set;
    }
    public string LastName
    {
      get;
      set;
    }

    public string UserId { get; set; }

    public UserService()
    {
      FirstName = "Mohammad";
      LastName = "khiami";
      UserId = "mkhiami";
      Roles = "Administrator";
    }
  }
}
