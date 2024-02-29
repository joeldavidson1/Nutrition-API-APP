ENERGY = ["kcal", "kj"]

MACRONUTRIENTS = ["Protein (g)", "Fat (g)", "Carbohydrate (g)"]

PROXIMATES = ["Water (g)", "Starch (g)", "Total Sugars (g)", "Glucose (g)", "Galactose (g)", 
                          "Fructose (g)", "Sucrose (g)", "Maltose (g)", "Lactose (g)", "Alcohol (g)", 
                          "Non Starch Polysaccharides (g)", "Fibre (g)", "Fats Saturated (g)", 
                          "Fats Monounsaturated (g)", "Fats Polyunsaturated (g)", "Fats Trans (g)", 
                          "Cholesterol (g)"]
                          
VITAMINS = ["Vitamin B6 (mg)", "Vitamin B12 (mcg)", "Vitamin C (mg)",
                    "Vitamin D (mcg)", "Vitamin E (mg)", "Vitamin K1 (mcg)", "Biotin (mcg)", 
                    "Folate (mcg)", "Carotene (mcg)", "Tryptophan (mg)", "Thiamin (mg)", "Folate (mcg)", "Retinol (mcg)",
                    "Riboflavin (mg)", "Niacin (mg)", "Pantothenate (mg)"]

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

NUTRIENT_CATEGORIES= ["Energy", "Macronutrients", "Proximates", "Vitamins", "Minerals"]

MEASUREMENT_UNITS = {'g': 'grams', 'mg': 'milligrams', 'mcg': 'micrograms'}