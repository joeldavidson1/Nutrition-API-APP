import psycopg2
import json
import csv

# ----- CHANGE THIS ------
connection = psycopg2.connect(host='localhost', dbname='postgres',
                              user='postgres', password='12345')
cursor = connection.cursor()


def create_tables():
    try:

        # cursor.execute(""" CREATE TABLE IF NOT EXISTS person (
        #     id INT PRIMARY KEY,
        #     name VARCHAR(255)
        # )
        # """)

        # defining the SQL statements which will create the tables in PostgreSQL
        create_food_groups_table = """
        CREATE TABLE IF NOT EXISTS "FoodGroups" (
            "FoodGroupCode" VARCHAR PRIMARY KEY,
            "Description" VARCHAR
        )
        """

        create_food_items_table = """
        CREATE TABLE IF NOT EXISTS "FoodItems" (
            "FoodCode" VARCHAR PRIMARY KEY,
            "Name" VARCHAR,
            "Description" VARCHAR,
            "DataReferences" VARCHAR,
            "FoodGroupCode" VARCHAR,
            FOREIGN KEY ("FoodGroupCode") REFERENCES "FoodGroups" ("FoodGroupCode")
        )
        """

        create_energy_table = """
        CREATE TABLE IF NOT EXISTS "Energy" (
            "FoodCode" VARCHAR PRIMARY KEY,
            "Kcal" FLOAT NULL,
            "Kj" FLOAT NULL,
            FOREIGN KEY ("FoodCode") REFERENCES "FoodItems" ("FoodCode")
        )
        """

        create_macronutrients_table = """
        CREATE TABLE IF NOT EXISTS "Macronutrients" (
            "FoodCode" VARCHAR PRIMARY KEY,
            "Protein_g" FLOAT NULL,
            "Fat_g" FLOAT NULL,
            "Carbohydrate_g" FLOAT NULL,
            FOREIGN KEY ("FoodCode") REFERENCES "FoodItems" ("FoodCode")
        )
        """

        create_proximates_table = """
        CREATE TABLE IF NOT EXISTS "Proximates" (
            "FoodCode" VARCHAR PRIMARY KEY,
            "Water_g" FLOAT NULL,
            "Starch_g" FLOAT NULL,
            "Total_Sugars_g" FLOAT NULL,
            "Glucose_g" FLOAT NULL,
            "Galactose_g" FLOAT NULL,
            "Fructose_g" FLOAT NULL,
            "Sucrose_g" FLOAT NULL,
            "Maltose_g" FLOAT NULL,
            "Lactose_g" FLOAT NULL,
            "Alcohol_g" FLOAT NULL,
            "Non_Starch_Polysaccharides_g" FLOAT NULL,
            "Fibre_g" FLOAT NULL,
            "Fats_Saturated_g" FLOAT NULL,
            "Fats_Monounsaturated_g" FLOAT NULL,
            "Fats_Polyunsaturated_g" FLOAT NULL,
            "Fats_Trans_g" FLOAT NULL,
            "Cholesterol_g" FLOAT NULL,
            FOREIGN KEY ("FoodCode") REFERENCES "FoodItems" ("FoodCode")
        )
        """

        create_minerals_table = """
        CREATE TABLE IF NOT EXISTS "Minerals" (
            "FoodCode" VARCHAR PRIMARY KEY,
            "Sodium_mg" FLOAT NULL,
            "Potassium_mg" FLOAT NULL,
            "Calcium_mg" FLOAT NULL,
            "Magnesium_mg" FLOAT NULL,
            "Phosphorus_mg" FLOAT NULL,
            "Iron_mg" FLOAT NULL,
            "Copper_mg" FLOAT NULL,
            "Zinc_mg" FLOAT NULL,
            "Chloride_mg" FLOAT NULL,
            "Manganese_mg" FLOAT NULL,
            "Selenium_mcg" FLOAT NULL,
            "Iodine_mcg" FLOAT NULL,
            FOREIGN KEY ("FoodCode") REFERENCES "FoodItems" ("FoodCode")
        )
        """

        create_vitamins_table = """
        CREATE TABLE IF NOT EXISTS "Vitamins" (
            "FoodCode" VARCHAR PRIMARY KEY,
            "Retinol_mcg" FLOAT NULL,
            "Thiamin_mg" FLOAT NULL,
            "Riboflavin_mg" FLOAT NULL,
            "Niacin_mg" FLOAT NULL,
            "Pantothenate_mg" FLOAT NULL,
            "Vitamin_B6_mg" FLOAT NULL,
            "Biotin_mcg" FLOAT NULL,
            "Folate_mcg" FLOAT NULL,
            "Vitamin_B12_mcg" FLOAT NULL,
            "Vitamin_C_mg" FLOAT NULL,
            "Vitamin_D_mcg" FLOAT NULL,
            "Vitamin_E_mg" FLOAT NULL,
            "Vitamin_K1_mcg" FLOAT NULL,
            "Carotene_mcg" FLOAT NULL,
            "Tryptophan_mg" FLOAT NULL,
            FOREIGN KEY ("FoodCode") REFERENCES "FoodItems" ("FoodCode")
        )
        """

        # Execute the commands
        cursor.execute(create_food_groups_table)
        cursor.execute(create_food_items_table)
        cursor.execute(create_energy_table)
        cursor.execute(create_macronutrients_table)
        cursor.execute(create_proximates_table)
        cursor.execute(create_minerals_table)
        cursor.execute(create_vitamins_table)

        # connection.commit()

        # cursor.close()
        # connection.close()

        print('Successfully created tables in PostgreSQL')

    except psycopg2.Error as e:
        print('Error creating tables:', e)


