using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wardrobe.DAL.Models;

namespace Wardrobe.DAL.Data
{
    public class WardrobeContext : DbContext
    {
        public WardrobeContext() // Empty constructor for scaffolding
        {
                
        }

        public WardrobeContext(DbContextOptions<WardrobeContext> options) : base(options)
        {
                
        }

        public DbSet<Garment> Garments { get; set; }
        public DbSet<GarmentType> GarmentTypes { get; set; }
        public DbSet<GlobalBrand> GlobalBrands { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<UserBrand> UserBrands { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Table mappings
            modelBuilder.Entity<Garment>().ToTable("Garment");
            modelBuilder.Entity<GarmentType>().ToTable("GarmentType");
            modelBuilder.Entity<GlobalBrand>().ToTable("GlobalBrand");
            modelBuilder.Entity<User>().ToTable("User");
            modelBuilder.Entity<UserBrand>().ToTable("UserBrand");

            // Unique constraints
            modelBuilder.Entity<Garment>()
                .HasIndex(g => g.Name)
                .IsUnique();
            modelBuilder.Entity<GarmentType>()
                .HasIndex(g => g.Name)
                .IsUnique();
            modelBuilder.Entity<GlobalBrand>()
                .HasIndex(g => g.Name)
                .IsUnique();
            modelBuilder.Entity<User>()
                .HasIndex(u => u.Username)
                .IsUnique();
            modelBuilder.Entity<UserBrand>()
                .HasIndex(u => u.Name)
                .IsUnique();

            base.OnModelCreating(modelBuilder);
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("Server=wardrobe.database,5432;Database=wardrobe;Username=postgres;Password=postgres;Include Error Detail=true");
            }
        }
    }
}
