import api_handler
import credentials
import constants
import re
import streamlit as st
import plotly.express as px
import pandas as pd
from collections import defaultdict
import copy


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
def convert_response_to_lowercase(response):
    new_response = []
    for item in response:
        new_item = {}
        for k, v in item.items():
            if isinstance(v, dict):
                v = {k.lower(): v[k] for k in v}
            new_item[k] = v
        new_response.append(new_item)

    return new_response


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


api_token = credentials.api_key 
# response = api_handler.get_data_from_api(api_token)

st.title("API Testing")

tab1, tab2 = st.tabs(["Macronutrient Distribution", "Specific Nutrient"])

with tab1:
    # st.subheader("Every food item is displayed as per 100g")
    # st.write("""
    #      ##### Search a food and see the macronutrient distribution
    #     """)
    # food_to_search = st.text_input("Search for a food item", placeholder="Banana")
    
    # if food_to_search != "":
    #     response = api_handler.get_data_from_api(
    #     api_token, "foodItems", True, SearchFoodByName=food_to_search, PageSize=50)
    #     for food_item in response:
    #         if st.button(food_item["Name"], key=food_item["FoodCode"]):
    #             # st.write(f"Protein: {food_item['Macronutrients']['protein_g']}g")
    #             macronutrients = food_item['Macronutrients']
    #             fig = px.bar(x=['Protein', 'Fat', 'Carbohydrate'], y=list(macronutrients.values()), 
    #                         title=f"Macronutrient values for {food_item['Name']}",
    #                         labels={"x": "Macronutrient", "y": "Grams"})
    #             st.plotly_chart(fig)

    #             with st.expander("see more information"):
    #                 st.write(f"Food Code: {food_item['FoodCode']}")
    #                 st.write(f"Description of food: {food_item['Description']}")
    #                 st.write(f"Data source: {food_item['DataReferences']}")
    # Initialize the user food list in the session state if it doesn't exist
    if 'user_food_list' not in st.session_state:
        st.session_state.user_food_list = []
    if 'selected_food' not in st.session_state:
        st.session_state.selected_food = None
    if 'quantity' not in st.session_state:
        st.session_state.quantity = 0

    food_to_search = st.text_input("Search for a food item", placeholder="Banana")
    if food_to_search != "":
        response = api_handler.get_data_from_api(
        api_token, "foodItems", True, SearchFoodByName=food_to_search, PageSize=50)
        for food_item in response:
         