using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using ottplatform.Models;
using ottplatform.Service;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

    
builder.Services.AddDbContext<AppDbContext>(
    options => options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"))
    
    );

builder.Services.AddSingleton<EmailSender>();

builder.Services.AddSession(

    Options =>
    {
        Options.IdleTimeout = TimeSpan.FromMinutes(10);
    }
    
    );



var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
}
app.UseStaticFiles();

app.UseRouting();

app.UseSession();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=OTTwebsite}/{action=Index}/{id?}");

app.Run();
