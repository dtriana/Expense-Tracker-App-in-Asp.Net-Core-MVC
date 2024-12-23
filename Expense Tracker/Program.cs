using System.Configuration;
using Expense_Tracker.Models;

var builder = WebApplication.CreateBuilder(args);

builder.AddServiceDefaults();
var syncFusionLicenseKey = builder.Configuration["SyncFusionLicenseKey"];

if (string.IsNullOrEmpty(syncFusionLicenseKey))
    throw new Exception("Sync Fusion License Key is missing");

// Add services to the container.
builder.Services.AddControllersWithViews();

//DI
builder.AddSqlServerDbContext<ApplicationDbContext>("sqldata");

//Register Syncfusion license
Syncfusion.Licensing.SyncfusionLicenseProvider.RegisterLicense(syncFusionLicenseKey);

var app = builder.Build();

app.MapDefaultEndpoints();

if (app.Environment.IsDevelopment())
{
    using (var scope = app.Services.CreateScope())
    {
        var context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
        context.Database.EnsureCreated();
    }
}
else
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    app.UseHsts();
}

app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Dashboard}/{action=Index}/{id?}");

app.Run();
