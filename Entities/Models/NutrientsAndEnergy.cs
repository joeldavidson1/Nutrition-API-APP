using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Entities.Models;

public class NutrientsAndEnergy
{
    [Key]
    public string NutrientId { get; set; }
    public string Name { get; set; }
    public string UnitSymbol { get; set; }
    public string CategoryId { get; set; }
    [ForeignKey("CategoryId")]
    public NutrientCategories? NutrientCategories { get; set; } 
    public virtual ICollection<NutrientValues> NutrientValues { get; set; }
}