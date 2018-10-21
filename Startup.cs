using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SpaServices.AngularCli;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Console;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using SabisTest.Data;
using SabisTest.Entities;

namespace SabisTest
{
  public class Startup
  {

    #region Properties
    public IConfiguration Configuration { get; }
    public static readonly LoggerFactory MyLoggerFactory
      = new LoggerFactory(new[] { new ConsoleLoggerProvider((_, __) => true, true) });
    #endregion

    public Startup(IConfiguration configuration)
    {
      Configuration = configuration;
    }




    // This method gets called by the runtime. Use this method to add services to the container.
    public void ConfigureServices(IServiceCollection services)
    {
      services.AddIdentity<UserInfo, IdentityRole>(cfg => { cfg.User.RequireUniqueEmail = true; }).AddEntityFrameworkStores<SabisDataContext>();
      //both cookie and jwt supported 
      services.AddAuthentication()
        .AddCookie()
        .AddJwtBearer(cfg =>
        {
          cfg.TokenValidationParameters = new TokenValidationParameters()
          {
            ValidIssuer = Configuration["Tokens:Issuer"],
            ValidAudience = Configuration["Tokens:Audience"],
          IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration["Tokens:Key"]))
          };

        });
      //Add Loggin
      services.AddLogging();
      services.AddTransient<DataSeeder>();
      //Add Cors, I want to test it out from Azure when done
      services.AddCors(options =>
      {
        options.AddPolicy("AllowAllOriginsHeadersAndMethods",
            builder => builder.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod());
      });
      services.AddScoped<ISabisRepository, SabisRepository>();

      services.AddScoped<IUserService, UserService>();


      //I should have used n enviroment variable
      //TODO: make a enviroment variable insteas
      var connectionString = Configuration["ConnectionStrings:SabisTestContext"];
      services.AddDbContext<SabisDataContext>(o => o.UseLoggerFactory(MyLoggerFactory).UseSqlServer(connectionString));

      //make JSON more readable
      services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1)
                .AddJsonOptions(options =>
                {
                  options.SerializerSettings.DateParseHandling = DateParseHandling.DateTimeOffset;
                  options.SerializerSettings.ContractResolver =
                      new CamelCasePropertyNamesContractResolver();
                  options.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
                });
      // In production, the Angular files will be served from this directory
      services.AddSpaStaticFiles(configuration =>
      {
        configuration.RootPath = "ClientApp/dist";
      });

    }

    // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
    public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
    {
      if (env.IsDevelopment())
      {
        app.UseDeveloperExceptionPage();
      }
      else
      {
        app.UseHsts();
      }
      //loggerFactory.AddFile("Logs/ts-{Date}.txt");

      app.UseHttpsRedirection();
      //app.UseAuthentication();

      app.UseStaticFiles();
      app.UseSpaStaticFiles();
      app.UseCors("AllowAllOriginsHeadersAndMethods");
      app.UseMvc(routes =>
      {
        routes.MapRoute(
                  name: "default",
                  template: "{controller}/{action=Index}/{id?}");
      });

      app.UseSpa(spa =>
      {
        // To learn more about options for serving an Angular SPA from ASP.NET Core,
        // see https://go.microsoft.com/fwlink/?linkid=864501

        spa.Options.SourcePath = "ClientApp";

        if (env.IsDevelopment())
        {
          spa.UseAngularCliServer(npmScript: "start");
        }
      });

    }
  }
}
