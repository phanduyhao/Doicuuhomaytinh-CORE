using AspNetCoreHero.ToastNotification;
using AspNetCoreHero.ToastNotification.Notyf.Models;
using Doicuuhomaytinh_CORE.Models;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.EntityFrameworkCore;
using System.Text.Encodings.Web;
using System.Text.Unicode;

var builder = WebApplication.CreateBuilder(args);
// Add services to the container.
builder.Services.AddControllersWithViews().AddRazorRuntimeCompilation();
builder.Services.AddDbContext<QuanLyDCHMTContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("DbQuanLyDCHMT"));
});
builder.Services.AddSingleton<HtmlEncoder>(HtmlEncoder.Create(allowedRanges: new[] { UnicodeRanges.All }));
builder.Services.AddNotyf(config => { config.DurationInSeconds = 10; config.IsDismissable = true; config.Position = NotyfPosition.BottomRight; });
builder.Services.AddSession();
builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(p =>
    {
        p.ExpireTimeSpan = TimeSpan.FromSeconds(60);
        p.LoginPath = "/dang-nhap.html";
    });
var app = builder.Build();
// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseSession();
app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();
app.MapControllerRoute(
    name: "areas",
    pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}");
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();