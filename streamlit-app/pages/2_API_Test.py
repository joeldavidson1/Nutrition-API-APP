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

    food_to_search = st.text_input("Search for a food item", placeholder="Search for a food item, e.g. 'banana'")
    if food_to_search != "":
        response = api_handler.get_data_from_api(
        api_token, "foodItems", True, SearchFoodByName=food_to_search, PageSize=50)
        for food_item in response:
            if (st.button(food_item["Name"], key=food_item["FoodCode"])):
                st.session_state.selected_food = food_item

    # Quantity input
    if st.session_state.selected_food:
        st.write(f"Selected food: {st.session_state.selected_food['Name']}")
        st.session_state.quantity = st.number_input('Enter quantity (g)', min_value=0, value=st.session_state.quantity)

    # Add button
    if st.session_state.selected_food and st.session_state.quantity > 0:
        if st.button('Add to my food list'):
            food_with_quantity = add_food_with_quantity(st.session_state.selected_food, st.session_state.quantity)
            st.session_state.user_food_list.append(food_with_quantity)
            st.session_state.selected_food = None
            st.session_state.quantity = 0

    # User food list
    # st.write('Your food list:', st.session_state.user_food_list)

    # User food list
    st.markdown("#")
    if st.session_state.user_food_list:
        # # Convert the user's food list to a DataFrame
        # df = pd.DataFrame([(food['Name'], quantity) for food, quantity in st.session_state.user_food_list], columns=['Food', 'Quantity (g)'])
        
        # Display the DataFrame as a table
        # st.table(df)
        col1, col2, col3 = st.columns([3,1,1])
        col1.markdown("##### Food")
        col2.markdown("##### Quantity")

        # Display each food item with an input field for the quantity and a button for removal
        for i, (food, quantity) in enumerate(st.session_state.user_food_list):
            with col1:
                st.write("##")
                st.write(food['Name'])
            with col2:
                st.write("#####")
                new_quantity = st.number_input('Quantity (g)', label_visibility="collapsed", min_value=0, value=quantity, key=f'quantity_{i}')
                st.session_state.user_food_list[i] = (food, new_quantity)
            with col3:
                st.write("###")
                if st.button('Remove', key=f'remove_{i}'):
                    del st.session_state.user_food_list[i]
                    st.experimental_rerun()
   
        with col2:
            # Button to rerun the app
            if st.button('Update '):
                st.experimental_rerun()

        # st.write(st.session_state.user_food_list)

        # Calculate total nutrition values
        total_nutrition = defaultdict(float)
        for food, quantity in st.session_state.user_food_list:
            adjusted_food = adjust_nutrition_values(food, quantity)
            add_nutrition_values(adjusted_food, total_nutrition)

        # Display total nutrition values
        # st.json(total_nutrition)
        st.markdown("#")
        st.subheader("Total nutrition values:")

        for category, nutrients in total_nutrition.items():
            st.subheader(f"{category}:\n")
            for nutrient, value in nutrients.items():
                st.write(f"{reverse_format_nutrient_option(nutrient)}: {round(value, 2)}")

# Initialize session state variables
if 'prev_order_by' not in st.session_state:
    st.session_state.prev_order_by = None

with tab2:
    st.subheader("Choose to filter the food items by their energy or a specific nutrient")

    st.subheader("Choose a nutrient category:")
    selected_category = st.radio("", list(set(constants.NUTRIENTS_TO_CATEGORIES.values())))

    st.subheader(f"Select a nutrient from the {selected_category} category:")
    options = [nutrient for nutrient, category in constants.NUTRIENTS_TO_CATEGORIES.items() if category == selected_category]
    selected_option = st.radio("", options)

    formatted_option = format_nutrient_option(selected_option)

    order_by = f"{constants.NUTRIENTS_TO_CATEGORIES.get(selected_option).lower()} {formatted_option} desc"

    # Check if the selection has changed before sending the request
    if st.session_state.prev_order_by != order_by:
        response = api_handler.get_data_from_api(api_token, "foodItems", OrderBy=order_by)
        st.session_state.prev_order_by = order_by  # Update the previous selection
        
    with st.container():
        food_groups_response = api_handler.get_data_from_api(api_token, "foodGroups")
        # Create a dictionary where the keys are descriptions and the values are the food group codes
        food_groups_dict = {food_group["description"]: food_group["foodGroupCode"] for food_group in food_groups_response}
        
        # Create a list of descriptions
        food_groups = list(food_groups_dict.keys())
        food_groups.insert(0, "All")

        # Add a dropdown menu to filter by food group
        selected_group = st.selectbox("Filter by Food Group", food_groups)

        if selected_group == "All":
            response = api_handler.get_data_from_api(api_token, "foodItems", OrderBy=order_by)
        else:
            # Use the selected description to look up the food group code
            food_group_code = food_groups_dict[selected_group]
            response = api_handler.get_data_from_api(api_token, f"foodGroups/{food_group_code}/foodItems", OrderBy=order_by)
        
        response = convert_response_to_lowercase(response)
        st.subheader(f"Top x foods for {selected_option} and: {selected_group} Food Group(s):")
        if response:
            nutrient_data = {}
            for food in response:
                nutrient_data[food["Name"]] = food[constants.NUTRIENTS_TO_CATEGORIES.get(selected_option)][formatted_option]

            df = pd.DataFrame({"Food": list(nutrient_data.keys()), selected_option: list(nutrient_data.values())})
            st.write(df)
        else:
            st.write("No data available")