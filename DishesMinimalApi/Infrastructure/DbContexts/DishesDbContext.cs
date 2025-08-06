using DishesAPI.Entities;
using DishesMinimalApi.Infrastructure.Entities;
using Microsoft.EntityFrameworkCore;

namespace DishesAPI.DbContexts;

public class DishesDbContext : DbContext
{
    public DbSet<Dish> Dishes { get; set; } = null!;
    public DbSet<Ingredient> Ingredients { get; set; } = null!;
    public DbSet<DishIngredient> DishesIngredients { get; set; } = null!;

    public DishesDbContext(DbContextOptions<DishesDbContext> options)
        : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<DishIngredient>(entity =>
        {
            entity.HasKey(di => new { di.DishId, di.IngredientId });

            entity
                .HasOne(di => di.Dish)
                .WithMany(d => d.DishIngredients)
                .HasForeignKey(di => di.DishId);

            entity
                .HasOne(di => di.Ingredient)
                .WithMany(i => i.DishIngredients)
                .HasForeignKey(di => di.IngredientId);
        });

        base.OnModelCreating(modelBuilder);
    }
}
