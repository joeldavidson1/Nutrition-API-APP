import re
import copy
from collections import defaultdict
import streamlit as st
import constants
import pandas as pd

def reverse_format_nutrient_option(option):
    # Split the option into words
    words = option.split('_')
    # Add spaces before capital letters
    words[0] = re.sub(r"(?<=\w)([A-Z])", r" \1", words[0])
    # Capitalize each word and join them with a space, putting the last word in parentheses
    formatted_option = ' '.join(word.title() for word in words[:-1]) + ' (' + words[-1] + ')'

    return formatted_option


def get_key(my_dict, val):
    for key, value in my_dict.items():
        if value == val:
            return key

    return "Value does not exist in the dictionary"


def format_nutrient_option(option):
    # Remove brackets
    option = re.sub(r'[()]', '', option)
    # Split the option into words
    words = option.split()
    # Get the measurement unit
    measurement = words[-1]
    # If there is only one word, return it as is
    if len(words) == 1:
        return option.lower()
    # Remove spaces and underscores between words if there are more than two words
    elif len(words) > 2:
        formatted_option = ''.join(words[:-1])
    else:
        formatted_option = '_'.join(words[:-1])
    # Append the measurement unit with an underscore
    formatted_option += '_' + measurement

    return formatted_option.lower()

# Converts nested keys to lowercase, due to our previous formatting function for the API GET request
# def convert_response_to_lowercase(response):
#     new_response = []
#     for item in response:
#         new_item = {}
#         for k, v in item.items():
#             if isinstance(v, dict):
#                 v = {k.lower(): v[k] for k in v}
#             new_item[k] = v
#         new_response.append(new_item)

#     return new_response


def adjust_nutrition_values(nutrition_dict, quantity):
    adjusted_dict = copy.deepcopy(nutrition_dict)
    for key, value in adjusted_dict.items():
        if isinstance(value, dict):
            adjusted_dict[key] = adjust_nutrition_values(value, quantity)
        elif isinstance(value, (int, float)):
            adjusted_dict[key] = value * (quantity / 100)
    return adjusted_dict


def add_nutrition_values(nutrition_dict, total_nutrition):
    for key, value in nutrition_dict.items():
        if isinstance(value, dict):
            if key not in total_nutrition:
                total_nutrition[key] = defaultdict(float)
            add_nutrition_values(value, total_nutrition[key])
        elif isinstance(value, (int, float)):
            total_nutrition[key] += value

def add_food_with_quantity(food, quantity):
    return (copy.deepcopy(food), quantity)


def reverse_format_nutrient_option(option):
    # Split the option into words
    words = option.split('_')
    # Add spaces before capital letters
    words[0] = re.sub(r"(?<=\w)([A-Z])", r" \1", words[0])
    # Capitalize each word and join them with a space, putting the last word in parentheses
    formatted_option = ' '.join(word.title() for word in words[:-1]) + ' (' + words[-1] + ')'

    return formatted_option
    

def display_selected_row(grid_table, food_groups_dict):
    if (grid_table['selected_rows']):
        selected_row = grid_table['selected_rows'][0]
        st.subheader(f'{selected_row['FoodCode']}: {selected_row['Name']}')
        st.markdown(f"**Food Group**: {get_key(food_groups_dict, selected_row['FoodGroupCode'])}")
        st.markdown(f"**Description**: {selected_row['Description']}")
        st.markdown(f"**Data References**: {selected_row['DataReferences']}")

        # Initialize a list to hold the columns
        columns = []

        for i, (category, nutrients) in enumerate(selected_row.items()):
            # Skip the keys that are not nutrient categories
            if category not in constants.NUTRIENT_CATEGORIES:
                continue

            # Create a new row every 2 categories
            if i % 2 == 0:
                columns = st.columns(2)

            # Convert the nutrients dictionary to a DataFrame
            df = pd.DataFrame([(nutrient, value) for nutrient, value in nutrients.items()], columns=['Nutrient', 'Value'])

            # Apply the reverse_format_nutrient_option method to the 'Nutrient' column
            df['Nutrient'] = df['Nutrient'].apply(reverse_format_nutrient_option)
            
            # Use the current column for the category subheader and DataFrame
            with columns[i % 2]:
                st.subheader(f"{category}:\n")
                st.dataframe(df)