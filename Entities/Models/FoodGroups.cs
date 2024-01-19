using System.ComponentModel.DataAnnotations;

namespace Entities.Models;

public class FoodGroups
{
    [Key]
    public string? FoodGroupCode { get; set; }
    public string? Description { get; set; }
    public ICollection<FoodItems>? FoodItems { get; set; }
}