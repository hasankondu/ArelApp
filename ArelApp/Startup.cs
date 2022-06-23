using ArelApp.Business.Abstract;
using ArelApp.Business.Concrete;
using ArelApp.DataAccess;
using ArelApp.DataAccess.Abstract;
using ArelApp.DataAccess.Concrete;
using ArelApp.Entities.Concrete;
using ArelApp.UI.Identity;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;

namespace ArelApp
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
            services.AddDbContext<ArelAppAutomationContext>();

            services.AddIdentity<User, Role>()
                .AddEntityFrameworkStores<ArelAppAutomationContext>()
                .AddDefaultTokenProviders();

            services.Configure<IdentityOptions>(options =>
            {
                options.Password.RequireDigit = false;
                options.Password.RequireLowercase = false;
                options.Password.RequiredLength = 5;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequireUppercase = false;

                options.Lockout.MaxFailedAccessAttempts = 5;
                options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(5);
                options.Lockout.AllowedForNewUsers = true;

                options.User.AllowedUserNameCharacters = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789-._@+çÇðÐüÜýÝþÞöÖ!";
                options.User.RequireUniqueEmail = true;

                options.SignIn.RequireConfirmedEmail = false;
                options.SignIn.RequireConfirmedPhoneNumber = false;
            });


            services.AddScoped<ILectureDal, EfLectureDal>();
            services.AddScoped<ILectureService, LectureService>();
            services.AddScoped<IDepartmentDal, EfDepartmentDal>();
            services.AddScoped<IDepartmentService, DepartmentService>();
            services.AddScoped<IRoleService, RoleService>();
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IUserDal, EfUserDal>();
            services.AddScoped<IExamService, ExamService>();
            services.AddScoped<IExamDal, EfExamDal>();
            services.AddScoped<IAppointmentService, AppointmentService>();
            services.AddScoped<IAppointmentDal, EfAppointmentDal>();
            services.AddControllersWithViews();

            //services.AddIdentity<User, Role>(config =>
            //{
            //    config.Password.RequireNonAlphanumeric = false; //optional
            //    config.SignIn.RequireConfirmedEmail = true; //optional
            //})
            //.AddEntityFrameworkStores<ArelAppAutomationContext>()
            //.AddDefaultTokenProviders();

            services.ConfigureApplicationCookie(options =>
            {
                options.LoginPath = "/account/login";
                options.LogoutPath = "/account/logout";
                options.AccessDeniedPath = "/account/accessdenied";
                options.ExpireTimeSpan = TimeSpan.FromMinutes(60);
                options.SlidingExpiration = true;
                options.Cookie = new CookieBuilder
                {
                    HttpOnly = true,
                    Name="ArelApp.Security.Cookie",
                    SameSite=SameSiteMode.Strict

                };
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env,UserManager<User> userManager,RoleManager<Role> roleManager)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            app.UseHttpsRedirection();
            app.UseStaticFiles();
            SeedIdentity.Seed(userManager,roleManager,Configuration).Wait();
            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {

                endpoints.MapControllerRoute(
                    name: "adminUsers",
                    pattern: "user/update/{id?}",
                    defaults: new { controller = "User", action = "Update" }
                    );


                endpoints.MapControllerRoute(
                    name: "adminDepartments",
                    pattern: "department/update/{id?}",
                    defaults: new { controller = "Department", action = "Update" }
                    );

                endpoints.MapControllerRoute(
                    name: "adminLectures",
                    pattern: "lecture/update/{id?}",
                    defaults: new { controller = "Lecture", action = "Update" }
                    );
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=User}/{action=Login}/{id?}");
            });
           
        }
    }
}