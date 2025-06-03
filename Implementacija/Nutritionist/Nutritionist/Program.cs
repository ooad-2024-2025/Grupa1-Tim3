using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Nutritionist.Data;
using Nutritionist.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection") ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(connectionString));
builder.Services.AddDatabaseDeveloperPageExceptionFilter();

builder.Services.AddDefaultIdentity<User>(options => options.SignIn.RequireConfirmedAccount = false)
    .AddRoles<IdentityRole<Guid>>()
    .AddEntityFrameworkStores<ApplicationDbContext>();
builder.Services.AddControllersWithViews();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseMigrationsEndPoint();
}
else
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");
app.MapRazorPages();

//using (var scope = app.Services.CreateScope())
//{
//    var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole<Guid>>>();

//    // The three roles you want in your system:
//    string[] rolesToSeed = new[] { "Korisnik", "Admin", "Nutricionista" };

//    foreach (var roleName in rolesToSeed)
//    {
//        // Check if this role already exists
//        var exists = roleManager.RoleExistsAsync(roleName).GetAwaiter().GetResult();
//        if (!exists)
//        {
//            // Create the role
//            var role = new IdentityRole<Guid>(roleName);
//            roleManager.CreateAsync(role).GetAwaiter().GetResult();
//        }
//    }
//}

app.Run();
