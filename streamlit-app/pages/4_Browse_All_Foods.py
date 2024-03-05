import streamlit as st
import pandas as pd
from st_aggrid import AgGrid, GridOptionsBuilder
import api_handler
import credentials
import helper
import constants

st.header("Browse All Foods from the CoFID Database")

food_groups_response = api_handler.get_data_from_api(credentials.api_key, "foodGroups")
food_groups_dict = {food_group["description"]: food_group["foodGroupCode"] for food_group in food_groups_response}
        
# Create a list of descriptions
food_groups = list(food_groups_dict.keys())

# Add a dropdown menu to filter by food group
selected_group = st.selectbox("Filter by Food Group", food_groups)

food_group_code = food_groups_dict[selected_group]
response = api_handler.get_data_from_api(credentials.api_key, f"foodItems/{food_group_code}/foodItems", True)

if response:
    df = pd.DataFrame(response)
    df_to_display = df[['foodCode', 'name']]
    # AgGrid(df_to_display, )
    # st.write(df_to_display)

    st.markdown('#### Select a food item to view more details')
    gd = GridOptionsBuilder.from_dataframe(df_to_display)
    gd.configure_pagination(enabled=True)
    gd.configure_selection(selection_mode='single', use_checkbox=True)

    gd.configure_column('FoodCode', width=60)

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

    helper.display_selected_row(grid_table, food_groups_dict) 
else:
    st.write(f"No food items found for the food group: {selected_group}")


