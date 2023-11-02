using Microsoft.EntityFrameworkCore;

using LIDOM.Models;

namespace LIDOM.Databases
{
    public class LidomDBContext : DbContext
    {
        public DbSet<LidomTeam> LidomTeams { get; set; }

        public DbSet<Calendar> Calendars { get; set; }

        public DbSet<Stadistic> Stadistics { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\any_c\OneDrive\Documents\LIDOM.mdf;Integrated Security=True;Connect Timeout=30");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Stadistic>()
                .HasKey(s => new { s.Id_Calendar, s.Id_Team });

             modelBuilder.Entity<Calendar>()
                .HasOne(c => c.LidomSecondTeam)
                .WithMany()
                .HasForeignKey(c => c.Id_SecondTeam)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<Calendar>()
                .HasOne(c => c.LidomFirstTeam)
                .WithMany()
                .HasForeignKey(c => c.Id_FirstTeam)
                .OnDelete(DeleteBehavior.NoAction);
        }

        public override int SaveChanges()
        {
            var addedEntities = ChangeTracker.Entries()
                .Where(e => e.State == EntityState.Added)
                .ToList();

            addedEntities.ForEach(e =>
            {
                e.Property("CreatedDate").CurrentValue = DateTime.UtcNow;
                e.Property("UpdatedDate").CurrentValue = DateTime.UtcNow;
            });

            var editedEntities = ChangeTracker.Entries()
                .Where(e => e.State == EntityState.Modified)
                .ToList();

            editedEntities.ForEach(e =>
            {
                e.Property("UpdatedDate").CurrentValue = DateTime.UtcNow;
            });

            return base.SaveChanges();
        }

    }
}
