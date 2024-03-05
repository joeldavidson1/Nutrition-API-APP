import api_handler
import credentials
import constants
import re
import streamlit as st
import plotly.express as px
import pandas as pd
from collections import defaultdict
import copy
import helper


api_token = credentials.api_key 
# response = api_handler.get_data_from_api(api_token)

st.title("Search for food items and add them to a food list to view the nutrient composition")

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
        if (st.button(food_item["name"], key=food_item["foodCode"])):
            st.session_state.selected_food = food_item

# Quantity input
if st.session_state.selected_food:
    st.write(f"Selected food: {st.session_state.selected_food['name']}")
    st.session_state.quantity = st.number_input('Enter quantity (g)', min_value=0, value=st.session_state.quantity)

# Add button
if st.session_state.selected_food and st.session_state.quantity > 0:
    if st.button('Add to my food list'):
        food_with_quantity = helper.add_food_with_quantity(st.session_state.selected_food, st.session_state.quantity)
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
            st.write(food['name'])
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
        if st.button('Update'):
            st.experimental_rerun()

    # st.write(st.session_state.user_food_list)

    # Calculate total nutrition values
    total_nutrition = defaultdict(float)
    for food, quantity in st.session_state.user_food_list:
        adjusted_food = helper.adjust_nutrition_values(food, quantity)
        helper.add_nutrition_values(adjusted_food, total_nutrition)

    # Display total nutrition values
    # st.json(total_nutrition)
    st.markdown("#")
    st.subheader("Total nutrition values:")

    # for category, nutrients in total_nutrition.items():
    #     st.subheader(f"{category}:\n")
    #     for nutrient, value in nutrients.items():
    #         st.write(f"{reverse_format_nutrient_option(nutrient)}: {round(value, 2)}")

    macronutrients = total_nutrition['macronutrients']
    names = ['Protein', 'Fat', 'Carbohydrate']
    colour_dict = {'Protein': '#FF8333', 'Fat': '#FFD133', 'Carbohydrate': '#11A400'}
    helper.create_bar_chart(list(macronutrients.values()), names, "Macronutrient Composition as a Percentage (%)", "Macronutrient", colour_dict)

    fats = [total_nutrition['proximates'][fat] for fat in ['fatsSaturated_g', 'fatsMonounsaturated_g', 'fatsPolyunsaturated_g', 'fatsTrans_g']]
    names = ['Saturated', 'Monounsaturated', 'Polyunsaturated', 'Trans']
    helper.create_bar_chart(fats, names, "Fats Composition as a Percentage (%)", "Fatty Acid")
 
    # Initialize a list to hold the columns
    columns = []

    for i, (category, nutrients) in enumerate(total_nutrition.items()):
        # Create a new row every 2 categories
        if i % 2 == 0:
            columns = st.columns(2)
        
        # Convert the nutrients dictionary to a DataFrame
        df = pd.DataFrame([(nutrient, round(value, 2)) for nutrient, value in nutrients.items()], columns=['Nutrient', 'Value'])
        # Apply the reverse_format_nutrient_option method to the 'Nutrient' column
        df['Nutrient'] = df['Nutrient'].apply(helper.reverse_format_nutrient_option)

        # Use the current column for the category subheader and DataFrame
        with columns[i % 2]:
            st.subheader(f"{category.capitalize()}:\n")
            st.dataframe(df)
    