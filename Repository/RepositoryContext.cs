using Entities.Models;
using Microsoft.EntityFrameworkCore;

namespace Repository;

public class RepositoryContext : DbContext
{
    public RepositoryContext(DbContextOptions options) : base(options)
    {
        
    }
    //
    // public DbSet<FoodItems>? FoodItems { get; set; }
    // public DbSet<FoodGroups>? FoodGroups { get; set; }
    // public DbSet<MacronutrientsAndEnergy>? MacronutrientsAndEnergy { get; set; }
    // public DbSet<Proximates>? Proximates { get; set; }
    // public DbSet<Minerals>? Minerals { get; set; }
    // public DbSet<Vitamins>? Vitamins { get; set; }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // modelBuilder.Entity<FoodItems>(entity =>
        // {
        //     entity.HasOne(foodItem => foodItem.FoodGroup)
        //         .WithMany(foodGroup => foodGroup.FoodItems)
        //         .HasForeignKey(foodItem => foodItem.FoodGroupCode);
        //
        //     entity.HasOne(m => m.MacronutrientsAndEnergy)
        //         .WithOne(foodItem => foodItem.FoodItem)
        //         .HasForeignKey<MacronutrientsAndEnergy>(x => x.FoodCode);
        //     
        //     entity.HasOne(m => m.Proximates)
        //         .WithOne(foodItem => foodItem.FoodItem)
        //         .HasForeignKey<Proximates>(x => x.FoodCode);
        //
        //     entity.HasOne(m => m.Minerals)
        //         .WithOne(foodItem => foodItem.FoodItem)
        //         .HasForeignKey<Minerals>(x => x.FoodCode);
        //     
        //     entity.HasOne(v => v.Vitamins)
        //         .WithOne(foodItem => foodItem.FoodItem)
        //         .HasForeignKey<Vitamins>(x => x.FoodCode);
        // });
        
        modelBuilder.Entity<FoodItems>(entity =>
        {
            entity.HasOne(foodItem => foodItem.FoodGroup)
                .WithMany(foodGroup => foodGroup.FoodItems)
                .HasForeignKey(foodItem => foodItem.FoodGroupCode);
        });

        modelBuilder.Entity<NutrientsAndEnergy>(entity =>
        {
            entity.HasOne(nutrientsAndEnergy => nutrientsAndEnergy.NutrientCategories)
                .WithMany(nutrientCategories => nutrientCategories.NutrientsAndEnergy)
                .HasForeignKey(nutrientsAndEnergy => nutrientsAndEnergy.CategoryId);
        });

        modelBuilder.Entity<NutrientValues>(entity =>
        {
            entity.HasOne(nutrientValues => nutrientValues.FoodItems)
                .WithMany(foodItems => foodItems.NutrientValues)
                .HasForeignKey(nutrientValues => nutrientValues.FoodCode);
            
            entity.HasOne(nutrientValues => nutrientValues.NutrientsAndEnergy)
                .WithMany(nutrientsAndEnergy => nutrientsAndEnergy.NutrientValues)
                .HasForeignKey(nutrientValues => nutrientValues.NutrientId);
        });

    }
}