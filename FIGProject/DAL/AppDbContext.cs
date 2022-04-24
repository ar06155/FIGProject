using FIGProject.Models;
using Microsoft.EntityFrameworkCore;

namespace FIGProject.DAL
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {

        }
        public DbSet<Player> Players { get; set; }
        public DbSet<Team> Teams { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //Property Configurations
            modelBuilder.Entity<Team>()
                    .HasIndex(t => new { t.Name, t.Location })
                    .IsUnique();       //a team has to have unique combination of Name and Location

            //modelBuilder.Entity<Player>()
            //        .HasOne(p => p.Team)
            //        .WithMany(t => t.Players)
            //        .HasForeignKey("TeamId");
        }



    }

    
}
