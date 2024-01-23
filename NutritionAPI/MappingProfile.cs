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
        
        CreateMap<NutrientValues, NutrientUnitAndValueDto>()
            .ForMember(dest => dest.UnitSymbol, opt => opt.MapFrom(src => src.NutrientsAndEnergy.UnitSymbol))
            .ForMember(dest => dest.Value, opt => opt.MapFrom(src => src.Value));
    }

    private EnergyDto? MapEnergy(ICollection<NutrientValues> nutrientValues)
    {
        string kcal = FormatNutrient(MapNutrient(nutrientValues, "energy_kcal"));
        string kj = FormatNutrient(MapNutrient(nutrientValues, "energy_kj"));

        return new EnergyDto(kcal, kj);
    }
    
    private MacronutrientsDto? MapMacronutrients(ICollection<NutrientValues> nutrientValues)
    {
        string protein = FormatNutrient(MapNutrient(nutrientValues, "protein"));
        string fat = FormatNutrient(MapNutrient(nutrientValues, "fat"));
        string carbohydrate = FormatNutrient(MapNutrient(nutrientValues, "carbohydrate"));
        
        return new MacronutrientsDto(protein, fat, carbohydrate);
    }

    private ProximatesDto? MapProximates(ICollection<NutrientValues> nutrientValues)
    {
        string water = FormatNutrient(MapNutrient(nutrientValues, "water"));
        string starch = FormatNutrient(MapNutrient(nutrientValues, "fat"));
        string totalSugars = FormatNutrient(MapNutrient(nutrientValues, "total_sugars"));
        string glucose = FormatNutrient(MapNutrient(nutrientValues, "glucose"));
        string galactose = FormatNutrient(MapNutrient(nutrientValues, "galactose"));
        string fructose = FormatNutrient(MapNutrient(nutrientValues, "fructose"));
        string sucrose = FormatNutrient(MapNutrient(nutrientValues, "sucrose"));
        string maltose = FormatNutrient(MapNutrient(nutrientValues, "maltose"));
        string lactose = FormatNutrient(MapNutrient(nutrientValues, "lactose"));
        string alcohol = FormatNutrient(MapNutrient(nutrientValues, "alcohol"));
        string nonStarchPolysaccharides = FormatNutrient(MapNutrient(nutrientValues, "non_starch_polysaccharides"));
        string fibre = FormatNutrient(MapNutrient(nutrientValues, "fibre_aoac"));
        string fatsSaturated = FormatNutrient(MapNutrient(nutrientValues, "fats_saturated"));
        string fatsMonounsaturated = FormatNutrient(MapNutrient(nutrientValues, "fats_monounsaturated"));
        string fatsPolyunsaturated = FormatNutrient(MapNutrient(nutrientValues, "fats_polyunsaturated"));
        string fatsTrans = FormatNutrient(MapNutrient(nutrientValues, "fats_trans"));
        string cholesterol = FormatNutrient(MapNutrient(nutrientValues, "cholesterol"));

        return new ProximatesDto(water, starch, totalSugars, glucose, galactose, fructose, sucrose, maltose,
            lactose, alcohol, nonStarchPolysaccharides, fibre, fatsSaturated, fatsMonounsaturated,
            fatsPolyunsaturated, fatsTrans, cholesterol);
    }
    
    private VitaminsDto? MapVitamins(ICollection<NutrientValues> nutrientValues)
    {
        string retinol = FormatNutrient(MapNutrient(nutrientValues, "retinol"));
        string thiamin = FormatNutrient(MapNutrient(nutrientValues, "thiamin"));
        string riboflavin = FormatNutrient(MapNutrient(nutrientValues, "riboflavin"));
        string niacin = FormatNutrient(MapNutrient(nutrientValues, "niacin"));
        string pantothenate = FormatNutrient(MapNutrient(nutrientValues, "pantothenate"));
        string biotin = FormatNutrient(MapNutrient(nutrientValues, "biotin"));
        string folate = FormatNutrient(MapNutrient(nutrientValues, "folate"));
        string vitaminB12 = FormatNutrient(MapNutrient(nutrientValues, "vitamin_b12"));
        string vitaminB6 = FormatNutrient(MapNutrient(nutrientValues, "vitamin_b6"));
        string vitaminC = FormatNutrient(MapNutrient(nutrientValues, "vitamin_c"));
        string vitaminD = FormatNutrient(MapNutrient(nutrientValues, "vitamin_d"));
        string vitaminE = FormatNutrient(MapNutrient(nutrientValues, "vitamin_e"));
        string vitaminK1 = FormatNutrient(MapNutrient(nutrientValues, "vitammin_k1"));
        string carotene = FormatNutrient(MapNutrient(nutrientValues, "carotene"));
        string trytophan = FormatNutrient(MapNutrient(nutrientValues, "trytophan"));

        return new VitaminsDto(retinol, thiamin, riboflavin, niacin, pantothenate, biotin, folate, vitaminB12,
            vitaminB6, vitaminC, vitaminD, vitaminE, vitaminK1, carotene, trytophan);
    }
    
    private MineralsDto? MapMinerals(ICollection<NutrientValues> nutrientValues)
    {
        string sodium = FormatNutrient(MapNutrient(nutrientValues, "sodium"));
        string potassium = FormatNutrient(MapNutrient(nutrientValues, "potassium"));
        string calcium = FormatNutrient(MapNutrient(nutrientValues, "calcium"));
        string magnesium = FormatNutrient(MapNutrient(nutrientValues, "magnesium"));
        string phosphorus = FormatNutrient(MapNutrient(nutrientValues, "phosphorus"));
        string iron = FormatNutrient(MapNutrient(nutrientValues, "iron"));
        string copper = FormatNutrient(MapNutrient(nutrientValues, "copper"));
        string zinc = FormatNutrient(MapNutrient(nutrientValues, "zinc"));
        string chloride = FormatNutrient(MapNutrient(nutrientValues, "chloride"));
        string manganese = FormatNutrient(MapNutrient(nutrientValues, "manganese"));
        string selenium = FormatNutrient(MapNutrient(nutrientValues, "selenium"));
        string iodine = FormatNutrient(MapNutrient(nutrientValues, "iodine"));


        return new MineralsDto(sodium, potassium, calcium, magnesium, phosphorus, iron, copper, zinc, chloride,
            manganese, selenium, iodine);
    }

    private NutrientUnitAndValueDto MapNutrient(ICollection<NutrientValues> nutrientValues, string nutrientName)
    {
        foreach (NutrientValues nutrientValue in nutrientValues)
        {
            string? nutrientValueName = nutrientValue.NutrientsAndEnergy?.Name?.ToLower();
            if (nutrientValueName == nutrientName.ToLower())
            {
                return new NutrientUnitAndValueDto(
                    nutrientValue.NutrientsAndEnergy?.UnitSymbol,
                    nutrientValue.Value
                );
            }
        }

        // If no matching nutrient is found, return an instance with empty values
        return new NutrientUnitAndValueDto("N/A", "N/A");
    }
   
    private string FormatNutrient(NutrientUnitAndValueDto nutrient)
    {
        if (nutrient.Value is "Tr" or "N/A" or "N")
        {
            return nutrient.Value;
        }
        
        return $"{nutrient.Value}{nutrient.UnitSymbol}";
    }
}
