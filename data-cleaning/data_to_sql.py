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

