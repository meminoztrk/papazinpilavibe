﻿using Microsoft.EntityFrameworkCore;
using NLayer.Core;
using NLayer.Core.Models;
using NLayer.Repository.Confugirations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Markup;

namespace NLayer.Repository
{
    public class AppDbContext:DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options):base(options)
        {
            
        }
        
        public DbSet<User> Users { get; set; }
        public DbSet<Province> Provinces { get; set; }
        public DbSet<About> About { get; set; }
        public DbSet<Business> Business { get; set; }
        public DbSet<BusinessImage> BusinessImage { get; set; }
        public DbSet<BusinessFAQ> BusinessFAQ { get; set; }
        public DbSet<BusinessComment> BusinessComment { get; set; }
        public DbSet<BusinessSubComment> BusinessSubComment { get; set; }
        public DbSet<BusinessUserImage> BusinessUserImage { get; set; }
        public DbSet<FavoriteBusiness> FavoriteBusiness { get; set; }
        public DbSet<FavoriteComment> FavoriteComment { get; set; }
        public DbSet<Report> Report { get; set; }
        public DbSet<OwnerRequest> OwnerRequest { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly()); // Bütün Assembly Dosyalarını Çek
            //modelBuilder.ApplyConfiguration(new ProductConfiguration()); // Sadece Bir Tanesini Çek
           
            base.OnModelCreating(modelBuilder);
        }
        public override int SaveChanges()
        {
            var entries = ChangeTracker
                .Entries()
                .Where(e => e.Entity is BaseEntity && (
                        e.State == EntityState.Added
                        || e.State == EntityState.Modified));

            foreach (var entityEntry in entries)
            {
                ((BaseEntity)entityEntry.Entity).UpdatedDate = DateTime.Now;

                if (entityEntry.State == EntityState.Added)
                {
                    ((BaseEntity)entityEntry.Entity).IsActive = true;
                    ((BaseEntity)entityEntry.Entity).IsDeleted = false;
                    ((BaseEntity)entityEntry.Entity).CreatedDate = DateTime.Now;
                }
            }

           

            return base.SaveChanges();
        }

        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            var entries = ChangeTracker
                .Entries()
                .Where(e => e.Entity is BaseEntity && (
                        e.State == EntityState.Added
                        || e.State == EntityState.Modified));

            foreach (var entityEntry in entries)
            {
                ((BaseEntity)entityEntry.Entity).UpdatedDate = DateTime.Now;

                if (entityEntry.State == EntityState.Added)
                {
                    ((BaseEntity)entityEntry.Entity).IsActive = true;
                    ((BaseEntity)entityEntry.Entity).IsDeleted = false;
                    ((BaseEntity)entityEntry.Entity).CreatedDate = DateTime.Now;
                }
            }



            return base.SaveChangesAsync();
        }
    }
}
