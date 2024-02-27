import streamlit as st
import helper
import constants
import api_handler
import credentials
import pandas as pd


# Initialize session state variables
if 'prev_order_by' not in st.session_state:
    st.session_state.prev_order_by = None

st.subheader("Choose to filter the food items by their energy or a nutrient")

st.subheader("Choose a nutrient category:")
selected_category = st.radio("selected", list(set(constants.NUTRIENTS_TO_CATEGORIES.values())), label_visibility="collapsed")

st.subheader(f"Select a nutrient from the {selected_category} category:")
options = [nutrient for nutrient, category in constants.NUTRIENTS_TO_CATEGORIES.items() if category == selected_category]
selected_option = st.radio("selected", options, label_visibility="collapsed")

formatted_option = helper.format_nutrient_option(selected_option)

order_by = f"{constants.NUTRIENTS_TO_CATEGORIES.get(selected_option).lower()} {formatted_option} desc"

# Check if the selection has changed before sending the request
if st.session_state.prev_order_by != order_by:
    response = api_handler.get_data_from_api(credentials.api_key, "foodItems", OrderBy=order_by)
    st.session_state.prev_order_by = order_by  # Update the previous selection
    
with st.container():
    food_groups_response = api_handler.get_data_from_api(credentials.api_key, "foodGroups")
    # Create a dictionary where the keys are descriptions and the values are the food group codes
    food_groups_dict = {food_group["description"]: food_group["foodGroupCode"] for food_group in food_groups_response}
    
    # Create a list of descriptions
    food_groups = list(food_groups_dict.keys())
    food_groups.insert(0, "All")

    # Add a dropdown menu to filter by food group
    selected_group = st.selectbox("Filter by Food Group", food_groups)
    top_x_food_slider = st.slider('Move the slider to show (x) amount of foods', min_value=1, max_value=50, value=10)

    if selected_group == "All":
        response = api_handler.get_data_from_api(credentials.api_key, "foodItems", PageSize=top_x_food_slider, OrderBy=order_by)
    else:
        # Use the selected description to look up the food group code
        food_group_code = food_groups_dict[selected_group]
        response = api_handler.get_data_from_api(credentials.api_key, f"foodGroups/{food_group_code}/foodItems", PageSize=top_x_food_slider, OrderBy=order_by)
    
    response = helper.convert_response_to_lowercase(response)
    st.subheader(f"Top {top_x_food_slider} foods for {selected_option} and: {selected_group} Food Group(s):")
    if response:
        nutrient_data = {}
        for food in response:
            nutrient_data[food["Name"]] = food[constants.NUTRIENTS_TO_CATEGORIES.get(selected_option)][formatted_option]

        df = pd.DataFrame({"Food": list(nutrient_data.keys()), selected_option: list(nutrient_data.values())})
        st.write(df)
    else:
        st.write("No data available")