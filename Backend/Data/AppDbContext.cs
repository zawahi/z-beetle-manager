using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using KuwagataApp.Backend.Models;

namespace KuwagataApp.Backend.Data
{
    public class AppDbContext : IdentityDbContext<ApplicationUser>
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<Beetle> Beetles { get; set; } = null!;
        public DbSet<BottleRecord> BottleRecords { get; set; } = null!;
        public DbSet<BreedingRecord> BreedingRecords { get; set; } = null!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
                
            modelBuilder.Entity<Beetle>()
                .HasOne(b => b.Sire)
                .WithMany()
                .HasForeignKey(b => b.SireId)
                .OnDelete(DeleteBehavior.SetNull);
                
            modelBuilder.Entity<Beetle>()
                .HasOne(b => b.Dam)
                .WithMany()
                .HasForeignKey(b => b.DamId)
                .OnDelete(DeleteBehavior.SetNull);
        }
    }
}
