using System.ComponentModel.DataAnnotations;

namespace Entities.Models;

public class NutrientCategories
{
    [Key]
    public string? CategoryId { get; set; }
    public string? CategoryName { get; set; }
    public ICollection<NutrientsAndEnergy>? NutrientsAndEnergy { get; set; }
}