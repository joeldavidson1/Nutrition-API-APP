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
                'trytophan_mg': row['Tryptophan/60 (mg)']
            }
        }

        nutrition.append(food_dict)

# for food in nutrition:
#     print(food)
# final = json.dumps(nutrition)

with open('nutrition.json', 'w') as f:
    json.dump(nutrition, f)

# CMD + shift + P to format json

# food_name = parsed_data['foods'][1]['foodName']
# protein_amount = parsed_data['foods'][1]['foodMacronutrients']['Protein (g)']
# protein_information = parsed_data['nutrientsInformation']['macronutrients']['protein']
# vitamink_information = parsed_data['nutrientsInformation']['micronutrients']['vitamins']['vitaminK']

# with open('test.json') as f:
#     parsed_data = json.load(f)

# print(parsed_data[0]['foodMicronutrients']['foodMinerals']['sodium'])
# print(parsed_data[0]['foodMicronutrients']['foodVitamins']['vitaminC'])

    # food_dict = {
    #     'foodCode': row['Food Code'],
    #     'foodName': row['Food Name'],
    #     'foodDescription': row['Description'],
    #     'foodGroup': row['Group'],
    #     'foodDataReferences': row['Main data references'],
    #     'energy_kcal': row['Energy (kcal)'],
    #     'energy_kj': row['Energy (kJ)'],
    #     'protein': row['Protein (g)'],
    #     'fat': row['Fat (g)'],
    #     'carbohydrate': row['Carbohydrate (g)'],
    #     'water': row['Water (g)'],
    #     'starch': row['Starch (g)'],
    #     'total_sugars': row['Total sugars (g)'],
    #     'glucose': row['Glucose (g)'],
    #     'galactose': row['Galactose (g)'],
    #     'fructose': row['Fructose (g)'],
    #     'sucrose': row['Sucrose (g)'],
    #     'maltose': row['Maltose (g)'],
    #     'lactose': row['Lactose (g)'],
    #     'alcohol': row['Alcohol (g)'],
    #     'non_starch_polysaccharides': row['NSP (g)'],
    #     'fibre_aoac': row['AOAC fibre (g)'],
    #     'fats_saturated': row['Satd FA /100g fd (g)'],
    #     'fats_monounsaturated': row['Mono FA /100g food (g)'],
    #     'fats_polyunsaturated': row['Poly FA /100g food (g)'],
    #     'fats_trans': row['Trans FAs /100g food (g)'],
    #     'cholesterol': row['Cholesterol (mg)'],
    #     'sodium': row['Sodium (mg)'],
    #     'potassium': row['Potassium (mg)'],
    #     'calcium': row['Calcium (mg)'],
    #     'magnesium': row['Magnesium (mg)'],
    #     'phosphorus': row['Phosphorus (mg)'],
    #     'iron': row['Iron (mg)'],
    #     'copper': row['Copper (mg)'],
    #     'zinc': row['Zinc (mg)'],
    #     'chloride': row['Chloride (mg)'],
    #     'manganese': row['Manganese (mg)'],
    #     'selenium': row['Selenium (µg)'],
    #     'iodine': row['Iodine (µg)'],
    #     'retinol': row['Retinol (µg)'],
    #     'thiamin': row['Thiamin (mg)'],
    #     'riboflavin': row['Riboflavin (mg)'],
    #     'niacin': row['Niacin (mg)'],
    #     'pantothenate': row['Pantothenate (mg)'],
    #     'vitamin_b6': row['Vitamin B6 (mg)'],
    #     'biotin': row['Biotin (µg)'],
    #     'folate': row['Folate (µg)'],
    #     'vitamin_b12': row['Vitamin B12 (µg)'],
    #     'vitamin_c': row['Vitamin C (mg)'],
    #     'vitamin_d': row['Vitamin D (µg)'],
    #     'vitamin_e': row['Vitamin E (mg)'],
    #     'vitamin_k1': row['Vitamin K1 (µg)'],
    #     'carotene': row['Carotene (µg)'],
    #     'trytophan': row['Tryptophan/60 (mg)']
    # }
