using Microsoft.EntityFrameworkCore;
using NutritionAPI.Models;

namespace NutritionAPI.Data;

public class DataContext : DbContext
{
    public DataContext(DbContextOptions<DataContext> options) : base(options)
    {
    }

    public DbSet<FoodItems> FoodItems { get; set; }
    public DbSet<FoodGroup> FoodGroups { get; set; }
    public DbSet<MacronutrientsAndEnergy> MacronutrientsAndEnergy { get; set; }
    public DbSet<Minerals> Minerals { get; set; }
    public DbSet<Vitamins> Vitamins { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        // modelBuilder.Entity<FoodItem>().ToTable("fooditems"); // Ensure correct table name

        modelBuilder.Entity<FoodItems>(entity =>
        {
            entity.HasOne(foodItem => foodItem.FoodGroup)
                .WithMany(foodGroup => foodGroup.FoodItems)
                .HasForeignKey(foodItem => foodItem.FoodGroupCode);
        
            entity.HasOne(m => m.MacronutrientsAndEnergy)
                .WithOne(foodItem => foodItem.FoodItem)
                .HasForeignKey<MacronutrientsAndEnergy>(x => x.FoodCode);
            
            entity.HasOne(m => m.Proximates)
                .WithOne(foodItem => foodItem.FoodItem)
                .HasForeignKey<Proximates>(x => x.FoodCode);

            entity.HasOne(m => m.Minerals)
                .WithOne(foodItem => foodItem.FoodItem)
                .HasForeignKey<Minerals>(x => x.FoodCode);
            
            entity.HasOne(v => v.Vitamins)
                .WithOne(foodItem => foodItem.FoodItem)
                .HasForeignKey<Vitamins>(x => x.FoodCode);
        });
    }
}