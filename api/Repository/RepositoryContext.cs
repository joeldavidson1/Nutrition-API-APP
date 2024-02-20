using Entities.Configuration;
using Entities.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;

namespace Repository;

public class RepositoryContext : IdentityDbContext<User>
{
    public RepositoryContext(DbContextOptions options) : base(options)
    {
        
    }
    //
    // public DbSet<FoodItems>? FoodItems { get; set; }
    // public DbSet<FoodGroups>? FoodGroups { get; set; }
    // public DbSet<Macronutrients>? Macronutrients{ get; set; }
    // public DbSet<Proximates>? Proximates { get; set; }
    // public DbSet<Minerals>? Minerals { get; set; }
    // public DbSet<Vitamins>? Vitamins { get; set; }
    // public DbSet<Energy>? Energy { get; set; }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<FoodItems>(entity =>
        {
            entity.HasOne(foodItem => foodItem.FoodGroup)
                .WithMany(foodGroup => foodGroup.FoodItemsCollection)
                .HasForeignKey(foodItem => foodItem.FoodGroupCode);
        
            entity.HasOne(m => m.Energy)
                .WithOne(foodItem => foodItem.FoodItems)
                .HasForeignKey<Energy>(x => x.FoodCode);
            
            entity.HasOne(m => m.Macronutrients)
                .WithOne(foodItem => foodItem.FoodItems)
                .HasForeignKey<Macronutrients>(x => x.FoodCode);
            
            entity.HasOne(m => m.Proximates)
                .WithOne(foodItem => foodItem.FoodItems)
                .HasForeignKey<Proximates>(x => x.FoodCode);
        
            entity.HasOne(m => m.Minerals)
                .WithOne(foodItem => foodItem.FoodItems)
                .HasForeignKey<Minerals>(x => x.FoodCode);
            
            entity.HasOne(v => v.Vitamins)
                .WithOne(foodItem => foodItem.FoodItems)
                .HasForeignKey<Vitamins>(x => x.FoodCode);
        });
        
        base.OnModelCreating(modelBuilder);

        modelBuilder.ApplyConfiguration(new RoleConfiguration());
    }
}