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
response = api_handler.get_data_from_api(credentials.api_key, f"foodGroups/{food_group_code}/foodItems", True)

df = pd.DataFrame(response)
df_to_display = df[['FoodCode', 'Name']]
# AgGrid(df_to_display, )
# st.write(df_to_display)

st.subheader("Select a food item to view more details")
gd = GridOptionsBuilder.from_dataframe(df_to_display)
gd.configure_pagination(enabled=True)
gd.configure_selection(selection_mode='single', use_checkbox=True)
grid_table = AgGrid(df, gridOptions=gd.build(), height=500, width='100%', theme='streamlit', editable=False,
                     custom_css={
                    "#gridToolBar": {
                        "padding-bottom": "0px !important",
                        }
                        })

if (grid_table['selected_rows']):
    selected_row = grid_table['selected_rows'][0]
    # st.write(selected_row)
    st.subheader(f'{selected_row['FoodCode']}: {selected_row['Name']}')
    st.markdown(f"**Food Group**: {helper.get_key(food_groups_dict, selected_row['FoodGroupCode'])}")
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
        df['Nutrient'] = df['Nutrient'].apply(helper.reverse_format_nutrient_option)
        
        # Use the current column for the category subheader and DataFrame
        with columns[i % 2]:
            st.subheader(f"{category}:\n")
            st.dataframe(df)


