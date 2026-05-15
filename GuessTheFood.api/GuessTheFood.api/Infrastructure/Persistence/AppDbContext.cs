using GuessTheFood.api.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace GuessTheFood.Api.Infrastructure.Persistence;

public class AppDbContext : DbContext
{
    public AppDbContext(
        DbContextOptions<AppDbContext> options)
        : base(options)
    {
    }

    public DbSet<User> Users => Set<User>();
    public DbSet<Role> Roles => Set<Role>();
    public DbSet<Dish> Dishes => Set<Dish>();
    public DbSet<Ingredient> Ingredients => Set<Ingredient>();
    public DbSet<DishIngredient> DishIngredients => Set<DishIngredient>();
    public DbSet<Country> Countries => Set<Country>();

    protected override void OnModelCreating(
        ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Country>()
            .Property(c => c.Name)
            .HasMaxLength(50)
            .IsRequired();

        modelBuilder.Entity<Country>()
            .HasIndex(c => c.Name)
            .IsUnique();

        modelBuilder.Entity<Ingredient>()
            .Property(i => i.Name)
            .HasMaxLength(50)
            .IsRequired();

        modelBuilder.Entity<Ingredient>()
            .Property(i => i.Type)
            .HasConversion<string>()
            .HasMaxLength(30)
            .IsRequired();

        modelBuilder.Entity<Ingredient>()
            .HasIndex(i => i.Name)
            .IsUnique();

        modelBuilder.Entity<User>()
            .HasOne(u => u.Role)
            .WithMany()
            .HasForeignKey(u => u.RoleId);

        modelBuilder.Entity<Dish>()
            .HasOne(d => d.Country)
            .WithMany()
            .HasForeignKey(d => d.CountryId);

        modelBuilder.Entity<DishIngredient>()
            .HasKey(di => new
            {
                di.DishId,
                di.IngredientId
            });

        modelBuilder.Entity<DishIngredient>()
            .HasOne(di => di.Dish)
            .WithMany(d => d.Ingredients)
            .HasForeignKey(di => di.DishId);

        modelBuilder.Entity<DishIngredient>()
            .HasOne(di => di.Ingredient)
            .WithMany()
            .HasForeignKey(di => di.IngredientId);
    }
}