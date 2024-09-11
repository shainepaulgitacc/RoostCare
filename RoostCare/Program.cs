using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using RoostCare.Models.Infrastracture;
using RoostCare.Models.Infrastracture.Implementation;
using RoostCare.Models.Domain;
using RoostCare.Models.Services;
using LibraryManagement.Model.Infrastracture.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Linq;
using RoostCare.Hubs;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();

builder.Services.AddDbContext<ApplicationDbContext>(options =>
{
    var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
    options.UseSqlServer(connectionString);
});

builder.Services.Configure<SecurityStampValidatorOptions>(options =>
{
    options.ValidationInterval = TimeSpan.FromSeconds(1);
});

builder.Services.AddDefaultIdentity<ApplicationUser>(options =>
{
    options.SignIn.RequireConfirmedAccount = false;
})
    .AddRoles<IdentityRole>()
    .AddEntityFrameworkStores<ApplicationDbContext>();

builder.Services.AddScoped(typeof(IBaseRepository<>), typeof(BaseRepository<>));
builder.Services.AddScoped<IRoosterRepository, RoosterRepository>();
builder.Services.AddScoped<FileUploader>();
builder.Services.AddAutoMapper(typeof(ModelMapper));
builder.Services.AddScoped<QRCode_Generator>();
builder.Services.AddSignalR();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

using (var scope = app.Services.CreateScope())
{
    var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();
    var accRepo = scope.ServiceProvider.GetRequiredService<IBaseRepository<ApplicationUser>>();
    var userManager = scope.ServiceProvider.GetRequiredService<UserManager<ApplicationUser>>();
    var accounts = await accRepo.GetAll();

    var roles = new[] { "Admin", "Staff" };
    foreach (var role in roles)
    {
        if (!await roleManager.RoleExistsAsync(role))
        {
            await roleManager.CreateAsync(new IdentityRole(role));
        }
    }

    if (!accounts.Any())
    {
        var account = new ApplicationUser
        {
            EmailConfirmed = true,
            Email = "admin@gmail.com",
            UserName = "admin@gmail.com",
            NormalizedEmail = "ADMIN@GMAIL.COM",
            NormalizedUserName = "ADMIN@GMAIL.COM"
        };
        await userManager.CreateAsync(account, "@AdminAccount01");
        await userManager.AddToRoleAsync(account, "Admin");
    }
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();
app.UseAuthorization();
app.MapRazorPages();
app.MapHub<NotificationHub>("/notificationhub");

app.Run();
