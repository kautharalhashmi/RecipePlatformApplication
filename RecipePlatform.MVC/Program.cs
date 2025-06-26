using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using RecipePlatform.BLL.Interface;
using RecipePlatform.BLL.Repo_s;
using RecipePlatform.DAL.Context;
using RecipePlatform.DAL.Interface;
using RecipePlatform.DAL.Repo_s;
using RecipePlatform.Models;
using System;
using System.Threading.Tasks;

namespace RecipePlatform.MVC
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // 1. Configure EF Core with your connection string
            builder.Services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(builder.Configuration.GetConnectionString("Connection")));

            // 2. Configure Identity with ApplicationUser and roles
            builder.Services.AddIdentity<ApplicationUser, IdentityRole>(options =>
            {
                options.Password.RequireDigit = true;
                options.Password.RequireLowercase = true;
                options.Password.RequiredLength = 6;
                options.Password.RequireUppercase = false;
                options.Password.RequireNonAlphanumeric = false;
            })
            .AddEntityFrameworkStores<ApplicationDbContext>()
            .AddDefaultTokenProviders();

            // 3. Configure cookie settings for login/logout
            builder.Services.ConfigureApplicationCookie(options =>
            {
                options.LoginPath = "/Account/Login";
                options.AccessDeniedPath = "/Account/AccessDenied";
            });

            // 4. Add MVC with views
            builder.Services.AddControllersWithViews();


            // DAL Generic Repo
            builder.Services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));

            // DAL Specific Repos
            builder.Services.AddScoped<ICategoryRepository, CategoryRepository>();
            builder.Services.AddScoped<IRecipeRepository, RecipeRepository>();
            builder.Services.AddScoped<IRatingRepository, RatingRepository>();

            // BLL Services
            builder.Services.AddScoped<ICategoryService, CategoryService>();
            builder.Services.AddScoped<IRecipeService, RecipeService>();
            builder.Services.AddScoped<IRatingService, RatingService>();
            builder.Services.AddRazorPages();

            var app = builder.Build();

            // 6. Seed roles on startup
            using (var scope = app.Services.CreateScope())
            {
                var services = scope.ServiceProvider;
                try
                {
                    await SeedRoles.Initialize(services);
                }
                catch (Exception ex)
                {
                    var logger = services.GetRequiredService<ILogger<Program>>();
                    logger.LogError(ex, "An error occurred seeding the DB roles.");
                }
            }
          
            // Configure middleware
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Recipe}/{action=AllHome}/{id?}");

            await app.RunAsync();
        }


        // SeedRoles class to initialize roles
        //public static class SeedRoles
        //{
        //    public static async Task Initialize(IServiceProvider serviceProvider)
        //    {
        //        var roleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();

        //        string[] roleNames = { "Admin", "User" };

        //        foreach (var roleName in roleNames)
        //        {
        //            var roleExist = await roleManager.RoleExistsAsync(roleName);
        //            if (!roleExist)
        //            {
        //                await roleManager.CreateAsync(new IdentityRole(roleName));
        //            }
        //        }
        //    }
        //}
        public static class SeedRoles
        {
            public static async Task Initialize(IServiceProvider serviceProvider)
            {
                using var scope = serviceProvider.CreateScope();

                var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();
                var userManager = scope.ServiceProvider.GetRequiredService<UserManager<ApplicationUser>>();


                string[] roles = new[] { "Admin", "User" };
                foreach (var role in roles)
                {
                    if (!await roleManager.RoleExistsAsync(role))
                    {
                        await roleManager.CreateAsync(new IdentityRole(role));
                    }
                }

                //first admin 
                string adminEmail = "admin@example.com";
                string adminPassword = "Admin123!";

                var adminUser = await userManager.FindByEmailAsync(adminEmail);
                if (adminUser == null)
                {
                    var user = new ApplicationUser
                    {
                        UserName = adminEmail,
                        Email = adminEmail,
                        EmailConfirmed = true,
                        FullName = "Admin441",
                        Gender = "NotSpecified",
                        Country = "Unknown"
                    };

                    var createUserResult = await userManager.CreateAsync(user, adminPassword);
                    if (createUserResult.Succeeded)
                    {
                        await userManager.AddToRoleAsync(user, "Admin");
                    }
                    else
                    {

                        throw new Exception("Failed to create the admin user during seeding.");
                    }
                }
            }
        }
}

}
