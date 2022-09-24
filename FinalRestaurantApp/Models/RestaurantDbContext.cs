using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

#nullable disable

namespace FinalRestaurantApp.Models
{
    public partial class RestaurantDbContext : DbContext
    {
        public RestaurantDbContext()
        {
        }

        public RestaurantDbContext(DbContextOptions<RestaurantDbContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Customer> Customers { get; set; }
        public virtual DbSet<Order> Orders { get; set; }
        public virtual DbSet<Restaurant> Restaurants { get; set; }
        public virtual DbSet<RestaurantMenu> RestaurantMenus { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("Server=;Database=RestaurantDb;Trusted_Connection=True;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "SQL_Latin1_General_CP1_CI_AS");

            modelBuilder.Entity<Order>(entity =>
            {
                entity.ToTable("orders");

                entity.HasIndex(e => e.CustomerId, "IX_orders_CustomerId");

                entity.HasIndex(e => e.RestaurantMenuId, "IX_orders_RestaurantMenuId");

                entity.HasOne(d => d.Customer)
                    .WithMany(p => p.Orders)
                    .HasForeignKey(d => d.CustomerId);

                entity.HasOne(d => d.RestaurantMenu)
                    .WithMany(p => p.Orders)
                    .HasForeignKey(d => d.RestaurantMenuId);
            });

            modelBuilder.Entity<RestaurantMenu>(entity =>
            {
                entity.HasIndex(e => e.RestaurantId, "IX_RestaurantMenus_RestaurantId");

                entity.HasOne(d => d.Restaurant)
                    .WithMany(p => p.RestaurantMenus)
                    .HasForeignKey(d => d.RestaurantId);
            });

            OnModelCreatingPartial(modelBuilder);

            modelBuilder.Entity<Restaurant>()
               .HasQueryFilter(x => !x.Archived);
            modelBuilder.Entity<RestaurantMenu>()
                        .HasQueryFilter(x => !x.Archived);
            modelBuilder.Entity<Customer>()
                      .HasQueryFilter(x => !x.Archived);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
