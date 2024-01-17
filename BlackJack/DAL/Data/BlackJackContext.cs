using DAL.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Data
{
    using Microsoft.EntityFrameworkCore;

    public class BlackjackDbContext : DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Data Source=localhost;Initial Catalog=Blackjack;Integrated Security=True;Encrypt=False");
        }



        public DbSet<Player> Players { get; set; }
        public DbSet<Game> Games { get; set; }
        public DbSet<Result> Results { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Result>()
                .HasKey(r => new { r.PlayerId, r.GameId });

            modelBuilder.Entity<Result>()
                .HasOne(r => r.Player)
                .WithMany(p => p.Results)
                .HasForeignKey(r => r.PlayerId)
                .OnDelete(DeleteBehavior.Cascade); // Added this line for cascading delete

            modelBuilder.Entity<Result>()
                .HasOne(r => r.Game)
                .WithMany(g => g.Results)
                .HasForeignKey(r => r.GameId)
                .OnDelete(DeleteBehavior.Cascade); // Added this line for cascading delete
        }



    }

}
