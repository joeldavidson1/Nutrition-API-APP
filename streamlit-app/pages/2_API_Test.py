import api_handler
import credentials
import constants
import re
import streamlit as st
import plotly.express as px
import pandas as pd


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


api_token = credentials.api_key 
# response = api_handler.get_data_from_api(api_token)

st.title("API Testing")

tab1, tab2 = st.tabs(["Macronutrient Distribution", "Specific Nutrient"])

with tab1:
    st.subheader("Every food item is displayed as per 100g")
    st.write("""
         ##### Search a food and see the macronutrient distribution
        """)
    food_to_search = st.text_input("Search for a food item", placeholder="Banana")
    
    if food_to_search != "":
        response = api_handler.get_data_from_api(
        api_token, "foodItems", True, SearchFoodByName=food_to_search, PageSize=50)
        for food_item in response:
            if st.button(food_item["Name"], key=food_item["FoodCode"]):
                # st.write(f"Protein: {food_item['Macronutrients']['protein_g']}g")
                macronutrients = food_item['Macronutrients']
                fig = px.bar(x=['Protein', 'Fat', 'Carbohydrate'], y=list(macronutrients.values()), 
                            title=f"Macronutrient values for {food_item['Name']}",
                            labels={"x": "Macronutrient", "y": "Grams"})
                st.plotly_chart(fig)

                with st.expander("see more information"):
                    st.write(f"Food Code: {food_item['FoodCode']}")
                    st.write(f"Description of food: {food_item['Description']}")
                    st.write(f"Data source: {food_item['DataReferences']}")

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
        st.subheader(f"Top x foods for {selected_option} for the {selected_group} Food Group(s):")
        if response:
            nutrient_data = {}
            for food in response:
                nutrient_data[food["Name"]] = food[constants.NUTRIENTS_TO_CATEGORIES.get(selected_option)][formatted_option]

            df = pd.DataFrame({"Food": list(nutrient_data.keys()), selected_option: list(nutrient_data.values())})
            st.write(df)
        else:
            st.write("No data available")