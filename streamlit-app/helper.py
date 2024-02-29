import re
import copy
from collections import defaultdict
import streamlit as st
import constants
import plotly.express as px
import pandas as pd

def get_key(my_dict, val):
    for key, value in my_dict.items():
        if value == val:
            return key

    return "Value does not exist in the dictionary"


def extract_measurement(option):
    # Use regex to find the measurement unit
    match = re.search(r'\((.*?)\)', option)
    # If a match is found, return the measurement unit
    if match:
        return constants.MEASUREMENT_UNITS[match.group(1)]
    # If no match is found, return None
    else:
        return None
    

def format_nutrient_option(option, for_api=True):
    # Remove brackets
    option = re.sub(r'[()]', '', option)
    # Split the option into words
    words = option.split()
    # Get the measurement unit
    measurement = words[-1]
    # If there is only one word, return it as is
    if len(words) == 1:
        return option.lower() if for_api else option
    # Remove spaces and underscores between words if there are more than two words
    elif len(words) > 2:
        formatted_option = ''.join(words[:-1])
    else:
        formatted_option = '_'.join(words[:-1])
    # Append the measurement unit with an underscore
    formatted_option += '_' + measurement

    # Convert to lowercase if for_api is True, otherwise preserve the original capitalization
    return formatted_option.lower() if for_api else formatted_option[0].lower() + formatted_option[1:]


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

        macronutrients = selected_row['Macronutrients']
        names = ['Protein', 'Fat', 'Carbohydrate']
        colour_dict = {'Protein': '#FF8333', 'Fat': '#FFD133', 'Carbohydrate': '#11A400'}
        create_bar_chart(list(macronutrients.values()), names, "Macronutrient Composition as a Percentage (%)", "Macronutrient", colour_dict)

        fats = [selected_row['Proximates'][fat] for fat in ['fatsSaturated_g', 'fatsMonounsaturated_g', 'fatsPolyunsaturated_g', 'fatsTrans_g']]
        names = ['Saturated', 'Monounsaturated', 'Polyunsaturated', 'Trans']
        create_bar_chart(fats, names, "Fats Composition as a Percentage (%)", "Fatty Acid")

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
            

def create_bar_chart(data, names, title, x_label, colour_dict=None):
    if any(value is None for value in data):
        st.markdown("*Not enough reliable information about the amounts to display chart*")
        return 
    
    # Convert the values to percentages
    total = sum(data)
    if total == 0:
        st.markdown("*No data available to display chart*")
        return
    
    percentages = [(value / total) * 100 for value in data]
    fig = px.bar(x=names, y=percentages, 
                title=title,
                labels={"x": x_label, "y": "Percentage (%)"},
                color = names,
                color_discrete_map=colour_dict)
    fig.update_yaxes(range=[0, 100])
    fig.update_layout(showlegend=False)
    st.plotly_chart(fig)
