
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using RecipePlatform.BLL.Interface;
using RecipePlatform.BLL.Repo_s;
using RecipePlatform.DAL.Context;
using RecipePlatform.Models;

namespace RecipePlatform.API
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();
            // // Add services to the container.
            builder.Services.AddControllersWithViews();
            builder.Services.AddDbContext<ApplicationDbContext>(options =>
           options.UseSqlServer(builder.Configuration.GetConnectionString("Connection")));


            //identity 
            builder.Services.AddIdentity<ApplicationUser, IdentityRole>(options =>
            {
                // lock account setting
                options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromHours(1); // 1 hr
                options.Lockout.MaxFailedAccessAttempts = 5; // 5 tries 
                options.Lockout.AllowedForNewUsers = true;  // lock it if 5 times wrong password 

                // password setting )
                options.Password.RequireDigit = true;
                options.Password.RequiredLength = 8;
                options.Password.RequireNonAlphanumeric = true;
                options.Password.RequireUppercase = true;
                options.Password.RequireLowercase = true;
            })
            .AddEntityFrameworkStores<ApplicationDbContext>()
            .AddDefaultTokenProviders();

            //repo 
          //  builder.Services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));


            //for controllers but make sure you do have servives in repo
            builder.Services.AddScoped<IRecipeService, RecipeService>();
            //  builder.Services.AddScoped<IRatingService, RatingService>();
            builder.Services.AddScoped<ICategoryService, CategoryService>();




            //a
            builder.Services.AddAuthorization();


            //--------------------------------------------------




            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }

        //user role 

        //role
        public static async Task SeedRolesAndAdminUser(IServiceProvider serviceProvider)
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
