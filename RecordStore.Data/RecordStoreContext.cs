using Microsoft.EntityFrameworkCore;
using RecordStore.Core.Models;

namespace RecordStore.Data
{
    public class RecordStoreContext : DbContext
    {
        public RecordStoreContext(DbContextOptions<RecordStoreContext> options)
            : base(options)
        {
        }

        public DbSet<Record> Records { get; set; }
        public DbSet<Artist> Artists { get; set; }
        public DbSet<Genre> Genres { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Record>()
                .HasOne(r => r.Artist)
                .WithMany(a => a.Records)
                .HasForeignKey(r => r.ArtistID);

            modelBuilder.Entity<Record>()
                .HasOne(r => r.Genre)
                .WithMany(g => g.Records)
                .HasForeignKey(r => r.GenreID);

            modelBuilder.Entity<Record>()
                .Property(r => r.Price)
                .HasColumnType("decimal(18,2)");

            // Seed some initial data
            modelBuilder.Entity<Genre>().HasData(
                new Genre { GenreID = 1, Name = "Rock" },
                new Genre { GenreID = 2, Name = "Jazz" },
                new Genre { GenreID = 3, Name = "Classical" },
                new Genre { GenreID = 4, Name = "Pop" },
                new Genre { GenreID = 5, Name = "Hip Hop" }
            );

            modelBuilder.Entity<Artist>().HasData(
                new Artist { ArtistID = 1, Name = "Pink Floyd" },
                new Artist { ArtistID = 2, Name = "Miles Davis" },
                new Artist { ArtistID = 3, Name = "Ludwig van Beethoven" }
            );

            modelBuilder.Entity<Record>().HasData(
                new Record
                {
                    RecordID = 1,
                    Title = "The Dark Side of the Moon",
                    ArtistID = 1,
                    GenreID = 1,
                    Year = 1973,
                    Price = 29.99M,
                    CoverImageUrl = "/images/dark-side-of-the-moon.jpg"
                },
                new Record
                {
                    RecordID = 2,
                    Title = "Kind of Blue",
                    ArtistID = 2,
                    GenreID = 2,
                    Year = 1959,
                    Price = 24.99M,
                    CoverImageUrl = "/images/kind-of-blue.jpg"
                },
                new Record
                {
                    RecordID = 3,
                    Title = "Symphony No. 9",
                    ArtistID = 3,
                    GenreID = 3,
                    Year = 1824,
                    Price = 19.99M,
                    CoverImageUrl = "/images/symphony-9.jpg"
                }
            );
        }
    }
} 