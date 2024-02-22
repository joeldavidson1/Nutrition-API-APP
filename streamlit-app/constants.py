ENERGY = ["Energy (kcal)", "Energy (kj)"]

MACRONUTRIENTS = ["Protein (g)", "Fat (g)", "Carbohydrate (g)"]

PROXIMATES = ["Water (g)", "Starch (g)", "Total Sugars (g)", "Glucose (g)", "Galactose (g)", 
                          "Fructose (g)", "Sucrose (g)", "Maltose (g)", "Lactose (g)", "Alcohol (g)", 
                          "Non-Starch Polysaccharides (g)", "Fibre (g)", "Saturated Fats (g)", 
                          "Monounsaturated Fats (g)", "Polyunsaturated Fats (g)", "Trans Fats (g)", 
                          "Cholesterol (g)"]
                          
VITAMINS = ["Vitamin A (mcg)", "Vitamin B1 (mg)", "Vitamin B2 (mg)", "Vitamin B3 (mg)",
                    "Vitamin B5 (mg)", "Vitamin B6 (mg)", "Vitamin B12 (mcg)", "Vitamin C (mg)",
                    "Vitamin D (mcg)", "Vitamin E (mg)", "Vitamin K1 (mcg)", "Biotin (mcg)", 
                    "Folate (mcg)", "Carotene (mcg)", "Tryptophan (mg)"]

MINERALS = ["Sodium (mg)", "Potassium (mg)", "Calcium (mg)", "Magnesium (mg)", "Phosphorus (mg)", 
                    "Iron (mg)", "Copper (mg)", "Zinc (mg)", "Chloride (mg)", "Manganese (mg)", 
                    "Selenium (mcg)", "Iodine (mcg)"]

NUTRIENTS_TO_CATEGORIES = {
    **{nutrient: "Energy" for nutrient in ENERGY},
    **{nutrient: "Macronutrients" for nutrient in MACRONUTRIENTS},
    **{nutrient: "Proximates" for nutrient in PROXIMATES},
    **{nutrient: "Vitamins" for nutrient in VITAMINS},
    **{nutrient: "Minerals" for nutrient in MINERALS}
} 