def replace_null_with_none(obj):
    if isinstance(obj, list):
        return [replace_null_with_none(item) for item in obj]
    elif isinstance(obj, dict):
        return {key: replace_null_with_none(value) if value != "NULL" else None for key, value in obj.items()}
    else:
        return obj


def insert_data_to_tables():
    try:
        # csv here
        with open('food_groups.csv', 'r', newline='', encoding='utf-8') as csv_file:
            reader = csv.reader(csv_file)
            next(reader)

            for row in reader:
                cursor.execute("""
                INSERT INTO "FoodGroups" ("FoodGroupCode", "Description") VALUES (%s, %s)
                """, (
                    row[0],
                    row[1]
                ))

        with open('nutrition.json') as json_file:
            food_data = json.load(json_file)
            food_data = replace_null_with_none(food_data)

            for food_item in food_data:
                cursor.execute("""
                INSERT INTO "FoodItems" ("FoodCode", "Name", "Description", "DataReferences", "FoodGroupCode") VALUES (%s, %s, %s, %s, %s)
                """, (
                    food_item['foodCode'],
                    food_item['foodName'],
                    food_item['foodDescription'],
                    food_item['foodDataReferences'],
                    food_item['foodGroup']
                ))

                cursor.execute("""
                INSERT INTO "Energy" ("FoodCode", "Kcal", "Kj")
                VALUES (%s, %s, %s)
                """, (
                    food_item['foodCode'],
                    food_item['foodEnergy']['kcal'],
                    food_item['foodEnergy']['kj'],
                ))

                cursor.execute("""
                INSERT INTO "Macronutrients" ("FoodCode", "Protein_g", "Fat_g", "Carbohydrate_g")
                VALUES (%s, %s, %s, %s)
                """, (
                    food_item['foodCode'],
                    food_item['foodMacronutrients']['protein_g'],
                    food_item['foodMacronutrients']['fat_g'],
                    food_item['foodMacronutrients']['carbohydrate_g']
                ))

                cursor.execute("""
                INSERT INTO "Proximates" ("FoodCode", "Water_g", "Starch_g", "Total_Sugars_g", "Glucose_g", "Galactose_g", "Fructose_g", "Sucrose_g", "Maltose_g", "Lactose_g", "Alcohol_g", "Non_Starch_Polysaccharides_g", "Fibre_g", "Fats_Saturated_g", "Fats_Monounsaturated_g", "Fats_Polyunsaturated_g", "Fats_Trans_g", "Cholesterol_g")
                VALUES (%s, %s, %s, %s, %s, %s, %s, %s, %s, %s, %s, %s, %s, %s, %s, %s, %s, %s)
                """, (
                    food_item['foodCode'],
                    food_item['foodProximates']['water_g'],
                    food_item['foodProximates']['starch_g'],
                    food_item['foodProximates']['total_sugars_g'],
                    food_item['foodProximates']['glucose_g'],
                    food_item['foodProximates']['galactose_g'],
                    food_item['foodProximates']['fructose_g'],
                    food_item['foodProximates']['sucrose_g'],
                    food_item['foodProximates']['maltose_g'],
                    food_item['foodProximates']['lactose_g'],
                    food_item['foodProximates']['alcohol_g'],
                    food_item['foodProximates']['non_starch_polysaccharides_g'],
                    food_item['foodProximates']['fibre_aoac_g'],
                    food_item['foodProximates']['fats_saturated_g'],
                    food_item['foodProximates']['fats_monounsaturated_g'],
                    food_item['foodProximates']['fats_polyunsaturated_g'],
                    food_item['foodProximates']['fats_trans_g'],
                    food_item['foodProximates']['cholesterol_g'],
                ))

                # Insert into Minerals Table
                cursor.execute("""
                    INSERT INTO "Minerals" ("FoodCode", "Sodium_mg", "Potassium_mg", "Calcium_mg", "Magnesium_mg", "Phosphorus_mg", "Iron_mg", "Copper_mg", "Zinc_mg", "Chloride_mg", "Manganese_mg", "Selenium_mcg", "Iodine_mcg")
                    VALUES (%s, %s, %s, %s, %s, %s, %s, %s, %s, %s, %s, %s, %s)
                """, (
                    food_item['foodCode'],
                    food_item['foodMinerals']['sodium_mg'],
                    food_item['foodMinerals']['potassium_mg'],
                    food_item['foodMinerals']['calcium_mg'],
                    food_item['foodMinerals']['magensium_mg'],
                    food_item['foodMinerals']['phosphorus_mg'],
                    food_item['foodMinerals']['iron_mg'],
                    food_item['foodMinerals']['copper_mg'],
                    food_item['foodMinerals']['zinc_mg'],
                    food_item['foodMinerals']['chloride_mg'],
                    food_item['foodMinerals']['manganese_mg'],
                    food_item['foodMinerals']['selenium_mcg'],
                    food_item['foodMinerals']['iodine_mcg']
                ))

                # Insert into Vitamins Table
                cursor.execute("""
                    INSERT INTO "Vitamins" ("FoodCode", "Retinol_mcg", "Thiamin_mg", "Riboflavin_mg", "Niacin_mg", "Pantothenate_mg", "Vitamin_B6_mg", "Biotin_mcg", "Folate_mcg", "Vitamin_B12_mcg", "Vitamin_C_mg", "Vitamin_D_mcg", "Vitamin_E_mg", "Vitamin_K1_mcg", "Carotene_mcg", "Tryptophan_mg")
                    VALUES (%s, %s, %s, %s, %s, %s, %s, %s, %s, %s, %s, %s, %s, %s, %s, %s)
                """, (
                    food_item['foodCode'],
                    food_item['foodVitamins']['retinol_mcg'],
                    food_item['foodVitamins']['thiamin_mg'],
                    food_item['foodVitamins']['riboflavin_mg'],
                    food_item['foodVitamins']['niacin_mg'],
                    food_item['foodVitamins']['pantothenate_mg'],
                    food_item['foodVitamins']['vitamin_b6_mg'],
                    food_item['foodVitamins']['biotin_mcg'],
                    food_item['foodVitamins']['folate_mcg'],
                    food_item['foodVitamins']['vitamin_b12_mcg'],
                    food_item['foodVitamins']['vitamin_c_mg'],
                    food_item['foodVitamins']['vitamin_d_mcg'],
                    food_item['foodVitamins']['vitamin_e_mg'],
                    food_item['foodVitamins']['vitamin_k1_mcg'],
                    food_item['foodVitamins']['carotene_mcg'],
                    food_item['foodVitamins']['tryptophan_mg']
                ))

        print('Successfully inserted data into tables')

    except psycopg2.Error as e:
        print('Error inserting data into tables:', e)


