using Microsoft.EntityFrameworkCore;
using Core.Entity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace Infrastructure.Data
{
    public class TaskDbContext : IdentityDbContext<UserEntity, UserRole, int>
    {
        public DbSet<PoolTable> PoolTables { get; set; }
        public DbSet<Reservation> Reservations { get; set; }
        public DbSet<Payment> Payments { get; set; }

        public TaskDbContext(DbContextOptions<TaskDbContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            // Konfiguracja dodatkowych właściwości, relacji itp.
        }
    }
}
