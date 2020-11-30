using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc.Razor;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using Web.ApiControllers;
using Web.Data;
using Web.Models;
using Microsoft.OpenApi.Models;
using Microsoft.Extensions.Logging;
using System.Diagnostics;

namespace Web
{
    public class Startup
    {
        public IConfiguration _configuration { get; }
        public IWebHostEnvironment _hostingEnvironment { get; }

        public Startup(IConfiguration configuration,
            IWebHostEnvironment hostingEnvironment)
        {
            _configuration = configuration;
            _hostingEnvironment = hostingEnvironment;
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddCors(options =>
            {
                options.AddPolicy("EmailPolicy",
                    builder => builder
                        .AllowAnyOrigin()
                        .AllowAnyMethod()
                        //.AllowCredentials()
                        .AllowAnyHeader()
                );
            });

            services.Configure<CookiePolicyOptions>(options =>
            {
                // This lambda determines whether user consent for non-essential cookies is needed for a given request.
                options.CheckConsentNeeded = context => true;
                options.MinimumSameSitePolicy = SameSiteMode.None;
            });

            services.Configure<EmailConfiguration>(_configuration.GetSection("EmailConfiguration"));
            services.Configure<MapConfiguration>(_configuration.GetSection("MapConfiguration"));

            //Choosing a db service
            CheckDB check = new CheckDB();

            //check for environment variables (described in docs/dbconfig.md)
            //if variable is not set, grab from appsettings.json
            String ConnectionString = check.getConnectionStringEnvVar() ?? _configuration.GetConnectionString("DefaultConnection");

            //if not set just use sqlite
            // String DatabaseType = check.checkType() ?? "sqlite";
            String DatabaseType = "";

            switch (DatabaseType)
            {
                case "mssql":
                    var host = _configuration["DBHOST"] ?? "localhost";
                    var port = _configuration["DBPORT"] ?? "1444";
                    var db = _configuration["DBNAME"] ?? "openvoting";
                    var username = _configuration["DBUSERNAME"] ?? "sa";
                    var password = _configuration["DBPASSWORD"] ?? "SqlExpress!";

                    string connStr = $"Server=tcp:{host}, {port};Database={db};UID={username};PWD={password};";
                    // string connStr = $"Data Source={host},{port};Integrated Security=False;";
                    // connStr += $"User ID={username};Password={password};Database={db};";
                    // connStr += $"Connect Timeout=30;Encrypt=False;TrustServerCertificate=True;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";
                    services.AddDbContext<ApplicationDbContext>(options =>
                        options.UseSqlServer(connStr));
                    break;
                case "mysql":
                    host = _configuration["DBHOST"] ?? "localhost";
                    port = _configuration["DBPORT"] ?? "3306";
                    password = _configuration["DBPASSWORD"] ?? "";
                    db = _configuration["DBNAME"] ?? "openvoting";
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

            services.AddDefaultIdentity<IdentityUser>(
             options =>
             {
                 options.Password.RequireDigit = false;
                 options.Password.RequiredLength = 6;
                 options.Password.RequireNonAlphanumeric = false;
                 options.Password.RequireUppercase = false;
                 options.Password.RequireLowercase = false;
                 options.SignIn.RequireConfirmedAccount = false;
             })
                .AddRoles<IdentityRole>()
            .AddEntityFrameworkStores<ApplicationDbContext>()
            .AddDefaultTokenProviders();

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo
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

            services.AddControllersWithViews()
            .AddViewLocalization(LanguageViewLocationExpanderFormat.Suffix)
            .AddDataAnnotationsLocalization()
            .AddJsonOptions(
                options =>
                {
                    options.JsonSerializerOptions.PropertyNameCaseInsensitive = true;
                    options.JsonSerializerOptions.PropertyNamingPolicy = null;

                    //options.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
                    //options.SerializerSettings.Converters.Add(new Newtonsoft.Json.Converters.StringEnumConverter());
                }
            );

            services.AddRazorPages();

            // 2.2   services.AddHttpClient();

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

            try
            {
                var local_access_token = _configuration["mapkey"];
                if (!string.IsNullOrEmpty(local_access_token))
                {
                    if (string.IsNullOrEmpty(MapController.AccessToken))
                    {
                        MapController.AccessToken = local_access_token;
                    }
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(
            IApplicationBuilder app,
            ApplicationDbContext context,
            IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();

            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                app.UseHsts();
            }

            app.UseCors("EmailPolicy");
            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseRouting();
            // 22 app.UseCookiePolicy();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseSwagger();

            // https://localhost:<port>/swagger
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "VotingTool API V1");
            });

            var options = app.ApplicationServices.GetService<IOptions<RequestLocalizationOptions>>();
            app.UseRequestLocalization(options.Value);

            // app.UseMvc(routes =>
            // {
            //     routes.MapRoute(
            //         name: "default",
            //         template: "{controller=Home}/{action=Index}/{id?}");
            // });

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
                endpoints.MapRazorPages();
            });


            context.Database.EnsureCreated();

            if (!context.Elections.Any())
            {
                SeedData.Initialize(context);
                AccountsInit.InitializeAsync(app);
                StateInit.Initialize(context);
                ThemesInit.Initialize(context);
            }
        }
    }
}