create_tables()
insert_data_to_tables()

connection.commit()

cursor.close()
connection.close()


# import psycopg2
# import json
# import csv
# import uuid

# # ----- CHANGE THIS ------
# connection = psycopg2.connect(host='localhost', dbname='postgres',
#                               user='postgres', password='12345')
# cursor = connection.cursor()


# def create_tables():
#     try:
#         # defining the SQL statements which will create the tables in PostgreSQL
#         create_food_groups_table = """
#         CREATE TABLE IF NOT EXISTS "FoodGroups" (
#             "FoodGroupCode" VARCHAR PRIMARY KEY,
#             "Description" VARCHAR
#         )
#         """

#         create_food_items_table = """
#         CREATE TABLE IF NOT EXISTS "FoodItems" (
#             "FoodCode" VARCHAR PRIMARY KEY,
#             "Name" VARCHAR,
#             "Description" VARCHAR,
#             "DataReferences" VARCHAR,
#             "FoodGroupCode" VARCHAR,
#             FOREIGN KEY ("FoodGroupCode") REFERENCES "FoodGroups" ("FoodGroupCode")
#         )
#         """

#         create_nutrient_categories = """
#         CREATE TABLE IF NOT EXISTS "NutrientCategories" (
#             "CategoryId" VARCHAR PRIMARY KEY,
#             "CategoryName" VARCHAR
#         )
#         """

