using Microsoft.EntityFrameworkCore;
using PingPong.API.Database.Models;

namespace PingPong.API.Database
{
    public class DataContext: DbContext
    {
        public DbSet<Player> Players { get; set; }
        public DbSet<Game> Games { get; set; }

        public DataContext(DbContextOptions options) : base(options)
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {
            
        }

        
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // modelBuilder.Entity<Game>()
            //     .HasOne(g => g.Player1Result)
            //     .WithOne(pr => pr.Game)
            //     .OnDelete(DeleteBehavior.NoAction);
            // modelBuilder.Entity<Game>()
            //     .HasOne(g => g.Player2Result)
            //     .WithOne(pr => pr.Game)
            //     .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<Game>(b =>
            {
                b.HasOne<PlayerResult>(g => g.Player1Result)
                    .WithMany()
                    .HasForeignKey(g => g.Player1ResultId)
                    .OnDelete(DeleteBehavior.NoAction)
                    .IsRequired();
            
                b.HasOne<PlayerResult>(g => g.Player2Result)
                    .WithMany()
                    .HasForeignKey(g => g.Player2ResultId)
                    .OnDelete(DeleteBehavior.NoAction)
                    .IsRequired();
            });
            
            // modelBuilder.Entity<Game>().OwnsOne(p => p.Player1Result, od =>
            // {
            //     od.ToTable("PlayerResult");
            // });
            // modelBuilder.Entity<Game>().OwnsOne(p => p.Player2Result, od =>
            // {
            //     od.ToTable("PlayerResult");
            // });
                
        }
    }
}