using Microsoft.EntityFrameworkCore;
using Business.Entities;

namespace RecordStore.Data
{
    public class RecordStoreDbContext : DbContext
    {
        public RecordStoreDbContext(DbContextOptions<RecordStoreDbContext> options)
            : base(options) { }

        public DbSet<Artist> Artists { get; set; }
        public DbSet<Genre> Genres { get; set; }
        public DbSet<Record> Records { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderDetail> OrderDetails { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {   
            modelBuilder.Entity<Record>()
                .HasOne(r => r.Artist)
                .WithMany()
                .HasForeignKey(r => r.ArtistID);

            modelBuilder.Entity<Record>()
                .HasOne(r => r.Genre)
                .WithMany()
                .HasForeignKey(r => r.GenreID);

            modelBuilder.Entity<Order>()
                .HasOne(o => o.Customer)
                .WithMany()
                .HasForeignKey(o => o.CustomerID);

            modelBuilder.Entity<OrderDetail>()
                .HasOne(od => od.Order)
                .WithMany()
                .HasForeignKey(od => od.OrderID);

            modelBuilder.Entity<OrderDetail>()
                .HasOne(od => od.Record)
                .WithMany()
                .HasForeignKey(od => od.RecordID);
        }
    }
}