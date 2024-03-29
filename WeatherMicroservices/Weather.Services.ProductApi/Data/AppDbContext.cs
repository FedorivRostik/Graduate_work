﻿using Microsoft.EntityFrameworkCore;
using System.Reflection;
using Weather.Services.ProductApi.Models;

namespace Weather.Services.ProductApi.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

    public DbSet<Product> Products { get; set; }
    public DbSet<Genre> Genres { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

        var genre = new Genre { GenreId = Guid.Parse("1de31dd1-5f13-4b54-a6e2-6ed196c1026f"), Name = "Drinks", };
        modelBuilder.Entity<Genre>().HasData(genre);

        modelBuilder.Entity<Product>().HasData(
            new Product { ProductId = Guid.Parse("1ae31dd1-5f13-4b54-a6e2-6ed196c1026f"), Name = "Cola", Slug = "cola", Price = 0.89m, Amount = 100, GenreId = genre.GenreId },
            new Product { ProductId = Guid.Parse("2ae31dd1-5f13-4b54-a6e2-6ed196c1026f"), Name = "Pepsi", Slug = "pepsi", Price = 0.49m, Amount = 110, GenreId = genre.GenreId },
            new Product { ProductId = Guid.Parse("3ae31dd1-5f13-4b54-a6e2-6ed196c1026f"), Name = "Fanta", Slug = "fanta", Price = 0.59m, Amount = 120, GenreId = genre.GenreId }
            );
    }
}
