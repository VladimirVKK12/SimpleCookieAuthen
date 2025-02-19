using JStraining.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client.Extensibility;

namespace JStraining.DbConnection
{
    public class AppDbContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<UserList> userLists { get; set; }
        public AppDbContext(DbContextOptions<AppDbContext> op):base(op) { }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Data Source=DESKTOP-H0K0LN7;Database=JsTraining;Trusted_Connection=True;TrustServerCertificate=True");
        }
    }
}
