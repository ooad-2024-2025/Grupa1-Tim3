using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Nutritionist.Data;
using Nutritionist.Models;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.AspNetCore.Authorization;
using Azure.Storage.Blobs;
using SendGrid;
using SendGrid.Helpers.Mail;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.Extensions.Options;

var builder = WebApplication.CreateBuilder(args);

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection") ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(connectionString));
builder.Services.AddDatabaseDeveloperPageExceptionFilter();

builder.Services.AddDefaultIdentity<User>(options => options.SignIn.RequireConfirmedAccount = false)
    .AddRoles<IdentityRole<Guid>>()
    .AddEntityFrameworkStores<ApplicationDbContext>();
builder.Services.AddControllersWithViews();

var blobConfig = builder.Configuration.GetSection("AzureBlobStorage");
builder.Services.AddSingleton(sp =>
    new BlobContainerClient(
        blobConfig["ConnectionString"],
        blobConfig["ContainerName"]));

builder.Services.Configure<SendGridOptions>(builder.Configuration.GetSection("SendGrid"));
builder.Services.AddTransient<IEmailSender, SendGridEmailSender>();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseMigrationsEndPoint();
}
else
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
    name: "areas",
    pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}"
    );

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

public class SendGridOptions
{
    public string ApiKey { get; set; }
}

public class SendGridEmailSender : IEmailSender
{
    private readonly SendGridOptions _options;
    public SendGridEmailSender(IOptions<SendGridOptions> opts) => _options = opts.Value;

    public async Task SendEmailAsync(string email, string subject, string htmlMessage)
    {
        var client = new SendGridClient(_options.ApiKey);
        var msg = new SendGridMessage
        {
            From = new EmailAddress("edzafo1@etf.unsa.ba", "Nutritionist"),
            Subject = subject,
            HtmlContent = htmlMessage
        };
        msg.AddTo(new EmailAddress(email));
        await client.SendEmailAsync(msg);
    }
}