import streamlit as st
import helper
import constants
import api_handler
import credentials
import pandas as pd
from st_aggrid import AgGrid, GridOptionsBuilder
import plotly.express as px


# Initialize session state variables
if 'prev_order_by' not in st.session_state:
    st.session_state.prev_order_by = None

st.subheader("Choose to filter the food items by their energy or a nutrient")

st.subheader("Choose a nutrient category:")
selected_category = st.radio("selected", list(set(constants.NUTRIENTS_TO_CATEGORIES.values())), label_visibility="collapsed")

st.subheader(f"Select a nutrient from the {selected_category} category:")
options = [nutrient for nutrient, category in constants.NUTRIENTS_TO_CATEGORIES.items() if category == selected_category]
selected_option = st.radio("selected", options, label_visibility="collapsed")

formatted_option = helper.format_nutrient_option(selected_option, True)
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
    top_x_food_slider = st.slider('Move the slider to show (x) amount of foods', min_value=1, max_value=50, value=20)

    if selected_group == "All":
        response = api_handler.get_data_from_api(credentials.api_key, "foodItems", PageSize=top_x_food_slider, OrderBy=order_by)
    else:
        # Use the selected description to look up the food group code
        food_group_code = food_groups_dict[selected_group]
        response = api_handler.get_data_from_api(credentials.api_key, f"foodItems/{food_group_code}/foodItems", PageSize=top_x_food_slider, OrderBy=order_by)
    
    st.subheader(f"Top {top_x_food_slider} foods for {selected_option} and: {selected_group} Food Group(s):")
    if response:
        df = pd.DataFrame(response)

        # Format the selected option back to its original format
        formatted_option = helper.format_nutrient_option(selected_option, False)

        # Create a new column in df for the selected option and its values
        df[selected_option] = df.apply(lambda row: row[constants.NUTRIENTS_TO_CATEGORIES.get(selected_option).lower()][formatted_option], axis=1)
        df_to_display = df[['foodCode', 'name', selected_option]]
        st.markdown("#### Select a food item to view more details")
        gd = GridOptionsBuilder.from_dataframe(df_to_display)
        gd.configure_pagination(enabled=True)
        gd.configure_selection(selection_mode='single', use_checkbox=True)

        gd.configure_column('foodCode', width=60)
        gd.configure_column(selected_option, width=75)

        grid_table = AgGrid(df, gridOptions=gd.build(), 
                            height=500, width='100%', 
                            theme='streamlit', 
                            editable=False,
                            fit_columns_on_grid_load=True,
                            custom_css={
                            "#gridToolBar": {
                                "padding-bottom": "0px !important",
                                }
                                })

        # Sort the dataframe by the selected option and take the top X foods
        top_x = df_to_display.sort_values(by=selected_option, ascending=True).head(top_x_food_slider)

        if (selected_option == "kcal"):
            measurement = "kilocalories"
        elif (selected_option == "kj"):
            measurement = "kilojoules"
        else:
            measurement = helper.extract_measurement(selected_option) 

        # Display the top (x) as bar chart
        fig = px.bar(top_x, x=selected_option, y='name', orientation='h', title=f'Top {top_x_food_slider} foods for {selected_option} and: {selected_group} Food Group(s):', labels={"Name": "Food Name", selected_option: measurement}) 
        # Update y-axis to display all labels
        fig.update_yaxes(tickmode='linear', nticks=len(top_x))
        fig.update_xaxes(showgrid=True)
        fig.update_layout(height=800, bargap=0.3)
        st.plotly_chart(fig)

        helper.display_selected_row(grid_table, food_groups_dict) 

    else:
        st.write("No data available")