using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Entities.Models;


public class FoodItems
{
    [Key]
    public string? FoodCode { get; set; }
    public string? Name { get; set; }
    public string? Description { get; set; }
    public string? DataReferences { get; set; }
    public string? FoodGroupCode { get; set; }
    public FoodGroups? FoodGroup { get; set; }
    public Energy? Energy { get; set; }
    public Macronutrients? Macronutrients { get; set; }
    public Proximates? Proximates { get; set; }
    public Minerals? Minerals { get; set; }
    public Vitamins? Vitamins { get; set; }
}