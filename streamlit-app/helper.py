import re
import copy
from collections import defaultdict
import streamlit as st
import constants
import plotly.express as px
import pandas as pd
from scipy import stats


def get_key(my_dict, val):
    """
    Returns the key in the dictionary `my_dict` that corresponds to the given value `val`.
    
    Args:
        my_dict (dict): The dictionary to search in.
        val: The value to search for.
    
    Returns:
        The key in `my_dict` that corresponds to `val`, or "Value does not exist in the dictionary"
        if the value is not found.
    """
    for key, value in my_dict.items():
        if value == val:
            return key

    return "Value does not exist in the dictionary"


def extract_measurement(option): 
    """
    Extracts the measurement unit from the given option.

    Parameters:
    option (str): The option containing the measurement unit.

    Returns:
    str or None: The extracted measurement unit, or None if no measurement unit is found.
    """
    # Use regex to find the measurement unit
    match = re.search(r'\((.*?)\)', option)
    if match:
        return constants.MEASUREMENT_UNITS[match.group(1)]
    else:
        return None
    

def format_nutrient_option(option, for_api=True):
    """
    Formats a nutrient option by removing brackets, splitting into words, and appending the measurement unit.

    Parameters:
    option (str): The nutrient option to be formatted.
    for_api (bool): Flag indicating whether the formatted option is for API usage. Default is True.

    Returns:
    str: The formatted nutrient option.
    """
    # Remove brackets
    option = re.sub(r'[()]', '', option)
    # Split the option into words
    words = option.split()
    # Get the measurement unit
    measurement = words[-1]
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
    """
    Adjusts the nutrition values in a dictionary based on the given quantity.

    Args:
        nutrition_dict (dict): A dictionary containing nutrition values.
        quantity (int or float): The quantity to adjust the nutrition values by.

    Returns:
        dict: A new dictionary with adjusted nutrition values.
    """
    adjusted_dict = copy.deepcopy(nutrition_dict)
    for key, value in adjusted_dict.items():
        if isinstance(value, dict):
            adjusted_dict[key] = adjust_nutrition_values(value, quantity)
        elif isinstance(value, (int, float)):
            adjusted_dict[key] = value * (quantity / 100)
    return adjusted_dict


from collections import defaultdict

def add_nutrition_values(nutrition_dict, total_nutrition):
    """
    Recursively adds the nutrition values from the given `nutrition_dict` to the `total_nutrition` dictionary.

    Args:
        nutrition_dict (dict): A dictionary containing nutrition values.
        total_nutrition (dict): A dictionary to store the total nutrition values.

    Returns:
        None
    """
    for key, value in nutrition_dict.items():
        if isinstance(value, dict):
            if key not in total_nutrition:
                total_nutrition[key] = defaultdict(float)
            add_nutrition_values(value, total_nutrition[key])
        elif isinstance(value, (int, float)):
            total_nutrition[key] += value

def add_food_with_quantity(food, quantity):
    """
    Adds a food item with its corresponding quantity.

    Args:
        food: The food item to be added.
        quantity: The quantity of the food item.

    Returns:
        A tuple containing the copied food item and its quantity.
    """
    return (copy.deepcopy(food), quantity)


def reverse_format_nutrient_option(option):
    """
    Reverse formats a nutrient option by adding spaces before capital letters,
    capitalising each word, and putting the last word in parentheses.

    Parameters:
    option (str): The nutrient option to be reverse formatted.

    Returns:
    str: The reverse formatted nutrient option.
    """
    if option == "kcal" or option == "kj":
        return option

    words = option.split('_')
    # Add spaces before capital letters
    words[0] = re.sub(r"(?<=\w)([A-Z])", r" \1", words[0])
    # Capitalise each word and join them with a space, putting the last word in parentheses
    formatted_option = ' '.join(word.title() for word in words[:-1]) + ' (' + words[-1] + ')'

    return formatted_option


def display_selected_row(grid_table, food_groups_dict):
    """
    Display the selected row from the grid table.

    Parameters:
    - grid_table (dict): The grid table containing the selected row.
    - food_groups_dict (dict): A dictionary mapping food group codes to food group names.

    Returns:
    None
    """
    if (grid_table['selected_rows']):
        selected_row = grid_table['selected_rows'][0]
        st.subheader(f"{selected_row['foodCode']}: {selected_row['name']}")
        st.markdown(f"**Food Group**: {get_key(food_groups_dict, selected_row['foodGroupCode'])}")
        st.markdown(f"**Description**: {selected_row['description']}")
        st.markdown(f"**Data References**: {selected_row['dataReferences']}")

        top_nutrients = calculate_top_5_nutrients(st.session_state.full_response, selected_row)
        display_top_nutrients(top_nutrients)

        macronutrients = selected_row['macronutrients']
        names = ['Protein', 'Fat', 'Carbohydrate']
        colour_dict = {'Protein': '#FF8333', 'Fat': '#FFD133', 'Carbohydrate': '#11A400'}
        create_bar_chart(list(macronutrients.values()), names, "Macronutrient Composition as a Percentage (%)", "Macronutrient", colour_dict)

        fats = [selected_row['proximates'][fat] for fat in ['fatsSaturated_g', 'fatsMonounsaturated_g', 'fatsPolyunsaturated_g', 'fatsTrans_g']]
        names = ['Saturated', 'Monounsaturated', 'Polyunsaturated', 'Trans']
        create_bar_chart(fats, names, "Fats Composition as a Percentage (%)", "Fatty Acid")

        st.subheader(f"All nutrient information for {selected_row['name']} per 100g")
        # Initialize a list to hold the columns
        columns = []

        for i, (category, nutrients) in enumerate(selected_row.items()):
            category = category.capitalize()

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
                st.markdown(f"##### {category}:\n")
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


def calculate_top_5_nutrients(food_data, food_item):
    nutrient_percentiles = {}

    for category in constants.NUTRIENT_CATEGORIES:
        category = category.lower()
        for nutrient, value in food_item[category].items():
            if value is not None: 
                # Get all the values for the nutrient, excluding None values
                nutrient_values = [item[category][nutrient] for item in food_data if nutrient in item[category] and item[category][nutrient] is not None]
                
                # Calculate the percentile of the food item's nutrient value
                percentile = stats.percentileofscore(nutrient_values, value)
                
                # Reverse the percentile
                reversed_percentile = 100 - percentile
                
                nutrient_percentiles[nutrient] = reversed_percentile

    # Sort the dictionary by percentile in descending order and get the top 5 nutrients
    top_nutrients = sorted(nutrient_percentiles.items(), key=lambda x: x[1])[:5]

    return top_nutrients


def display_top_nutrients(top_nutrients):
    st.markdown("#### Top Nutrients:")
    st.markdown("These nutrients are in the top percentiles compared to other foods in the database. The lower the percentage "
                "the more the nutrient is present in the food.")
    
    # Create 5 columns
    cols = st.columns(5)

    for i, (nutrient, percentile) in enumerate(top_nutrients):
        # Display the nutrient and its percentile in a column
        cols[i].markdown(f"**{reverse_format_nutrient_option(nutrient)}**:\n{round(percentile, 2)}%")

    # If there are less than 5 top nutrients, fill the remaining columns with empty strings
    for i in range(len(top_nutrients), 5):
        cols[i].write("")