#         create_nutrients_and_energy_table = """
#         CREATE TABLE IF NOT EXISTS "NutrientsAndEnergy" (
#             "NutrientId" VARCHAR PRIMARY KEY,
#             "Name" VARCHAR NOT NULL,
#             "UnitSymbol" VARCHAR NOT NULL,
#             "CategoryId" VARCHAR,
#             FOREIGN KEY ("CategoryId") REFERENCES "NutrientCategories" ("CategoryId")
#         )
#         """

#         create_nutrient_values_table = """
#         CREATE TABLE IF NOT EXISTS "NutrientValues" (
#             "ValuesId" VARCHAR PRIMARY KEY,
#             "FoodCode" VARCHAR,
#             "NutrientId" VARCHAR,
#             "Value" FLOAT NULL,
#             FOREIGN KEY ("FoodCode") REFERENCES "FoodItems" ("FoodCode"),
#             FOREIGN KEY ("NutrientId") REFERENCES "NutrientsAndEnergy" ("NutrientId")
#         )
#         """

#         # Execute the commands
#         cursor.execute(create_food_groups_table)
#         cursor.execute(create_food_items_table)
#         cursor.execute(create_nutrient_categories)
#         cursor.execute(create_nutrients_and_energy_table)
#         cursor.execute(create_nutrient_values_table)

#         # connection.commit()

#         # cursor.close()
#         # connection.close()

#         print('Successfully created tables in PostgreSQL')

#     except psycopg2.Error as e:
#         print('Error creating tables:', e)


# NUTRIENT_CATEGORIES = [
#     'energy_kcal', 'energy_kj', 'protein', 'fat', 'carbohydrate',
#     'water', 'starch', 'total_sugars', 'glucose', 'galactose',
#     'fructose', 'sucrose', 'maltose', 'lactose', 'alcohol',
#     'non_starch_polysaccharides', 'fibre_aoac', 'fats_saturated',
#     'fats_monounsaturated', 'fats_polyunsaturated', 'fats_trans',
#     'cholesterol', 'retinol', 'thiamin', 'riboflavin', 'niacin',
#     'pantothenate', 'biotin', 'folate', 'vitamin_b12', 'vitamin_b6',
#     'vitamin_c', 'vitamin_d', 'vitamin_e', 'vitamin_k1', 'carotene',
#     'trytophan', 'sodium', 'potassium', 'calcium', 'magnesium',
#     'phosphorus', 'iron', 'copper', 'zinc', 'chloride', 'manganese',
#     'selenium', 'iodine'
# ]


