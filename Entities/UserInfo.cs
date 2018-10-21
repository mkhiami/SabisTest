using System;
using Microsoft.AspNetCore.Identity;

namespace SabisTest.Entities
{
  public class UserInfo : IdentityUser
  {
    public string FirstName { get; set; }
    public string LastName { get; set; }

  }
}
