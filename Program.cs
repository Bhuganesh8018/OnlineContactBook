using Microsoft.EntityFrameworkCore;
using OnlineContactBook.Data;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection")));

var app = builder.Build();

// ====== AUTO MIGRATION FOR RENDER ======
using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();
    db.Database.Migrate();   // Creates DB + tables automatically
}
// =======================================

// Render does not support HTTPS redirection properly
// so disable it to avoid errors.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Contacts/Error");
    // app.UseHsts();   // Don't use HTTPS HSTS on Render
}

// REMOVE this line for Render (important!)
// app.UseHttpsRedirection();

app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Contacts}/{action=Index}/{id?}");

app.Run();
