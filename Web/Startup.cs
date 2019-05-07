using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Web.Data;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Swashbuckle.AspNetCore.Swagger;
using Web.Models;
using Microsoft.AspNetCore.Mvc.Razor;
using System.Globalization;
using Microsoft.AspNetCore.Localization;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using Pomelo.EntityFrameworkCore.MySql.Infrastructure;

namespace Web
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddCors(o => o.AddPolicy("EmailPolicy", builder =>
            {
                builder.AllowAnyOrigin()
                    .AllowAnyMethod()
                    .AllowAnyHeader()
                    .AllowCredentials();
            }));

            services.Configure<CookiePolicyOptions>(options =>
            {
                // This lambda determines whether user consent for non-essential cookies is needed for a given request.
                options.CheckConsentNeeded = context => true;
                options.MinimumSameSitePolicy = SameSiteMode.None;
            });

            services.Configure<EmailConfiguration>(Configuration.GetSection("EmailConfiguration"));

            // string executableLocation = System.Reflection.Assembly.GetExecutingAssembly().Location;
            // string path = Path.GetDirectoryName(executableLocation).Split("bin")[0];
            //string cs = Configuration.GetConnectionString("DefaultConnection");
            // string[] csSplit = cs.Split("=");
            // cs = csSplit[0] + "=" + path + csSplit[1];

            //Choosing a db service
            CheckDB check = new CheckDB();

            //check for environment variables (described in docs/dbconfig.md)
            //if variable is not set, grab from appsettings.json
            String ConnectionString = check.getConnectionStringEnvVar() ?? Configuration.GetConnectionString("DefaultConnection");

            //if not set just use sqlite
            String DatabaseType = check.checkType() ?? "sqlite";

            switch (DatabaseType)
            {
                case "mssql":
                    var host = Configuration["DBHOST"] ?? "172.19.0.1";
                    var db = Configuration["DBNAME"] ?? "openvoting";
                    var port = Configuration["DBPORT"] ?? "1433";
                    var username = Configuration["DBUSERNAME"] ?? "sa";
                    var password = Configuration["DBPASSWORD"] ?? "Sql!Expre55";

                    string connStr = $"Data Source={host},{port};Integrated Security=False;";
                    connStr += $"User ID={username};Password={password};Database={db};";
                    connStr += $"Connect Timeout=30;Encrypt=False;TrustServerCertificate=True;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";
                    services.AddDbContext<ApplicationDbContext>(options =>
                        options.UseSqlServer(connStr));
                    break;
                case "mysql":
                    host = Configuration["DBHOST"] ?? "localhost";
                    port = Configuration["DBPORT"] ?? "3306";
                    password = Configuration["DBPASSWORD"] ?? "secret";
                    db = Configuration["DBNAME"] ?? "openvoting";
                    services.AddDbContext<ApplicationDbContext>(options =>
                    {
                        options.UseMySql($"server={host}; userid=root; pwd={password};"
                            + $"port={port}; database={db}");
                    });
                    break;
                default: //sqlite
                    services.AddDbContext<ApplicationDbContext>(options =>
                        options.UseSqlite(ConnectionString));
                    break;
            }



            /*     
  services.AddDefaultIdentity<IdentityUser>()
      .AddDefaultUI(UIFramework.Bootstrap4)
      .AddEntityFrameworkStores<ApplicationDbContext>();*/
            services.AddIdentity<IdentityUser, IdentityRole>(
               option =>
               {
                   option.Password.RequireDigit = false;
                   option.Password.RequiredLength = 6;
                   option.Password.RequireNonAlphanumeric = false;
                   option.Password.RequireUppercase = false;
                   option.Password.RequireLowercase = false;
               }
           ).AddEntityFrameworkStores<ApplicationDbContext>()
           .AddDefaultTokenProviders()
           .AddDefaultUI(UIFramework.Bootstrap4);


            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new Info
                {
                    Title = "VotingTool API",
                    Version = "v1",
                    Description = "VotingTool Backend REST API Service Documentation\nhttps://github.com/CstHub/votingtool"
                });

            });

            services.AddLocalization(opts =>
            {
                opts.ResourcesPath = "Resources";
            });

            services.AddMvc()
            .AddViewLocalization(LanguageViewLocationExpanderFormat.Suffix)
            .AddDataAnnotationsLocalization()
            .SetCompatibilityVersion(CompatibilityVersion.Version_2_2);

            services.Configure<RequestLocalizationOptions>(opts =>
            {
                var supportedCultures = new List<CultureInfo> {
                    new CultureInfo("en"),
                    new CultureInfo("fr"),
                  };

                opts.DefaultRequestCulture = new RequestCulture("en");
                opts.SupportedCultures = supportedCultures;
                opts.SupportedUICultures = supportedCultures;
            });

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, ApplicationDbContext context, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseDatabaseErrorPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseCors("EmailPolicy");
            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseCookiePolicy();

            app.UseAuthentication();

            app.UseSwagger();

            // https://localhost:<port>/swagger
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "VotingTool API V1");
            });

            var options = app.ApplicationServices.GetService<IOptions<RequestLocalizationOptions>>();
            app.UseRequestLocalization(options.Value);

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });

            DummyData.Initialize(context, app).Wait();
            StateInit.Initialize(context);
            ThemesInit.Initialize(context);
        }
    }
}