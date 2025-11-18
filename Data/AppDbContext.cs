using Microsoft.EntityFrameworkCore;
using OnlineContactBook.Models;

namespace OnlineContactBook.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        public DbSet<Contact> Contacts { get; set; } = default!;
    }
}
