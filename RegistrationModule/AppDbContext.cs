using Microsoft.EntityFrameworkCore;
using RegistrationModule.Models;

namespace RegistrationModule
{
    public class AppDbContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Hash> Hashes { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server=localhost;Database=labdb; User Id=sa; Password=myStrongPassword123; TrustServerCertificate=True");
        }
    }
}
