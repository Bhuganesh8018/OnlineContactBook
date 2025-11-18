using Microsoft.EntityFrameworkCore;

var app = builder.Build();

// 🔥 Auto-create database + tables on startup
using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();
    db.Database.Migrate();
}

app.Run();
