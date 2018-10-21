using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using SabisTest.Entities;
using SabisTest.Models;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace SabisTest.Controllers
{
  public class AccountController : Controller
  {
    private readonly ILogger<AccountController> _logger;
    private readonly SignInManager<UserInfo> _signInManager;
    private readonly UserManager<UserInfo> _userManager;
    private readonly IConfiguration _config;

    public AccountController(ILogger<AccountController> logger,
      SignInManager<UserInfo> signInManager,
      UserManager<UserInfo> userManager,
      IConfiguration config)
    {
      _logger = logger;
      _signInManager = signInManager;
      _userManager = userManager;
      _config = config;
    }

    // GET: /<controller>/
    public IActionResult Index()
    {
      return Ok("Hi!");
    }




    /// <summary>
    /// For bearer authentication
    /// </summary>
    /// <returns>The token.</returns>
    /// <param name="model">Model. with username and password</param>
    [HttpPost]
    [Route("/api/account/createtoken")]
    public async Task<IActionResult> CreateToken([FromBody] LoginViewModel model)
    {
      if (ModelState.IsValid)
      {
        var user = await _userManager.FindByNameAsync(model.Username);

        if (user != null)
        {
          var result = await _signInManager.CheckPasswordSignInAsync(user, model.Password, false);

          if (result.Succeeded)
          {
            // Create the token
            var claims = new[]
            {
              new Claim(JwtRegisteredClaimNames.Sub, user.Email),
              new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
              new Claim(JwtRegisteredClaimNames.UniqueName, user.UserName)
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Tokens:Key"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
              _config["Tokens:Issuer"],
              _config["Tokens:Audience"],
              claims,
              expires: DateTime.Now.AddMinutes(30),
              signingCredentials: creds);

            var results = new
            {
              token = new JwtSecurityTokenHandler().WriteToken(token),
              expiration = token.ValidTo
            };

            return Created("", results);
          }
        }
      }

      return BadRequest();
    }
  }
}
