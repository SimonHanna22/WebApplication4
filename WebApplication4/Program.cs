using Microsoft.EntityFrameworkCore;
using WebApplication4.Data;
using Microsoft.AspNetCore.Identity;
using WebApplication4.Utility;
using Microsoft.AspNetCore.Identity.UI.Services;


var builder = WebApplication.CreateBuilder(args);
builder.Services.AddRazorPages();
builder.Services.ConfigureApplicationCookie(options =>
{
	options.LoginPath = "/Identity/Account/Login";
	options.AccessDeniedPath = "/Identity/Account/AccessDenied";
});
// Add services to the container.
builder.Services.AddControllersWithViews();
        builder.Services.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

        builder.Services.AddIdentity<IdentityUser,IdentityRole>(options => options.SignIn.RequireConfirmedAccount = false).AddEntityFrameworkStores<ApplicationDbContext>().AddDefaultTokenProviders();
builder.Services.AddScoped<IEmailSender, EmailService>();
        var app = builder.Build();
//builder.Services.AddAutoMapper(typeof(MappingConf));

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
        {
            app.UseExceptionHandler("/Home/Error");
            // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
            app.UseHsts();
        }

        app.UseHttpsRedirection();
        app.UseStaticFiles();

        app.UseRouting();

        app.UseAuthorization();

        app.MapControllerRoute(
            name: "default",
            pattern: "{controller=Home}/{action=Index}/{id?}");
app.MapRazorPages();

        app.Run();
