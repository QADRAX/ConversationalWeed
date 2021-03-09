using Microsoft.EntityFrameworkCore;

namespace ConversationalWeed.DB.Models
{
    public partial class WeedLeaderboardContext : DbContext
    {
        public WeedLeaderboardContext() { }

        public WeedLeaderboardContext(DbContextOptions<WeedLeaderboardContext> options) : base(options) { }

        public DbSet<Match> Match { get; set; }

        public DbSet<PlayerMatch> PlayerMatch { get; set; }

        public DbSet<Player> Player { get; set; }

        public DbSet<PlayerSkin> PlayerSkin { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Match>(entity =>
            {
                entity.HasOne(e => e.Winner)
                    .WithMany(p => p.WinMatches)
                    .HasForeignKey(m => m.WinnerId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Match_WinnerPlayer");
            });

            modelBuilder.Entity<PlayerMatch>(entity =>
            {
                entity.HasOne(e => e.Player)
                    .WithMany(p => p.PlayerMatches)
                    .HasForeignKey(pm => pm.PlayerId)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("FK_PlayerMatch_Player");

                entity.HasOne(e => e.Match)
                    .WithMany(p => p.PlayerMatches)
                    .HasForeignKey(pm => pm.MatchId)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("FK_PlayerMatch_Match");
            });

            modelBuilder.Entity<PlayerSkin>(entity =>
            {
                entity.HasOne(e => e.Player)
                    .WithMany(p => p.PlayerSkins)
                    .HasForeignKey(e => e.PlayerId)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("FK_PlayerSkin_Player");
            });
        }

        //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        //{
        //    optionsBuilder.UseMySql("");
        //}
    }
}
