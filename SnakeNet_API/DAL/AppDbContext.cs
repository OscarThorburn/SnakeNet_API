using Microsoft.EntityFrameworkCore;
using SnakeNet_API.Models.Entities;

namespace SnakeNet_API.DAL
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        public DbSet<Snake> Snakes { get; set; }
        public DbSet<Enclosure> Enclosures { get; set; }
        public DbSet<GrowthRecord> GrowthRecords { get; set; }
        public DbSet<Elimination> Eliminations { get; set; }
        public DbSet<FeedingRecord> FeedingRecords { get; set; }
        public DbSet<EnclosureReading> EnclosureReadings { get; set; }
        public DbSet<EnclosureLight> EnclosureLights { get; set; }
        public DbSet<EnclosureSubstrate> EnclosureSubstrates { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Snake Entity Configuration
            modelBuilder.Entity<Snake>()
                .HasKey(s => s.Id);

            modelBuilder.Entity<Snake>()
                .HasOne(s => s.Enclosure)
                .WithOne(e => e.Snake)
                .HasForeignKey<Enclosure>(e => e.Id);

            // GrowthRecord Entity Configuration
            modelBuilder.Entity<GrowthRecord>()
                .HasKey(gr => gr.Id);

            modelBuilder.Entity<GrowthRecord>()
                .HasOne(gr => gr.Snake)
                .WithMany()
                .HasForeignKey("SnakeId");

            // FeedingRecord Entity Configuration
            modelBuilder.Entity<FeedingRecord>()
                .HasKey(fr => fr.Id);

            modelBuilder.Entity<FeedingRecord>()
                .HasOne(fr => fr.Snake)
                .WithMany()
                .HasForeignKey("SnakeId");

            // EnclosureReading Entity Configuration
            modelBuilder.Entity<EnclosureReading>()
                .HasKey(er => er.Id);

            modelBuilder.Entity<EnclosureReading>()
                .HasOne(er => er.Enclosure)
                .WithMany()
                .HasForeignKey("EnclosureId");

            // Enclosure Entity Configuration
            modelBuilder.Entity<Enclosure>()
                .HasKey(e => e.Id);

            modelBuilder.Entity<Enclosure>()
                .HasOne(e => e.Snake)
                .WithOne(s => s.Enclosure)
                .HasForeignKey<Snake>(s => s.Id);

            // Elimination Entity Configuration
            modelBuilder.Entity<Elimination>()
                .HasKey(e => e.Id);

            modelBuilder.Entity<Elimination>()
                .HasOne(e => e.Snake)
                .WithMany()
                .HasForeignKey("SnakeId");

            // EnclosureLight Entity Configuration
            modelBuilder.Entity<EnclosureLight>()
                .HasKey(el => el.Id);

            modelBuilder.Entity<EnclosureLight>()
                .HasOne(er => er.Enclosure)
                .WithMany()
                .HasForeignKey("EnclosureId");

            // EnclosureSubstrate Entity Configuration
            modelBuilder.Entity<EnclosureSubstrate>()
                .HasKey(es => es.Id);

            modelBuilder.Entity<EnclosureSubstrate>()
                .HasOne(es => es.Enclosure)
                .WithMany()
                .HasForeignKey("EnclosureId");
        }
    }
}
