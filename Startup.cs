using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Razor;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using Swashbuckle.AspNetCore.Swagger;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Reflection;
using Web.ApiControllers;
using Web.Data;
using Web.Models;
using Web;

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
            services.Configure<MapConfiguration>(Configuration.GetSection("MapConfiguration"));

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
            .AddDataAnnotationsLocalization(
                // config to use sharedResource as localization provider for data annotation
                options => { options.DataAnnotationLocalizerProvider = (type, factory) => factory.Create(typeof(SharedResource)); })
            .SetCompatibilityVersion(CompatibilityVersion.Version_2_2)
            .AddJsonOptions(
                options =>
                {
                    options.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
                    options.SerializerSettings.Converters.Add(new Newtonsoft.Json.Converters.StringEnumConverter());
                    
                });

            services.AddHttpClient();

            services.Configure<RequestLocalizationOptions>(opts =>
            {
                var supportedCultures = new List<CultureInfo> {
                    new CultureInfo("en"),
                    new CultureInfo("fr"),
                    new CultureInfo("es")
                };

                opts.DefaultRequestCulture = new RequestCulture("en");
                opts.SupportedCultures = supportedCultures;
                opts.SupportedUICultures = supportedCultures;
            });

            try
            {
                var local_access_token = Configuration["mapkey"];
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

            }
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(
            IApplicationBuilder app,
            ApplicationDbContext context,
            IHostingEnvironment env)
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

            
            var options = app.ApplicationServices.GetService<IOptions<RequestLocalizationOptions>>();
            app.UseRequestLocalization(options.Value);

            app.UseCookiePolicy();

            app.UseAuthentication();

            app.UseSwagger();

            // https://localhost:<port>/swagger
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "VotingTool API V1");
            });
            
            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });

            context.Database.EnsureCreated();


            AccountsInit.InitializeAsync(app);
            StateInit.Initialize(context);
            if (!context.Themes.Any())
            {
                ThemesInit.Initialize(context);
            }


        }
    }
}