# def insert_data_to_tables():
#     try:
#         # csv here
#         with open('food_groups.csv', 'r', newline='', encoding='utf-8') as csv_file:
#             reader = csv.reader(csv_file)
#             next(reader)

#             for row in reader:
#                 cursor.execute("""
#                 INSERT INTO "FoodGroups" ("FoodGroupCode", "Description") VALUES (%s, %s)
#                 """, (
#                     row[0],
#                     row[1]
#                 ))

#         # Nutrient categories data
#         nutrient_categories_data = [
#             (str(uuid.uuid4()), 'energy'),
#             (str(uuid.uuid4()), 'macronutrients'),
#             (str(uuid.uuid4()), 'proximates'),
#             (str(uuid.uuid4()), 'vitamins'),
#             (str(uuid.uuid4()), 'minerals')
#         ]

#         cursor.execute("""
#         INSERT INTO "NutrientCategories" ("CategoryId", "CategoryName")
#         VALUES %s, %s, %s, %s, %s
#             """, nutrient_categories_data)

#         # Fetch CategoryId for each category
#         cursor.execute("SELECT * FROM \"NutrientCategories\"")
#         categories = cursor.fetchall()

#         cursor.execute("""
#         INSERT INTO "NutrientsAndEnergy" ("NutrientId", "Name", "UnitSymbol", "CategoryId")
#         VALUES  %s, %s, %s, %s, %s, %s, %s, %s, %s, %s, %s, %s, %s, %s, %s, %s, %s, %s, %s, %s, %s, %s, %s, %s, %s, %s, %s, %s,
#             %s, %s, %s, %s, %s, %s, %s, %s, %s, %s, %s, %s, %s, %s, %s, %s, %s, %s, %s, %s, %s
#         """, (
#             (str(uuid.uuid4()), 'energy_kcal',
#              'kcal', categories[0][0]),
#             (str(uuid.uuid4()), 'energy_kj', 'kj', categories[0][0]),
#             (str(uuid.uuid4()), 'protein', 'g', categories[1][0]),
#             (str(uuid.uuid4()), 'fat', 'g', categories[1][0]),
#             (str(uuid.uuid4()), 'carbohydrate', 'g', categories[1][0]),
#             (str(uuid.uuid4()), 'water', 'g', categories[2][0]),
#             (str(uuid.uuid4()), 'starch', 'g', categories[2][0]),
#             (str(uuid.uuid4()), 'total_sugars', 'g', categories[2][0]),
#             (str(uuid.uuid4()), 'glucose', 'g', categories[2][0]),
#             (str(uuid.uuid4()), 'galactose', 'g', categories[2][0]),
#             (str(uuid.uuid4()), 'fructose', 'g', categories[2][0]),
#             (str(uuid.uuid4()), 'sucrose', 'g', categories[2][0]),
#             (str(uuid.uuid4()), 'maltose', 'g', categories[2][0]),
#             (str(uuid.uuid4()), 'lactose', 'g', categories[2][0]),
#             (str(uuid.uuid4()), 'alcohol', 'g', categories[2][0]),
#             (str(uuid.uuid4()), 'non_starch_polysaccharides',
#              'g', categories[2][0]),
#             (str(uuid.uuid4()), 'fibre_aoac', 'g', categories[2][0]),
#             (str(uuid.uuid4()), 'fats_saturated', 'g', categories[2][0]),
#             (str(uuid.uuid4()), 'fats_monounsaturated',
#              'g', categories[2][0]),
#             (str(uuid.uuid4()), 'fats_polyunsaturated',
#              'g', categories[2][0]),
#             (str(uuid.uuid4()), 'fats_trans', 'g', categories[2][0]),
#             (str(uuid.uuid4()), 'cholesterol', 'g', categories[2][0]),
#             (str(uuid.uuid4()), 'retinol', 'mcg', categories[3][0]),
#             (str(uuid.uuid4()), 'thiamin', 'mg', categories[3][0]),
#             (str(uuid.uuid4()), 'riboflavin', 'mg', categories[3][0]),
#             (str(uuid.uuid4()), 'niacin', 'mg', categories[3][0]),
#             (str(uuid.uuid4()), 'pantothenate',
#              'mg', categories[3][0]),
#             (str(uuid.uuid4()), 'biotin', 'mcg', categories[3][0]),
#             (str(uuid.uuid4()), 'folate', 'mcg', categories[3][0]),
#             (str(uuid.uuid4()), 'vitamin_b12', 'mcg', categories[3][0]),
#             (str(uuid.uuid4()), 'vitamin_b6', 'mg', categories[3][0]),
#             (str(uuid.uuid4()), 'vitamin_c', 'mg', categories[3][0]),
#             (str(uuid.uuid4()), 'vitamin_d', 'mg', categories[3][0]),
#             (str(uuid.uuid4()), 'vitamin_e', 'mg', categories[3][0]),
#             (str(uuid.uuid4()), 'vitammin_k1', 'mcg', categories[3][0]),
#             (str(uuid.uuid4()), 'carotene', 'mcg', categories[3][0]),
#             (str(uuid.uuid4()), 'trytophan', 'mg', categories[3][0]),
#             (str(uuid.uuid4()), 'sodium', 'mg', categories[4][0]),
#             (str(uuid.uuid4()), 'potassium', 'mg', categories[4][0]),
#             (str(uuid.uuid4()), 'calcium', 'mg', categories[4][0]),
#             (str(uuid.uuid4()), 'magnesium', 'mg', categories[4][0]),
#             (str(uuid.uuid4()), 'phosphorus', 'mg', categories[4][0]),
#             (str(uuid.uuid4()), 'iron', 'mg', categories[4][0]),
#             (str(uuid.uuid4()), 'copper', 'mg', categories[4][0]),
#             (str(uuid.uuid4()), 'zinc', 'mg', categories[4][0]),
#             (str(uuid.uuid4()), 'chloride', 'mg', categories[4][0]),
#             (str(uuid.uuid4()), 'manganese', 'mg', categories[4][0]),
#             (str(uuid.uuid4()), 'selenium', 'mcg', categories[4][0]),
#             (str(uuid.uuid4()), 'iodine', 'mcg', categories[4][0])
#         ))

