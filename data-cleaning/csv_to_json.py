import csv
import json

nutrition = []
with open('combined_nutrition_tables.csv', 'r') as csv_file:
    csv_reader = csv.DictReader(csv_file)

    for row in csv_reader:
        food_dict = {
            'foodCode': row['Food Code'],
            'foodName': row['Food Name'],
            'foodDescription': row['Description'],
            'foodGroup': row['Group'],
            'foodDataReferences': row['Main data references'],
            'foodEnergy': {
                'kcal': row['Energy (kcal)'],
                'kj': row['Energy (kJ)'],
            },
            'foodMacronutrients': {
                'protein_g': row['Protein (g)'],
                'fat_g': row['Fat (g)'],
                'carbohydrate_g': row['Carbohydrate (g)']
            },
            'foodProximates': {
                'water_g': row['Water (g)'],
                'starch_g': row['Starch (g)'],
                'total_sugars_g': row['Total sugars (g)'],
                'glucose_g': row['Glucose (g)'],
                'galactose_g': row['Galactose (g)'],
                'fructose_g': row['Fructose (g)'],
                'sucrose_g': row['Sucrose (g)'],
                'maltose_g': row['Maltose (g)'],
                'lactose_g': row['Lactose (g)'],
                'alcohol_g': row['Alcohol (g)'],
                'non_starch_polysaccharides_g': row['NSP (g)'],
                'fibre_aoac_g': row['AOAC fibre (g)'],
                'fats_saturated_g': row['Satd FA /100g fd (g)'],
                'fats_monounsaturated_g': row['Mono FA /100g food (g)'],
                'fats_polyunsaturated_g': row['Poly FA /100g food (g)'],
                'fats_trans_g': row['Trans FAs /100g food (g)'],
                'cholesterol_g': row['Cholesterol (mg)']
            },
            'foodMinerals': {
                'sodium_mg': row['Sodium (mg)'],
                'potassium_mg': row['Potassium (mg)'],
                'calcium_mg': row['Calcium (mg)'],
                'magensium_mg': row['Magnesium (mg)'],
                'phosphorus_mg': row['Phosphorus (mg)'],
                'iron_mg': row['Iron (mg)'],
                'copper_mg': row['Copper (mg)'],
                'zinc_mg': row['Zinc (mg)'],
                'chloride_mg': row['Chloride (mg)'],
                'manganese_mg': row['Manganese (mg)'],
                'selenium_mcg': row['Selenium (µg)'],
                'iodine_mcg': row['Iodine (µg)']
            },
            'foodVitamins': {
                'retinol_mcg': row['Retinol (µg)'],
                'thiamin_mg': row['Thiamin (mg)'],
                'riboflavin_mg': row['Riboflavin (mg)'],
                'niacin_mg': row['Niacin (mg)'],
                'pantothenate_mg': row['Pantothenate (mg)'],
                'vitamin_b6_mg': row['Vitamin B6 (mg)'],
                'biotin_mcg': row['Biotin (µg)'],
                'folate_mcg': row['Folate (µg)'],
                'vitamin_b12_mcg': row['Vitamin B12 (µg)'],
                'vitamin_c_mg': row['Vitamin C (mg)'],
                'vitamin_d_mcg': row['Vitamin D (µg)'],
                'vitamin_e_mg': row['Vitamin E (mg)'],
                'vitamin_k1_mcg': row['Vitamin K1 (µg)'],
                'carotene_mcg': row['Carotene (µg)'],
                'tryptophan_mg': row['Tryptophan/60 (mg)']
            }
        }

        nutrition.append(food_dict)

# for food in nutrition:
#     print(food)
# final = json.dumps(nutrition)

with open('nutrition.json', 'w') as f:
    json.dump(nutrition, f)

