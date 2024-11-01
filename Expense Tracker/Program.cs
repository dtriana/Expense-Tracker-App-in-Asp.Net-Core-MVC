using System.Configuration;
using Expense_Tracker.Models;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);
var syncFusionLicenseKey = builder.Configuration["SyncFusionLicenseKey"];
var dbConnectionString = builder.Configuration["DbConnectionString"];

if (string.IsNullOrEmpty(syncFusionLicenseKey) || string.IsNullOrEmpty(dbConnectionString))
    throw new ConfigurationErrorsException("Sync Fusion License Key or Connection String is missing");

// Add services to the container.
builder.Services.AddControllersWithViews();

//DI
builder.Services.AddDbContext<ApplicationDbContext>(options =>
options.UseSqlServer(dbConnectionString));

//Register Syncfusion license
Syncfusion.Licensing.SyncfusionLicenseProvider.RegisterLicense(syncFusionLicenseKey);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
}
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Dashboard}/{action=Index}/{id?}");

app.Run();
