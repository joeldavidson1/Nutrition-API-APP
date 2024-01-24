using System.Reflection;
using AutoMapper;
using Entities.Models;
using Shared;
using Shared.DataTransferObjects;

namespace NutritionAPI;

public class MappingProfile : Profile
{ 
    public MappingProfile()
    {

        CreateMap<FoodItems, FoodItemsDto>()
            .ForMember(dest => dest.Energy, opt => opt.MapFrom(src => MapEnergy(src.NutrientValues)))
            .ForMember(dest => dest.Macronutrients, opt => opt.MapFrom(src => MapMacronutrients(src.NutrientValues)))
            .ForMember(dest => dest.Proximates, opt => opt.MapFrom(src => MapProximates(src.NutrientValues)))
            .ForMember(dest => dest.Vitamins, opt => opt.MapFrom(src => MapVitamins(src.NutrientValues)))
            .ForMember(dest => dest.Minerals, opt => opt.MapFrom(src => MapMinerals(src.NutrientValues)));
    }

    private EnergyDto? MapEnergy(ICollection<NutrientValues> nutrientValues)
    {
        string? kcal = MapNutrient(nutrientValues, "energy_kcal");
        string? kj = MapNutrient(nutrientValues, "energy_kj");

        return new EnergyDto(kcal, kj);
    }
    
    private MacronutrientsDto? MapMacronutrients(ICollection<NutrientValues> nutrientValues)
    {
        string? protein = MapNutrient(nutrientValues, "protein");
        string? fat = MapNutrient(nutrientValues, "fat");
        string? carbohydrate = MapNutrient(nutrientValues, "carbohydrate");
        
        return new MacronutrientsDto(protein, fat, carbohydrate);
    }
    
    private ProximatesDto? MapProximates(ICollection<NutrientValues> nutrientValues)
    {
        string? water = MapNutrient(nutrientValues, "water");
        string? starch = MapNutrient(nutrientValues, "fat");
        string? totalSugars = MapNutrient(nutrientValues, "total_sugars");
        string? glucose = MapNutrient(nutrientValues, "glucose");
        string? galactose = MapNutrient(nutrientValues, "galactose");
        string? fructose = MapNutrient(nutrientValues, "fructose");
        string? sucrose = MapNutrient(nutrientValues, "sucrose");
        string? maltose = MapNutrient(nutrientValues, "maltose");
        string? lactose = MapNutrient(nutrientValues, "lactose");
        string? alcohol = MapNutrient(nutrientValues, "alcohol");
        string? nonStarchPolysaccharides = MapNutrient(nutrientValues, "non_starch_polysaccharides");
        string? fibre = MapNutrient(nutrientValues, "fibre_aoac");
        string? fatsSaturated = MapNutrient(nutrientValues, "fats_saturated");
        string? fatsMonounsaturated = MapNutrient(nutrientValues, "fats_monounsaturated");
        string? fatsPolyunsaturated = MapNutrient(nutrientValues, "fats_polyunsaturated");
        string? fatsTrans = MapNutrient(nutrientValues, "fats_trans");
        string? cholesterol = MapNutrient(nutrientValues, "cholesterol");
    
        return new ProximatesDto(water, starch, totalSugars, glucose, galactose, fructose, sucrose, maltose,
            lactose, alcohol, nonStarchPolysaccharides, fibre, fatsSaturated, fatsMonounsaturated,
            fatsPolyunsaturated, fatsTrans, cholesterol);
    }
    
    private VitaminsDto? MapVitamins(ICollection<NutrientValues> nutrientValues)
    {
        string? retinol = MapNutrient(nutrientValues, "retinol");
        string? thiamin = MapNutrient(nutrientValues, "thiamin");
        string? riboflavin = MapNutrient(nutrientValues, "riboflavin");
        string? niacin = MapNutrient(nutrientValues, "niacin");
        string? pantothenate = MapNutrient(nutrientValues, "pantothenate");
        string? biotin = MapNutrient(nutrientValues, "biotin");
        string? folate = MapNutrient(nutrientValues, "folate");
        string? vitaminB12 = MapNutrient(nutrientValues, "vitamin_b12");
        string? vitaminB6 = MapNutrient(nutrientValues, "vitamin_b6");
        string? vitaminC = MapNutrient(nutrientValues, "vitamin_c");
        string? vitaminD = MapNutrient(nutrientValues, "vitamin_d");
        string? vitaminE = MapNutrient(nutrientValues, "vitamin_e");
        string? vitaminK1 = MapNutrient(nutrientValues, "vitammin_k1");
        string? carotene = MapNutrient(nutrientValues, "carotene");
        string? trytophan = MapNutrient(nutrientValues, "trytophan");
    
        return new VitaminsDto(retinol, thiamin, riboflavin, niacin, pantothenate, biotin, folate, vitaminB12,
            vitaminB6, vitaminC, vitaminD, vitaminE, vitaminK1, carotene, trytophan);
    }
    
    private MineralsDto? MapMinerals(ICollection<NutrientValues> nutrientValues)
    {
        string? sodium = MapNutrient(nutrientValues, "sodium");
        string? potassium = MapNutrient(nutrientValues, "potassium");
        string? calcium = MapNutrient(nutrientValues, "calcium");
        string? magnesium = MapNutrient(nutrientValues, "magnesium");
        string? phosphorus = MapNutrient(nutrientValues, "phosphorus");
        string? iron = MapNutrient(nutrientValues, "iron");
        string? copper = MapNutrient(nutrientValues, "copper");
        string? zinc = MapNutrient(nutrientValues, "zinc");
        string? chloride = MapNutrient(nutrientValues, "chloride");
        string? manganese = MapNutrient(nutrientValues, "manganese");
        string? selenium = MapNutrient(nutrientValues, "selenium");
        string? iodine = MapNutrient(nutrientValues, "iodine");
    
    
        return new MineralsDto(sodium, potassium, calcium, magnesium, phosphorus, iron, copper, zinc, chloride,
            manganese, selenium, iodine);
    }

    private string? MapNutrient(ICollection<NutrientValues> nutrientValues, string nutrientName)
    {
        foreach (NutrientValues nutrientValue in nutrientValues)
        {
            string? nutrientValueName = nutrientValue.NutrientsAndEnergy?.Name?.ToLower();
            if (nutrientValueName == nutrientName.ToLower())
            {
                return nutrientValue.Value;
            }
        }

        // If no matching nutrient is found, return an instance with empty values
        return "";
    }
   
    // private string FormatNutrient(NutrientUnitAndValueDto nutrient)
    // {
    //     if (nutrient.Value is "Tr" or "N/A" or "N")
    //     {
    //         return nutrient.Value;
    //     }
    //     
    //     return $"{nutrient.Value}{nutrient.UnitSymbol}";
    // }
}
