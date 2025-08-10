using BarRating.Data;
using BarRating.Data.Entities;
using BarRating.Data.Helpers;
using BarRating.Repository;
using BarRating.Service;
using BarRating.Service.Bar;
using BarRating.Service.HelpfulVote;
using BarRating.Service.Notification;
using BarRating.Service.Photo;
using BarRating.Service.Review;
using BarRating.Service.SavedBar;
using BarRating.Service.Schedule;
using BarRating.Service.User;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection") ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(connectionString));
builder.Services.AddDatabaseDeveloperPageExceptionFilter();

builder.Services.AddScoped<BarRepository>();
builder.Services.AddScoped<IBarService, BarService>();

builder.Services.AddScoped<BarScheduleRepository>();
builder.Services.AddScoped<ScheduleOverrideRepository>();
builder.Services.AddScoped<IScheduleService, ScheduleService>();

builder.Services.AddScoped<HelpfulVoteRepository>();
builder.Services.AddScoped<IHelpfulVoteService, HelpfulVoteService>();

builder.Services.AddScoped<SavedBarRepository>();
builder.Services.AddScoped<ISavedBarService, SavedBarService>();

builder.Services.AddScoped<ReviewRepository>();
builder.Services.AddScoped<IReviewService, ReviewService>();

builder.Services.AddScoped<IPhotoService, PhotoService>();

builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<UserRepository>();

builder.Services.AddScoped<INotificationService, NotificationService>();
builder.Services.AddScoped<NotificationRepository>();

builder.Services.Configure<CloudinarySettings>(builder.Configuration.GetSection("CloudinarySettings"));
builder.Services.AddDefaultIdentity<User>(options => options.SignIn.RequireConfirmedAccount = false)
    .AddRoles<Role>()
    .AddEntityFrameworkStores<ApplicationDbContext>();
builder.Services.AddControllersWithViews();
builder.Services.AddSignalR();
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

app.MapHub<NotificationHub>("/notificationHub");

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Bar}/{action=Index}/{id?}");
app.MapRazorPages();
using (var scope = app.Services.CreateScope())
{
    var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<Role>>();
    var userManager = scope.ServiceProvider.GetRequiredService<UserManager<User>>();

    var roles = new[] { "Admin","Moderator","Owner", "User" };
    foreach (var role in roles)
    {
        if (!await roleManager.RoleExistsAsync(role))
        {
            await roleManager.CreateAsync(new Role { Name = role });
        }
    }

    string firstname = "admin";
    string lastname = "admin";
    string username = "admin";
    string email = "admin@gmail.com";
    string password = "#Password1";
    if (await userManager.FindByEmailAsync(email) == null)
    {
        var user = new User
        {
            Email = email,
            FirstName = firstname,
            LastName = lastname,
            UserName = username
        };
        var result = await userManager.CreateAsync(user, password);
        if (result.Succeeded)
        {
            await userManager.AddToRoleAsync(user, "Admin");
        }
    }
}
app.Run();