#         # Fetch NutrientId for each category
#         cursor.execute("SELECT * FROM \"NutrientsAndEnergy\"")
#         nutrient_id_rows = cursor.fetchall()

#         # Extracting NutrientId values from the fetched rows
#         nutrient_ids = [row[0] for row in nutrient_id_rows]

#         with open('nutrition.json') as json_file:
#             food_data = json.load(json_file)

#             for food_item in food_data:
#                 food_code = food_item['foodCode']

#                 # Replace NULL strings with None for numeric columns
#                 for nutrient_category in NUTRIENT_CATEGORIES:
#                     if nutrient_category in food_item and food_item[nutrient_category] == 'NULL':
#                         food_item[nutrient_category] = None

#                 cursor.execute("""
#                 INSERT INTO "FoodItems" ("FoodCode", "Name", "Description", "DataReferences", "FoodGroupCode")
#                 VALUES (%s, %s, %s, %s, %s)
#                 """, (
#                     food_code,
#                     food_item['foodName'],
#                     food_item['foodDescription'],
#                     food_item['foodDataReferences'],
#                     food_item['foodGroup']
#                 ))

#                 nutrient_values_data = [
#                     (str(uuid.uuid4()), food_code,
#                      nutrient_ids[i], food_item[nutrient_category])
#                     for i, nutrient_category in enumerate(NUTRIENT_CATEGORIES)
#                 ]

#                 cursor.executemany("""
#                 INSERT INTO "NutrientValues" ("ValuesId", "FoodCode", "NutrientId", "Value")
#                 VALUES (%s, %s, %s, %s)
#                     """, nutrient_values_data)

#         print('Successfully inserted data into tables')

#     except psycopg2.Error as e:
#         print('Error inserting data into tables:', e)


# create_tables()
# insert_data_to_tables()

# connection.commit()

# cursor.close()
# connection.close()
